using System;
using System.Text;
using System.Collections;
using System.DirectoryServices;

using Graebel.Common.GCIDirectory.Entities;
using Graebel.Common.GCIDirectory.IDirectoryAdapters;
using Graebel.Common.GCIDirectory.Exceptions;

using ActiveDs;

namespace Graebel.Common.GCIDirectory.AdsiAdapters
{
	public class GroupDirectoryAdapter : DirectoryAccessorBase, IGroupDirectoryAdapter
	{
		private const int OBJECT_EXISTS_ERROR_CODE = -2147019886;
		
		/// <summary>
		/// This method will create a new group and return the distinguishedName.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="organizationalUnit"></param>
		/// <param name="samAccountName"></param>
		/// <param name="description"></param>
		/// <param name="groupType"></param>
		/// <returns>string</returns>
		public string CreateGroup(Entities.Configuration config, 
			string organizationalUnit,
			string samAccountName,
			string description,
			ADS_GROUP_TYPE_ENUM groupType)        
		{	
			try
			{
				using (DirectoryEntry directoryEntry = GetDirectoryEntry(config) )
				{                
					DirectorySearcher search = new DirectorySearcher(directoryEntry);
					search.Filter = "(&(name="+organizationalUnit+")(objectClass=organizationalUnit))";

					SearchResult result = search.FindOne();			
					DirectoryEntry ou = result.GetDirectoryEntry();  

					DirectoryEntry newGroup = ou.Children.Add("cn="+samAccountName, "group");
					newGroup.Properties["sAMAccountName"].Add(samAccountName);
					newGroup.Properties["description"].Add(description);
					newGroup.Properties["groupType"].Value = groupType | 
						ADS_GROUP_TYPE_ENUM.ADS_GROUP_TYPE_SECURITY_ENABLED;

					newGroup.CommitChanges();

					return newGroup.NativeGuid;

				}
			}
			catch (System.Runtime.InteropServices.COMException ex)
			{				
				if (ex.ErrorCode.Equals(OBJECT_EXISTS_ERROR_CODE))
					throw new DirectoryException(DirectoryExceptionType.ObjectExists, ex, ex.Message);
				else
					throw ex;
			}			
		}

		/// <summary>
		/// This method will delete a group
		/// </summary>
		/// <param name="config"></param>
		/// <param name="samAccountName"></param>
		/// <returns>void</returns>
		public void DeleteGroup(Entities.Configuration config, 
			string samAccountName)        
		{	
			using (DirectoryEntry directoryEntry = GetDirectoryEntry(config) )
			{                
				DirectorySearcher search = new DirectorySearcher(directoryEntry);
				search.Filter = "(&(SAMAccountName="+samAccountName+")(objectcategory=group))";

				SearchResult result = search.FindOne();			
				DirectoryEntry group = result.GetDirectoryEntry();                                                                                 

				group.DeleteTree();
				group.CommitChanges();

			}			
		}

		/// <summary>
		/// This method will return a collection of Group Objects.  This method should be
		/// used by administrators.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="samAccountName"></param>
		/// <returns>Groups</returns>
		public Groups GetGroupMembership(Entities.Configuration config,
			string samAccountName)        
		{	
			using (DirectoryEntry directoryEntry = GetDirectoryEntry(config) )
			{                
				DirectorySearcher search = new DirectorySearcher(directoryEntry);
				search.Filter = "(&(SAMAccountName="+samAccountName+")(objectcategory=person))";
				SearchResult result;
				DirectoryEntry user;
				try
				{
					result = search.FindOne();			
					user = result.GetDirectoryEntry();                                                                                 
				}
				catch (NullReferenceException)
				{
					//throw new ApplicationException("User not found.");
					return null;
				}
				object oGroups = user.Invoke("Groups");	
				Groups groups = new Groups();
				foreach (object ob in (IEnumerable)oGroups)
				{
					// Create object for each group.
					DirectoryEntry groupEntry = new DirectoryEntry(ob);
					groups.Add( PopulateGroup( groupEntry ) );
				}
				return groups;
			}					
		}

