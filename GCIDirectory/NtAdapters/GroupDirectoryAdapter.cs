using System;
using System.Collections;
using System.DirectoryServices;

using Graebel.Common.GCIDirectory.Entities;
using Graebel.Common.GCIDirectory.IDirectoryAdapters;

using ActiveDs;

namespace Graebel.Common.GCIDirectory.NtAdapters
{
	public class GroupDirectoryAdapter : DirectoryAccessorBase, IGroupDirectoryAdapter
	{
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
				throw new NotImplementedException("Functionality not supported by current provider");
			}
			catch (Exception ex)
			{
				throw(ex);
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
			try
			{
				throw new NotImplementedException("Functionality not supported by current provider");
			}
			catch (Exception ex)
			{
				throw(ex);
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
			throw new NotImplementedException("Functionality not supported by current provider");			
		}

		/// <summary>
		/// This method will return a collection of Group Objects. This will return
		/// the group membership in the specified group domain. Use this when needing
		/// the list of groups when the groups and user are in different domains.
		/// </summary>		
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
			throw new NotImplementedException("Functionality not supported by current provider");		
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
			try
			{
				throw new NotImplementedException("Functionality not supported by current provider");
			}
			catch (Exception ex)
			{
				throw(ex);
			}
		}

		/// <summary>
		/// This method will return an ArrayList of Orgazational Unit Objects.  
		/// </summary>
		/// <param name="config"></param>
		/// <returns>ArrayList</returns>
		public ArrayList GetOrganizationalUnits(Entities.Configuration config)        
		{	
			try
			{
				throw new NotImplementedException("Functionality not supported by current provider");
			}
			catch (Exception ex)
			{
				throw(ex);
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
			try
			{
				throw new NotImplementedException("Functionality not supported by current provider");
			}
			catch (Exception ex)
			{
				throw(ex);
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
				throw new NotImplementedException("Functionality not supported by current provider");
			}
			catch (Exception ex)
			{
				throw(ex);
			}

		}

		public void AddUserToGroup(Entities.Configuration groupConfig,
			string groupSamAccountName,
			Entities.Configuration userConfig,
			string userSamAccountName)        
		{	
			try
			{
				throw new NotImplementedException("Functionality not supported by current provider");
			}
			catch (Exception ex)
			{
				throw(ex);
			}

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
			try
			{
				throw new NotImplementedException("Functionality not supported by current provider");
			}
			catch (Exception ex)
			{
				throw(ex);
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
			try
			{
				throw new NotImplementedException("Functionality not supported by current provider");
			}
			catch (Exception ex)
			{
				throw(ex);
			}

		}

		/// <summary>
		/// Removes a user from a group.
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
			throw new NotImplementedException("Functionality not supported by current provider");	
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
			throw new NotImplementedException("Functionality not supported by current provider");			
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

