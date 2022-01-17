using System;
using System.Collections;
using System.Configuration;
using System.DirectoryServices;
using Graebel.Common.GCIDirectory.IDirectoryAdapters;
using Graebel.Common.GCIDirectory.Entities;
using Graebel.Common.GCIDirectory.Exceptions;

namespace Graebel.Common.GCIDirectory
{
	/// <summary>
	/// UserServices is a static class intended to be accessed through 
	/// Graebel aplications.  There are several core pieces of functionality
	/// that have been added.  We have the ability to manage users and groups
	/// in adsi version 5.0 adn version 6.0.		
	/// </summary>
	public class UserServices
	{
		
		/// <summary>
		/// Log the user in and return the User Entity from Directory Serices.
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		/// <param name="domain"></param>
		/// <returns>User</returns>
		public static User Login(string userName, string password, string domain)
		{
			//get the AD configuration information
			Entities.Configuration config = new Entities.Configuration(domain);
			IUserDirectoryAdapter uda = Factory.UserDirectoryAdapter.Create(config.ProviderNamespace);
			User user = uda.LogonUser(config, 
				userName,
				password);
			try
			{
				if (user != null)
				{
					IGroupDirectoryAdapter gda = Factory.GroupDirectoryAdapter.Create(config.ProviderNamespace);
					user.Groups = gda.GetGroupMembership(config,
						userName);
				}
			}
			catch (ApplicationException)
			{
				user.Groups = null;
			}	
			return user;			
		}

		/// <summary>
		/// Logins the user.
		/// </summary>
		/// <param name="userName">The user to login.</param>
		/// <param name="userDomain">The domain of the user.</param>
		/// <param name="groupDomain">The domain of the group.</param>
		/// <param name="groupNames">Array of group names to check for
		/// membership. Currently need to pass in the groups to check
		/// because of limitations with getting groups of
		/// foreign security principals.</param>
		/// <returns>User</returns>
		public static User Login(string userName, 
			string password,
			string userDomain,
			string[] groupNames,
			string groupDomain)
		{			
			Entities.Configuration userConfig = new Entities.Configuration(userDomain);
			Entities.Configuration groupConfig = new Entities.Configuration(groupDomain);
			ValidateSameProvider(userConfig, groupConfig);
			IUserDirectoryAdapter uda = Factory.UserDirectoryAdapter.Create(userConfig.ProviderNamespace);
			User user = uda.LogonUser(userConfig, 
				userName,
				password);
			try
			{
				if (user != null)
				{
					IGroupDirectoryAdapter gda = Factory.GroupDirectoryAdapter.Create(userConfig.ProviderNamespace);
					user.Groups = gda.GetGroupMembership(userConfig,
						userName,
						groupConfig,
						groupNames);
				}
			}
			catch (ApplicationException)
			{
				user.Groups = null;
			}	
			return user;				
		}

		/// <summary>
		/// Create a new user and return the distinguishedName
		/// </summary>
		/// <param name="domain"></param>
		/// <param name="organizationalUnit"></param>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		/// <param name="firstName"></param>
		/// <param name="lastName"></param>
		/// <param name="description"></param>
		/// <returns>String</returns>
		public static string CreateUser(string domain,
			string organizationalUnit, 
			string userName,
			string password, 
			string firstName,
			string lastName,
			string description)
		{
			Entities.Configuration config = new Entities.Configuration(domain);

			IUserDirectoryAdapter uda = Factory.UserDirectoryAdapter.Create(config.ProviderNamespace);

			return uda.CreateUser(config,
				organizationalUnit,
				userName, 
				password,
				firstName,
				lastName,
				description);			
		}

		/*
		 * Currently experiencing an error with constraint viiolations.
		 * The only field that is updatable is the description. The first 
		 * pass of this component should not enable updating.  If it becomes 
		 * necessary, we can further explore updating users.
		 */
		/// <summary>
		/// Update user properties
		/// </summary>
		/// <param name="domain"></param>
		/// <param name="userName"></param>
		/// <param name="firstName"></param>
		/// <param name="lastName"></param>
		/// <param name="description"></param>
		/// <returns></returns>
		public static void UpdateUser(string domain,
			string samAccountName,
			string firstName,
			string lastName,
			string description)
		{
			//get the AD configuration information
			Entities.Configuration config = new Entities.Configuration(domain);

			IUserDirectoryAdapter uda = Factory.UserDirectoryAdapter.Create(config.ProviderNamespace);
			uda.UpdateUser(config,
				samAccountName, 
				firstName,
				lastName,
				description);			
		}
		

