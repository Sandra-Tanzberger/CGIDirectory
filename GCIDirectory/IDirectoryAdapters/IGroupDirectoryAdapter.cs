using System;
using System.Collections;
using System.DirectoryServices;

using Graebel.Common.GCIDirectory.Entities;

using ActiveDs;

namespace Graebel.Common.GCIDirectory.IDirectoryAdapters
{
	internal interface IGroupDirectoryAdapter
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
		string CreateGroup(Entities.Configuration config, 
			string organizationalUnit,
			string samAccountName,
			string description,
			ADS_GROUP_TYPE_ENUM groupType);

		/// <summary>
		/// This method will delete a group
		/// </summary>
		/// <param name="config"></param>
		/// <param name="samAccountName"></param>
		/// <returns>void</returns>
		void DeleteGroup(Entities.Configuration config, 
			string samAccountName);        

		/// <summary>
		/// This method will return a collection of Group Objects.  This method should be
		/// used by administrators.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="samAccountName"></param>
		/// <returns>Groups</returns>
		Groups GetGroupMembership(Entities.Configuration config, 
			string samAccountName);

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
		Groups GetGroupMembership(Entities.Configuration userConfig,
			string samAccountName,
			Entities.Configuration groupConfig,
			params string[] groupNames);        

		/// <summary>
		/// This method will return a collection of Group Objects.  This method should be
		/// used by administrators.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="organizationalUnit"></param>
		/// <returns>Groups</returns>
		Groups GetOrganizationalUnitGroups(Entities.Configuration config, 
			string organizationalUnit);

		/// <summary>
		/// This method will return an ArrayList of Orgazational Unit Objects.  
		/// </summary>
		/// <param name="config"></param>
		/// <returns>ArrayList</returns>
		ArrayList GetOrganizationalUnits(Entities.Configuration config);
     
		/// <summary>
		/// This method will return a collection of User Objects.  This method should be 
		/// used to retrieve all the users in a given group
		/// 
		/// </summary>
		/// <param name="config"></param>
		/// <param name="samAccountName"></param>
		/// <returns>Users</returns>
		Users GetUsersInGroup(Entities.Configuration config,
			string samAccountName);

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
		void AddUserToGroup(Entities.Configuration config, 
			string groupSamAccountName,
			string userSamAccountName);

		/// <summary>
		/// This method will add a group to a group across domains.
		/// </summary>
		/// <param name="parentConfig"></param>
		/// <param name="parentSamAccountName"></param>
		/// <param name="childConfig"></param>
		/// <param name="childSamAccountName"></param>
		/// <returns>void</returns>
		void AddGroupToGroup(Entities.Configuration parentConfig, 
			string parentSamAccountName,
			Entities.Configuration childConfig,
			string childSamAccountName);

		/// <summary>
		/// This method will add a user to a group across domains.
		/// </summary>
		/// <param name="groupConfig"></param>
		/// <param name="groupSamAccountName"></param>
		/// <param name="userConfig"></param>
		/// <param name="userSamAccountName"></param>
		/// <returns>void</returns>
		void AddUserToGroup(Entities.Configuration groupConfig,
			string groupSamAccountName,
			Entities.Configuration userConfig,
			string userSamAccountName);

		/// <summary>
		/// This method will add a user to a group within the same domain.
		/// We do not support removing users from groups across domains yet.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="groupSamAccountName"></param>
		/// <param name="userSamAccountName"></param>
		/// <returns>void</returns>
		void RemoveUserFromGroup(Entities.Configuration config, 
			string groupSamAccountName,
			string userSamAccountName);

		/// <summary>
		/// Removes a user from a group.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="groupSamAccountName"></param>
		/// <param name="userSamAccountName"></param>
		/// <returns>void</returns>
		void RemoveUserFromGroup(Entities.Configuration groupConfig,
			string groupSamAccountName,
			Entities.Configuration userConfig,
			string userSamAccountName);   

		/// <summary>
		/// This method will remove a group from a group across domains.
		/// </summary>
		/// <param name="parentConfig"></param>
		/// <param name="parentSamAccountName"></param>
		/// <param name="childConfig"></param>
		/// <param name="childSamAccountName"></param>
		/// <returns>void</returns>
		void RemoveGroupFromGroup(Entities.Configuration parentConfig, 
			string parentSamAccountName,
			Entities.Configuration childConfig,
			string childSamAccountName);

//		/// <summary>
//		/// This method will populate a Group entity from a DirectoryEntry.
//		/// </summary>
//		/// <param name="de"></param>
//		/// <returns>Group</returns>
//		Group PopulateGroup(DirectoryEntry de);
	}
}

