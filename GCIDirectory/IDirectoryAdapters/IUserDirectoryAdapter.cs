using System;

using System.DirectoryServices;
using Graebel.Common.GCIDirectory.Entities;


namespace Graebel.Common.GCIDirectory.IDirectoryAdapters
{
	/// <summary>
	/// Summary description for IUserDirectoryAdapter.
	/// </summary>
	internal interface IUserDirectoryAdapter
	{

		/// <summary>
		/// Obtains a single User Object.  This method is intended to be used by system 
		/// administrators when managing a particular user.
		/// </summary>        
		User RetrieveUser(Entities.Configuration config, 
			string samAccountName);

		/// <summary>
		/// Obtains a single User Object.  This method is intended to be used to
		/// query ad by the distinguished name
		/// </summary>        
		User RetrieveUser(string adminUsername,
			string adminPassword,
			string distinguishedName);        
		
		/// <summary>
		/// Obtains a single User Object
		/// </summary>        
		User LogonUser(Entities.Configuration config, 
			string userName, 
			string password);

		/// <summary>
		/// return searchresults collection for the searchterm paseed in
		/// </summary>
		/// <param name="config"></param>
		/// <param name="userName"></param>
		/// <returns></returns>
		Users UserSearch(Entities.Configuration config, 
												string searchTerm);

		/// <summary>
		/// Create a new user in the specified OU.  If no OU is specified,
		/// then we throw and error
		/// </summary>        
		string CreateUser(Entities.Configuration config, 
			string organizationalUnit, 
			string userName,
			string password,
			string firstName,
			string lastName,
			string description);

		/// <summary>
		/// Update an existing user
		/// </summary>        
		void UpdateUser(Entities.Configuration config,
			string samAccountName,
			string firstName,
			string lastName,
			string description);

		/// <summary>
		/// Delete an existing user
		/// </summary>        
		void DeleteUser(Entities.Configuration config,
			string samAccountName);  

		/// <summary>
		/// Unlock a user account.  There should be no methods to lock 
		/// the account out.  Lockout will be managed by ADSI.
		/// </summary>        
		void UnlockUserAccount(Entities.Configuration config,
			string samAccountName);

		/// <summary>
		/// Enable an existing user account
		/// </summary>        
		void EnableUserAccount(Entities.Configuration config,
			string samAccountName);

		/// <summary>
		/// reset password
		/// 
		/// method does not include any validation to make sure that the user
		/// types the correct old password.
		/// </summary>        
		void ResetPassword(Entities.Configuration config,
			string userName, 
			string newPassword);        
		
		/// <summary>
		/// Disable an existing user account
		/// </summary>        
		void DisableUserAccount(Entities.Configuration config,
			string samAccountName);        

//		/// <summary>
//		/// This method will populate a User entity from a DirectoryEntry.
//		/// </summary>
//		/// <param name="de"></param>
//		/// <returns>User</returns>
//		User PopulateUser(DirectoryEntry de);
	}
}