		/// <summary>
		/// Delete a user account
		/// </summary>
		/// <param name="samAccountName"></param>
		/// <param name="domain"></param>
		/// <returns>void</returns>
		public static void DeleteUserAccount(string domain, string samAccountName)
		{
			Entities.Configuration config = new Entities.Configuration(domain);

			IUserDirectoryAdapter uda = Factory.UserDirectoryAdapter.Create(config.ProviderNamespace);
			uda.DeleteUser(config,
				samAccountName);			
		}

		/// <summary>
		/// Unlock a user account
		/// </summary>
		/// <param name="samAccountName"></param>
		/// <param name="domain"></param>
		/// <returns>void</returns>
		public static void UnlockUserAccount(string samAccountName, string domain)
		{
			Entities.Configuration config = new Entities.Configuration(domain);

			IUserDirectoryAdapter uda = Factory.UserDirectoryAdapter.Create(config.ProviderNamespace);
			uda.UnlockUserAccount(config,
				samAccountName);			
		}

		/// <summary>
		/// Enable a user account
		/// </summary>
		/// <param name="samAccountName"></param>
		/// <param name="domain"></param>
		/// <returns>void</returns>
		public static void EnableUserAccount(string samAccountName, string domain)
		{
			Entities.Configuration config = new Entities.Configuration(domain);

			IUserDirectoryAdapter uda = Factory.UserDirectoryAdapter.Create(config.ProviderNamespace);
			uda.EnableUserAccount(config,
				samAccountName);			
		}

		/// <summary>
		/// Disable a user account
		/// </summary>
		/// <param name="samAccountName"></param>
		/// <param name="domain"></param>
		/// <returns>void</returns>
		public static void DisableUserAccount(string samAccountName, string domain)
		{
			Entities.Configuration config = new Entities.Configuration(domain);

			//UserDirectoryAdapter uda = new UserDirectoryAdapter();
			IUserDirectoryAdapter uda = Factory.UserDirectoryAdapter.Create(config.ProviderNamespace);
			uda.DisableUserAccount(config,
				samAccountName);			
		}

		/// <summary>
		///reset user password
		///
		///Method does not check to see if user has typed a valid old password
		/// </summary>
		/// <param name="samAccountName"></param>
		/// <param name="domain"></param>
		/// <returns>void</returns>
		public static void ResetPassword(string userName, string newPassword, string domain)
		{
			Entities.Configuration config = new Entities.Configuration(domain);

			IUserDirectoryAdapter uda = Factory.UserDirectoryAdapter.Create(config.ProviderNamespace);
			uda.ResetPassword(config,
				userName, 
				newPassword);			
		}

		/// <summary>
		/// return the User Entity from Directory Serices.
		/// </summary>
		/// <param name="samAccountName"></param>
		/// <param name="domain"></param>
		/// <returns>User</returns>
		public static User RetrieveUser(string samAccountName, string domain)
		{
			User user = null;

			Entities.Configuration config = new Entities.Configuration(domain);
		
			IUserDirectoryAdapter uda = Factory.UserDirectoryAdapter.Create(config.ProviderNamespace);
			user = uda.RetrieveUser(config,
				samAccountName);

			if (user != null)
			{
				IGroupDirectoryAdapter gda = Factory.GroupDirectoryAdapter.Create(config.ProviderNamespace);
				user.Groups = gda.GetGroupMembership(config,
					samAccountName);
			}			
			return user;
		}