		/// <summary>
		/// This method will return a collection of Group Objects. This will return
		/// the group membership in the specified group domain. Use this when needing
		/// the list of groups when the groups and user are in different domains.
		/// </summary>
		/// <remarks>Note that to find the groups a foreign security principal belongs
		/// to (a user in a different domain) each group's users are examined
		/// to find the foreign security principal SID in the name attribute. 
		/// We were able to bind directly to the foreign security principal, but
		/// the "memberOf" attribute always returned null.</remarks>
		/// <param name="config"></param>
		/// <param name="samAccountName"></param>
		/// <param name="groupNames">The list of groups that should
		/// be checked for membership.</param>		
		/// <returns>The groups this user belongs to, in the specified 
		/// group domain. Note that nested group membership will not be
		/// returned.</returns>
		public Groups GetGroupMembership(Entities.Configuration userConfig,
			string samAccountName,
			Entities.Configuration groupConfig,
			params string[] groupNames)        
		{	
			Groups groups = new Groups();
			//if we're not dealing with a foreign security principal, then
			//use the usual method.
			if (userConfig.Domain == groupConfig.Domain)
			{
				groups = GetGroupMembership(userConfig, samAccountName);
			}
			else
			{
				string userSID = null;
				using (DirectoryEntry userDirectoryEntry = GetDirectoryEntry(userConfig) )
				{                
					// Find the User to get the SID
					DirectoryEntry userInHomeDomain = GetUser(userDirectoryEntry, samAccountName);														
					byte[] objectSid = (byte[])userInHomeDomain.Properties["objectSid"].Value;
					userSID = Utilities.SidWrapper.ConvertSidToStringSid(objectSid);									
				}					
				if (groupNames != null)
				{
					using (DirectoryEntry groupDirectoryEntry = GetDirectoryEntry(groupConfig))
					{									
						foreach (string groupName in groupNames)
						{							
							DirectoryEntry group = null;
							try
							{
								group = GetGroup(groupDirectoryEntry, groupName); 
							}
							catch (DirectoryException)
							{//group not found, don't error out
							}
							if (group != null)
							{
								object members = group.Invoke("Members",null);
								foreach (object member in (IEnumerable) members)
								{
									DirectoryEntry de = new DirectoryEntry(member);
									if (de.Properties["name"][0].ToString() == userSID)
									{
										groups.Add(PopulateGroup(group));									
										break;
									}								
								}
							}
						}
					}
				}
			}	
			return groups;
		}

		/// <summary>
		/// Returns a reference to a foreign security principal.
		/// </summary>		
		/// <returns>DirectoryEntry</returns>
		private DirectoryEntry GetForeignSecurityPrincipal(Entities.Configuration config, string sid)        
		{	
			//get a reference to the root 
			using (DirectoryEntry directoryEntry = GetDirectoryEntry(config) )
			{                
				DirectorySearcher userSearch = new DirectorySearcher(directoryEntry);
				userSearch.PropertiesToLoad.Add("memberOf");
				userSearch.Filter = "(&(objectSid="+sid+")(objectcategory=foreignsecurityprincipal))";
				SearchResult userResult = userSearch.FindOne();			
				if (userResult == null)
				{
					throw new Exception("User not found with account name of " + sid);
				}
				return userResult.GetDirectoryEntry(); 												
			}		
		}

		/// <summary>
		/// This method will return a collection of Group Objects.  This method should be
		/// used by administrators.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="organizationalUnit"></param>
		/// <returns>Groups</returns>
		public Groups GetOrganizationalUnitGroups(Entities.Configuration config,
			string organizationalUnit)        
		{	
			//get a reference to the root 
			using (DirectoryEntry directoryEntry = GetDirectoryEntry(config) )
			{                
				//get a referenct to the ou
				DirectorySearcher search = new DirectorySearcher(directoryEntry);
				search.Filter = "(&(name="+organizationalUnit+")(objectClass=organizationalUnit))";

				SearchResult result = search.FindOne();			
				DirectoryEntry ou = result.GetDirectoryEntry();  

				DirectorySearcher src = new DirectorySearcher(ou,"(objectCategory=group)");
				//search for all global groups
				//int val = (int) ActiveDs.ADS_GROUP_TYPE_ENUM.ADS_GROUP_TYPE_GLOBAL_GROUP;
				//string query = "(&(objectCategory=group)(groupType:1.2.840.113556.1.4.804:=" + val.ToString() + "))";
				int val = (int) ADS_GROUP_TYPE_ENUM.ADS_GROUP_TYPE_SECURITY_ENABLED;
				string query = "(&(objectCategory=group)(groupType:1.2.840.113556.1.4.804:=" + val.ToString() + "))";

				src.Filter = query;

				Groups groups = new Groups();

				foreach(SearchResult res in src.FindAll())
				{
					Group group = PopulateGroup( res.GetDirectoryEntry() );
					groups.Add(group);
				}
				
	
				return groups;
			}				
		}

		/// <summary>
		/// This method will return an ArrayList of Orgazational Unit Objects.  
		/// </summary>
		/// <param name="config"></param>
		/// <returns>ArrayList</returns>
		public ArrayList GetOrganizationalUnits(Entities.Configuration config)        
		{	
			//get a reference to the root 
			using (DirectoryEntry directoryEntry = GetDirectoryEntry(config) )
			{                
				//get a referenct to the ou
				DirectorySearcher search = new DirectorySearcher(directoryEntry);
				search.Filter = "(objectClass=organizationalUnit)";

				ArrayList ouList = null;

				if (search.FindAll().Count > 0)
				{		
					ouList = new ArrayList();	
					foreach(SearchResult result in search.FindAll())
					{
						ouList.Add( result.GetDirectoryEntry().Name.Replace("OU=","") );
					}
				}
			
				return ouList;
			}			
		}
     
		/// <summary>
		/// This method will return a collection of User Objects.  This method should be 
		/// used to retrieve all the users in a given group
		/// 
		/// </summary>
		/// <param name="config"></param>
		/// <param name="samAccountName"></param>
		/// <returns>Users</returns>
		public Users GetUsersInGroup(Entities.Configuration config,
			string samAccountName)
		{	
			using (DirectoryEntry directoryEntry = GetDirectoryEntry(config) )
			{                

				DirectorySearcher search = new DirectorySearcher(directoryEntry);
				search.Filter = "(&(SAMAccountName="+samAccountName+")(objectcategory=group))";

				SearchResult result = search.FindOne();			
				DirectoryEntry group = result.GetDirectoryEntry();                                                                                 

				UserDirectoryAdapter uda = new UserDirectoryAdapter();
				Users users = new Users();

				object members = group.Invoke("Members",null);
				foreach( object member in (IEnumerable) members)
				{
					DirectoryEntry de = new DirectoryEntry(member);
					User user = uda.PopulateUser(de);
					users.Add(user);

				}
				//					foreach(object distinguishedName in group.Properties["member"] )
				//					{
				//						User user = uda.RetrieveUser(provider+distinguishedName.ToString(), adminUserName, adminPassword, authTypes);
				//						users.Add(user);
				//					}
				return users;

			}			
		}

		/// <summary>
		/// This method will add a user to a group within the same domain.
		/// We do not support adding users to groups across domains yet.
		/// 
		/// In the event that cross domain membership is required, we need to 
		/// create a domain local group on the destination domain, 
		/// say... gweb\groupdomainlocal.  Then the user, graebel\ruser can become
		/// a member of the domain local group since gweb trusts gds.graebel.com. 
		/// </summary>
		/// <param name="config"></param>
		/// <param name="groupSamAccountName"></param>
		/// <param name="userSamAccountName"></param>
		/// <returns>void</returns>
		public void AddUserToGroup(Entities.Configuration config,
			string groupSamAccountName,
			string userSamAccountName)        
		{	
			try
			{
				//get a reference to the root 
				using (DirectoryEntry directoryEntry = GetDirectoryEntry(config) )
				{                
					// Find the User
					DirectoryEntry user = GetUser(directoryEntry, userSamAccountName);					
					// Find the Group
					DirectoryEntry group = GetGroup(directoryEntry, groupSamAccountName);  
					// To add multiple users to a group use AddRange( new string[] {"...userDnHere","...userDnHere..."});
					group.Properties["member"].Add(user.Properties["distinguishedName"].Value);
					group.CommitChanges();

					group.Close();
					user.Close();
				}
			}
			catch (System.Runtime.InteropServices.COMException ex)
			{				
				if (ex.ErrorCode.Equals(OBJECT_EXISTS_ERROR_CODE))
				{
					//if object is already in group, then ignore exception
				}
				else
				{
					throw ex;
				}
			}	
		}