		/// <summary>
		/// return the User Entity from Directory Serices.
		/// </summary>
		/// <param name="userName">The user to retrieve.</param>
		/// <param name="userDomain">The domain of the user.</param>
		/// <param name="groupDomain">The domain of the group.</param>
		/// <param name="groupNames">Array of group names to check for
		/// membership. Currently need to pass in the groups to check
		/// because of limitations with getting groups of
		/// foreign security principals.</param>
		/// <returns>User</returns>
		public static User RetrieveUser(string userName, 
			string userDomain,
			string[] groupNames,
			string groupDomain)
		{
			User user = null;

			Entities.Configuration userConfig = new Entities.Configuration(userDomain);
			Entities.Configuration groupConfig = new Entities.Configuration(groupDomain);
			ValidateSameProvider(userConfig, groupConfig);
		
			IUserDirectoryAdapter uda = Factory.UserDirectoryAdapter.Create(userConfig.ProviderNamespace);
			user = uda.RetrieveUser(userConfig,
				userName);

			if (user != null)
			{
				IGroupDirectoryAdapter gda = Factory.GroupDirectoryAdapter.Create(userConfig.ProviderNamespace);
				user.Groups = gda.GetGroupMembership(userConfig,
					userName,
					groupConfig,
					groupNames);
			}			
			return user;
		}



		public Users UserSearch(string searchTerm, string userDomain)
		{

			Entities.Configuration userConfig = new Entities.Configuration(userDomain);

			IUserDirectoryAdapter uda = Factory.UserDirectoryAdapter.Create(userConfig.ProviderNamespace);
			
			return uda.UserSearch(userConfig, searchTerm);
		}



		#region Groups