		/// <summary>
		/// This adds a user to a group in another domain.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="groupSamAccountName"></param>
		/// <param name="userSamAccountName"></param>
		/// <returns>void</returns>
		public void AddUserToGroup(Entities.Configuration groupConfig,
			string groupSamAccountName,
			Entities.Configuration userConfig,
			string userSamAccountName)        
		{	
			if (groupConfig.Domain == userConfig.Domain)
			{
				AddUserToGroup(groupConfig, groupSamAccountName, userSamAccountName);
			}
			else
			{
				try
				{
					//get a reference to the group directory entry
					using (DirectoryEntry groupDirectoryEntry = GetDirectoryEntry(groupConfig) )
					{                
						// Find the Group						
						DirectoryEntry group = GetGroup(groupDirectoryEntry, groupSamAccountName);                                                                               
						// Find the User
						using (DirectoryEntry userDirectoryEntry = GetDirectoryEntry(userConfig))
						{								
							DirectoryEntry user = GetUser(userDirectoryEntry, userSamAccountName);																	
							//need to add the user to the remote domain 
							//through it's SID value (S-1-5-21-1708537768-329068152-1801674531-1381)						
							byte[] objectSid = (byte[])user.Properties["objectSid"].Value;
							string userSID = Utilities.SidWrapper.ConvertSidToStringSid(objectSid);						
							ActiveDs.IADsGroup iadsGroup = (ActiveDs.IADsGroup)group.NativeObject;   
							iadsGroup.Add("LDAP://<SID=" + userSID + ">");				
							iadsGroup.SetInfo();  								
							group.CommitChanges();						
							group.Close();
							user.Close();
						}
					}
				}
				catch (System.Runtime.InteropServices.COMException ex)
				{				
					if (ex.ErrorCode.Equals(OBJECT_EXISTS_ERROR_CODE))
					{
						//if object is already in group, then ignore exception
						//throwing exception for same thing in CreateGroup and CreateUser
						//so client knows that they are using a duplicate name
						//in this case, just trying to add a user to a group, and if 
						//it is already in there client shouldn't need to do anything different
					}
					else
					{
						throw ex;
					}
				}
			}
		}
		

		/// <summary>
		/// Gets a DirectoryEntry object representing a user.
		/// </summary>
		/// <param name="directoryEntry"></param>
		/// <param name="groupSamAccountName"></param>
		/// <returns></returns>
		private DirectoryEntry GetUser(DirectoryEntry directoryEntry, string userSamAccountName)
		{			
			DirectorySearcher userSearch = new DirectorySearcher(directoryEntry);
			userSearch.Filter = "(&(SAMAccountName="+userSamAccountName+")(objectcategory=person))";
			SearchResult userResult = userSearch.FindOne();			
			if (userResult == null)
			{
				throw new DirectoryException("User not found with account name of " + userSamAccountName);
			}
			return userResult.GetDirectoryEntry(); 							
		}

		/// <summary>
		/// Gets a DirectoryEntry object representing a group.
		/// </summary>
		/// <param name="directoryEntry"></param>
		/// <param name="groupSamAccountName"></param>
		/// <returns></returns>
		private DirectoryEntry GetGroup(DirectoryEntry directoryEntry, string groupSamAccountName)
		{			
			DirectorySearcher groupSearch = new DirectorySearcher(directoryEntry);
			groupSearch.Filter = "(&(SAMAccountName="+groupSamAccountName+")(objectcategory=group))";
			SearchResult groupResult = groupSearch.FindOne();
			if (groupResult == null)
			{
				
				throw new DirectoryException("Group not found with name of " + groupSamAccountName);
			}
			return groupResult.GetDirectoryEntry();   			
		}

		/// <summary>
		/// This method will add a group to a group.  We have experienced issues attempting 
		/// to perform this task across domains.
		/// </summary>
		/// <param name="parentConfig"></param>
		/// <param name="parentSamAccountName"></param>
		/// <param name="childConfig"></param>
		/// <param name="childSamAccountName"></param>
		/// <returns>void</returns>
		public void AddGroupToGroup(Entities.Configuration parentConfig, 
			string parentSamAccountName,
			Entities.Configuration childConfig, 
			string childSamAccountName)        
		{	
			//get a reference to the parent Root 
			using (DirectoryEntry parentRootEntry = GetDirectoryEntry(parentConfig) )
			{                
				// Find the Parent Object
				DirectorySearcher parentSearch = new DirectorySearcher(parentRootEntry);
				parentSearch.Filter = "(&(SAMAccountName="+parentSamAccountName+")(objectcategory=group))";
				SearchResult parentResult = parentSearch.FindOne();			
				DirectoryEntry parent = parentResult.GetDirectoryEntry(); 
				
				//get a reference to the child object
				DirectoryEntry childRootEntry = GetDirectoryEntry(childConfig); 

				// Find the Child Object
				DirectorySearcher childSearch = new DirectorySearcher(childRootEntry);
				childSearch.Filter = "(&(SAMAccountName="+childSamAccountName+")(objectcategory=group))";
				SearchResult childResult = childSearch.FindOne();			
				DirectoryEntry child = childResult.GetDirectoryEntry();                                                                                 

				// To add multiple groups to a group use AddRange( new string[] {"...userDnHere","...userDnHere..."});
				parent.Properties["member"].Add(child.Properties["distinguishedName"].Value);
				parent.CommitChanges();

				parent.Close();
				child.Close();
			}			
		}

		/// <summary>
		/// This method will add a user to a group within the same domain.
		/// We do not support removing users from groups across domains yet.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="groupSamAccountName"></param>
		/// <param name="userSamAccountName"></param>
		/// <returns>void</returns>
		public void RemoveUserFromGroup(Entities.Configuration config,
			string groupSamAccountName,
			string userSamAccountName)        
		{	
			//get a reference to the root 
			using (DirectoryEntry directoryEntry = GetDirectoryEntry(config) )
			{                
				// Find the User
				DirectorySearcher userSearch = new DirectorySearcher(directoryEntry);
				userSearch.Filter = "(&(SAMAccountName="+userSamAccountName+")(objectcategory=person))";
				SearchResult userResult = userSearch.FindOne();			
				DirectoryEntry user = userResult.GetDirectoryEntry(); 
				
				// Find the Group
				DirectorySearcher groupSearch = new DirectorySearcher(directoryEntry);
				groupSearch.Filter = "(&(SAMAccountName="+groupSamAccountName+")(objectcategory=group))";
				SearchResult groupResult = groupSearch.FindOne();			
				DirectoryEntry group = groupResult.GetDirectoryEntry();                                                                                 

				//remove the user
				try
				{
					group.Properties["member"].Remove(user.Properties["distinguishedName"].Value);
					group.CommitChanges();
			
					group.RefreshCache();
					user.RefreshCache();
				}
				catch (ArgumentException)
				{
					//will get this if user was not in collection
					//just eat this
				}
				group.Close();
				user.Close();
			}		
		}

		/// <summary>
		/// Removes a user from a group, where the user and group may be
		/// in different domains.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="groupSamAccountName"></param>
		/// <param name="userSamAccountName"></param>
		/// <returns>void</returns>
		public void RemoveUserFromGroup(Entities.Configuration groupConfig,
			string groupSamAccountName,
			Entities.Configuration userConfig,
			string userSamAccountName)        
		{										
			//if user and group are in the same domain, use the usual method
			if (groupConfig.Domain == userConfig.Domain)
			{
				RemoveUserFromGroup(groupConfig, groupSamAccountName, userSamAccountName);
			}
			else
			{
				//get a reference to the root 
				using (DirectoryEntry groupDirectoryEntry = GetDirectoryEntry(groupConfig) )
				{                
					using (DirectoryEntry userDirectoryEntry = GetDirectoryEntry(userConfig))
					{					
						// Find the Group						
						DirectoryEntry group = GetGroup(groupDirectoryEntry, groupSamAccountName);                                                                               
						DirectoryEntry user = GetUser(userDirectoryEntry, userSamAccountName);																	
						//need to remove the user to the remote domain 
						//through it's SID value (S-1-5-21-1708537768-329068152-1801674531-1381)						
						byte[] objectSid = (byte[])user.Properties["objectSid"].Value;
						string userSID = Utilities.SidWrapper.ConvertSidToStringSid(objectSid);					
						ActiveDs.IADsGroup iadsGroup = (ActiveDs.IADsGroup)group.NativeObject;   
						iadsGroup.Remove("LDAP://<SID=" + userSID + ">");				
						iadsGroup.SetInfo();  						
						group.CommitChanges();
						group.Close();
						user.Close();					
					}				
				}	
			}
		}