		/// <summary>
		/// Return the groups That a user belongs to a user belongs to. This will not
		/// return any groups that exist in other domains.
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="domain"></param>
		/// <returns>Groups</returns>
		public static Groups RetrieveGroupMembership(string userName, string domain)
		{
			//get the AD configuration information
			Entities.Configuration config = new Entities.Configuration(domain);
		
			IGroupDirectoryAdapter gda = Factory.GroupDirectoryAdapter.Create(config.ProviderNamespace);
			Groups groups = gda.GetGroupMembership(config,
				userName);
						
			return groups;			
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
		public static Groups RetrieveGroupMembership(string userName,
			string userDomain,
			string[] groupNames,
			string groupDomain)        
		{	
			Entities.Configuration userConfig = new Entities.Configuration(userDomain);
			Entities.Configuration groupConfig = new Entities.Configuration(groupDomain);
			ValidateSameProvider(userConfig, groupConfig);
			IGroupDirectoryAdapter gda = Factory.GroupDirectoryAdapter.Create(userConfig.ProviderNamespace);
			return gda.GetGroupMembership(
				userConfig,
				userName,
				groupConfig,
				groupNames);
		}
		
		/// <summary>
		/// Validates that the config objects passed in are for the same
		/// provider.
		/// </summary>
		/// <param name="userConfig"></param>
		/// <param name="groupConfig"></param>
		private static void ValidateSameProvider(Entities.Configuration userConfig,
			Entities.Configuration groupConfig)
		{
			if (userConfig.ProviderNamespace != groupConfig.ProviderNamespace)
			{
				throw new Exception("Provider namespaces are different for the config " +
					"objects. The same provider must be used currently.");
			}
		}

		/// <summary>
		/// return all the Groups in the Organizational Unit.
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="domain"></param>
		/// <returns>Groups</returns>
		public static Groups RetrieveOrganizationalUnitGroups(string domain, string organizationalUnit)
		{
			Groups groups;

			//get the AD configuration information
			Entities.Configuration config = new Entities.Configuration(domain);
		
			IGroupDirectoryAdapter gda = Factory.GroupDirectoryAdapter.Create(config.ProviderNamespace);
			groups = gda.GetOrganizationalUnitGroups(config, 
				organizationalUnit);										
			return groups;
		}		

		/// <summary>
		/// return all the Organizational Units in a domain.
		/// </summary>
		/// <param name="domain"></param>
		/// <returns>ArrayList</returns>
		public static ArrayList RetrieveOrganizationalUnits(string domain)
		{
			//get the AD configuration information
			Entities.Configuration config = new Entities.Configuration(domain);
		
			IGroupDirectoryAdapter gda = Factory.GroupDirectoryAdapter.Create(config.ProviderNamespace);
			ArrayList ouList = gda.GetOrganizationalUnits(config);
						
			return ouList;			
		}				
		/// <summary>
		/// return the users that are members of a specified group.
		/// </summary>
		/// <param name="samAccountName"></param>
		/// <param name="domain"></param>
		/// <returns>Users</returns>
		public static Users RetrieveUsersInGroup(string samAccountName, string domain)
		{
			//get the AD configuration information
			Entities.Configuration config = new Entities.Configuration(domain);
		
			IGroupDirectoryAdapter gda = Factory.GroupDirectoryAdapter.Create(config.ProviderNamespace);
			Users users = gda.GetUsersInGroup(config,
				samAccountName);

			//check to see if the users belong to the foreign security principals
			for (int i=0;users.Count-1>=i;i++)
			//foreach (User user in users)
			{
				//if the samaccountname is empty, the user is foreign
				if (users[i].SamAccountName.Equals(string.Empty))
				{
					if (users[i].Name != string.Empty)
					{
						string sDomain = string.Empty;
						string sUser = string.Empty;
	
						Utilities.SidWrapper.ConvertSid(users[i].Name, ref sUser, ref sDomain);
					
						users.RemoveAt(i);
						User user = RetrieveUser(sUser, sDomain);
						users.Add(user);
					}
				}
			}
						
			return users;			
		}			
	
		/// <summary>
		/// Add a user to a group within the same domain.
		/// </summary>
		/// <param name="groupSamAccountName"></param>
		/// <param name="userSamAccountName"></param>
		/// <param name="domain"></param>
		/// <returns>void</returns>
		public static void AddUserToGroup(string groupSamAccountName, string userSamAccountName, string domain)
		{
			//get the AD configuration information
			Entities.Configuration config = new Entities.Configuration(domain);
		
			IGroupDirectoryAdapter gda = Factory.GroupDirectoryAdapter.Create(config.ProviderNamespace);
			gda.AddUserToGroup(config,
				groupSamAccountName,
				userSamAccountName);			
		}	

		/// <summary>
		/// Adds a user to a group, where the user and group may
		/// be in different domains.
		/// </summary>
		/// <param name="groupDomain"></param>
		/// <param name="groupSamAccountName"></param>
		/// <param name="userDomain"></param>
		/// <param name="userSamAccountName"></param>
		public static void AddUserToGroup(string groupDomain,
			string groupSamAccountName,
			string userDomain,
			string userSamAccountName)        
		{				
			//get the AD configuration information
			Entities.Configuration groupConfig = new Entities.Configuration(groupDomain);		
			//get the AD configuration information
			Entities.Configuration userConfig = new Entities.Configuration(userDomain);
			ValidateSameProvider(userConfig, groupConfig);
			IGroupDirectoryAdapter gda = Factory.GroupDirectoryAdapter.Create(groupConfig.ProviderNamespace);
			gda.AddUserToGroup(groupConfig,
				groupSamAccountName,
				userConfig,
				userSamAccountName);			
		}
			
		/// <summary>
		/// Add a group to a group across domains.  There is a limitation
		/// that if the groups are from different domains, the parent 
		/// group must be a domain local group.  
		/// </summary>
		/// <param name="parentDomain"></param>
		/// <param name="parentSamAccountName"></param>
		/// <param name="childDomain"></param>
		/// <param name="childSamAccountName"></param>
		/// <returns>void</returns>
		public static void AddGroupToGroup(string parentDomain,
			string parentSamAccountName,
			string childDomain, 
			string childSamAccountName)
		{
			//get the parent AD configuration information
			Entities.Configuration parentConfig = new Entities.Configuration(parentDomain);

			//get the child AD configuration information
			Entities.Configuration childConfig = new Entities.Configuration(childDomain);
		
			IGroupDirectoryAdapter gda = Factory.GroupDirectoryAdapter.Create(parentConfig.ProviderNamespace);
			gda.AddGroupToGroup(parentConfig,
				parentSamAccountName,
				childConfig,
				childSamAccountName);				
		}	

		/// <summary>
		/// Remove a group from a group across domains.  
		/// </summary>
		/// <param name="parentDomain"></param>
		/// <param name="parentSamAccountName"></param>
		/// <param name="childDomain"></param>
		/// <param name="childSamAccountName"></param>
		/// <returns>void</returns>
		public static void RemoveGroupFromGroup(string parentDomain,
			string parentSamAccountName,
			string childDomain, 
			string childSamAccountName)
		{
			//get the parent AD configuration information
			Entities.Configuration parentConfig = new Entities.Configuration(parentDomain);

			//get the child AD configuration information
			Entities.Configuration childConfig = new Entities.Configuration(childDomain);
		
			IGroupDirectoryAdapter gda = Factory.GroupDirectoryAdapter.Create(parentConfig.ProviderNamespace);
			gda.RemoveGroupFromGroup(parentConfig,
				parentSamAccountName,
				childConfig,
				childSamAccountName);				
		}	
			
		/// <summary>
		/// Remove a user from a group within the same domain.
		/// </summary>
		/// <param name="groupSamAccountName"></param>
		/// <param name="userSamAccountName"></param>
		/// <param name="domain"></param>
		/// <returns>void</returns>
		public static void RemoveUserFromGroup(string groupSamAccountName, string userSamAccountName, string domain)
		{
			//get the AD configuration information
			Entities.Configuration config = new Entities.Configuration(domain);
		
			IGroupDirectoryAdapter gda = Factory.GroupDirectoryAdapter.Create(config.ProviderNamespace);
			gda.RemoveUserFromGroup(config,
				groupSamAccountName,
				userSamAccountName);			
		}	
		
		/// <summary>
		/// Removes a user from a group across domains.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="groupSamAccountName"></param>
		/// <param name="userSamAccountName"></param>
		/// <returns>void</returns>
		public static void RemoveUserFromGroup(
			string groupSamAccountName,
			string groupDomain,			
			string userSamAccountName,
			string userDomain)        
		{
			//get the AD configuration information for the group
			Entities.Configuration groupConfig = new Entities.Configuration(groupDomain);		
			//get the AD configuration information for the user
			Entities.Configuration userConfig = new Entities.Configuration(userDomain);
			ValidateSameProvider(userConfig, groupConfig);
			IGroupDirectoryAdapter gda = Factory.GroupDirectoryAdapter.Create(groupConfig.ProviderNamespace);
			gda.RemoveUserFromGroup(groupConfig, 
				groupSamAccountName,
				userConfig,
				userSamAccountName);							
		}			

		/// <summary>
		/// Create a DomainLocal group.
		/// </summary>
		/// <param name="domain"></param>
		/// <param name="organizationalUnit"></param>
		/// <param name="samAccountName"></param>
		/// <param name="description"></param>
		/// <returns>string</returns>
		public static string CreateDomainLocalGroup(string domain, 
			string organizationalUnit,
			string samAccountName,
			string description
			)
		{
			//get the AD configuration information
			Entities.Configuration config = new Entities.Configuration(domain);

			IGroupDirectoryAdapter gda = Factory.GroupDirectoryAdapter.Create(config.ProviderNamespace);
			string distinguishedName = gda.CreateGroup(config, 
				organizationalUnit,
				samAccountName,
				description,
				ActiveDs.ADS_GROUP_TYPE_ENUM.ADS_GROUP_TYPE_DOMAIN_LOCAL_GROUP);
						
			return distinguishedName;			
		}

		/// <summary>
		/// Create a Global group.
		/// </summary>
		/// <param name="domain"></param>
		/// <param name="organizationalUnit"></param>
		/// <param name="samAccountName"></param>
		/// <param name="description"></param>
		/// <returns>string</returns>
		public static string CreateGlobalGroup(string domain, 
			string organizationalUnit,
			string samAccountName,
			string description
			)
		{
			//get the AD configuration information
			Entities.Configuration config = new Entities.Configuration(domain);
		
			IGroupDirectoryAdapter gda = Factory.GroupDirectoryAdapter.Create(config.ProviderNamespace);
			string distinguishedName = gda.CreateGroup(config, 
				organizationalUnit, 
				samAccountName,
				description,
				ActiveDs.ADS_GROUP_TYPE_ENUM.ADS_GROUP_TYPE_GLOBAL_GROUP);
						
			return distinguishedName;			
		}

		/// <summary>
		/// Create a Global group.
		/// </summary>
		/// <param name="domain"></param>
		/// <param name="samAccountName"></param>
		/// <returns>void</returns>
		public static void DeleteGroup(string domain, 
			string samAccountName)
		{
			//get the AD configuration information
			Entities.Configuration config = new Entities.Configuration(domain);
		
			IGroupDirectoryAdapter gda = Factory.GroupDirectoryAdapter.Create(config.ProviderNamespace);
			gda.DeleteGroup(config, 
				samAccountName);			
		}
		#endregion


	}
}