		/// <summary>
		/// This method will remove a group from a group across domains.
		/// </summary>
		/// <param name="parentConfig"></param>
		/// <param name="parentSamAccountName"></param>
		/// <param name="childConfig"></param>
		/// <param name="childSamAccountName"></param>
		/// <returns>void</returns>
		public void RemoveGroupFromGroup(Entities.Configuration parentConfig, 
			string parentSamAccountName,
			Entities.Configuration childConfig, 
			string childSamAccountName)  
		{	
			//get a reference to the parent Root 
			using (DirectoryEntry parentRootEntry = GetDirectoryEntry(parentConfig) )
			{                
				// Find the Parent Object
				DirectorySearcher parentSearch = new DirectorySearcher(parentRootEntry);
				parentSearch.Filter = "(&(SAMAccountName="+parentSamAccountName+")(objectcategory=group))";
				SearchResult parentResult = parentSearch.FindOne();			
				DirectoryEntry parent = parentResult.GetDirectoryEntry(); 
				
				//get a reference to the child object
				DirectoryEntry childRootEntry = GetDirectoryEntry(childConfig);
				
				// Find the Child Object
				DirectorySearcher childSearch = new DirectorySearcher(childRootEntry);
				childSearch.Filter = "(&(SAMAccountName="+childSamAccountName+")(objectcategory=group))";
				SearchResult childResult = childSearch.FindOne();			
				DirectoryEntry child = childResult.GetDirectoryEntry();                                                                                 

				//remove the user
				parent.Properties["member"].Remove(child.Properties["distinguishedName"].Value);
				parent.CommitChanges();
			
				parent.RefreshCache();
				child.RefreshCache();

				parent.Close();
				child.Close();
			}			
		}


		#region Populate Group

		public Group PopulateGroup(DirectoryEntry de)
		{
			Group group = new Group();
                
			if (de.NativeGuid==null)
			{
				///if the native guid is null, 
				///there is no sense in moving forward
				throw new ApplicationException("User does not exist.");
			}
			else
			{
				group.NativeGuid = de.NativeGuid;
			}

			if (de.Guid==Guid.Empty)
			{
				group.SetGuidNull();
			}
			else
			{
				group.Guid = de.Guid;
			}
				                                                           
			if (IsPropertyNull(de, "cn"))
			{
				group.SetCnNull();
			}
			else
			{
				group.Cn = GetProperty(de, "cn");
			}
                                        
			if (IsPropertyNull(de, "description"))
			{
				group.SetDescriptionNull();
			}
			else
			{
				group.Description = GetProperty(de, "description");
			}
                                        
			if (IsPropertyNull(de, "groupType"))
			{
				group.SetGroupTypeNull();
			}
			else
			{
				group.GroupType = SetPropertyToInt( GetProperty(de, "groupType") );
			}
                                                                                
			if (IsPropertyNull(de, "distinguishedName"))
			{
				group.SetDistinguishedNameNull();
			}
			else
			{
				group.DistinguishedName = GetProperty(de, "distinguishedName");
			}
                                        
			if (IsPropertyNull(de, "objectCategory"))
			{
				group.SetObjectCategoryNull();
			}
			else
			{
				group.ObjectCategory = GetProperty(de, "objectCategory");
			}
                                                                                
			if (IsPropertyNull(de, "name"))
			{
				group.SetNameNull();
			}
			else
			{
				group.Name = GetProperty(de, "name");
			}
                                        
			if (IsPropertyNull(de, "samAccountName"))
			{
				group.SetSamAccountNameNull();
			}
			else
			{
				group.SamAccountName = GetProperty(de, "samAccountName");
			}
                                        
			if (IsPropertyNull(de, "samAccountType"))
			{
				group.SetSamAccountTypeNull();
			}
			else
			{
				group.SamAccountType = SetPropertyToInt( GetProperty(de, "samAccountType") );
			}
                                                                               
			if (IsPropertyNull(de, "whenChanged"))
			{
				group.SetWhenChangedNull();
			}
			else
			{
				group.WhenChanged = SetPropertyToDateTime( GetProperty(de, "whenChanged") );
			}
                                        
			if (IsPropertyNull(de, "whenCreated"))
			{
				group.SetWhenCreatedNull();
			}
			else
			{
				group.WhenCreated = SetPropertyToDateTime( GetProperty(de, "whenCreated") );
			}
                                                                            
			return group;
		}
		#endregion

	}
}

