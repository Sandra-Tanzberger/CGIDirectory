using System;
using System.Collections;
using System.DirectoryServices;
using Graebel.Common.GCIDirectory.Entities;

using Graebel.Common.GCIDirectory.IDirectoryAdapters;
using ActiveDs;

namespace Graebel.Common.GCIDirectory.NtAdapters
{
	public class UserDirectoryAdapter : DirectoryAccessorBase, IUserDirectoryAdapter
	{               

		#region Retrieve User
		/// <summary>
		/// Obtains a single User Object.  This method is intended to be used by system 
		/// administrators when managing a particular user.
		/// </summary>        
		public User RetrieveUser(Entities.Configuration config, 
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
		/// Obtains a single User Object.  This method is intended to be used to
		/// query ad by the distinguished name
		/// </summary>        
		public User RetrieveUser(string adminUsername,
			string adminPassword,
			string distinguishedName)        
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


		public Users UserSearch(Entities.Configuration config, string samAccountName)
		{
			try
			{
//				using (DirectoryEntry directoryEntry = GetDirectoryEntry(config) )
//				{
//					DirectorySearcher search = new DirectorySearcher(directoryEntry);
//					search.Filter = "(&(SAMAccountName="+samAccountName+"*)(objectcategory=person))";
//				
//					return search.FindAll();
//				}
return null;
			}
			catch (Exception ex)
			{
				System.Runtime.InteropServices.COMException COMEx = (System.Runtime.InteropServices.COMException) ex;
				throw(COMEx);
			}
		
		}

		#endregion

		#region Login User
		/// <summary>
		/// Obtains a single User Object
		/// </summary>        
		public User LogonUser(Entities.Configuration config, 
			string userName, 
			string password)        
		{
			try
			{
				User user = null;

				using (DirectoryEntry deRoot = new DirectoryEntry(config.AdServer +"/Domain Users", userName, password, AuthenticationTypes.None) )
				{      
					user = PopulateUser(deRoot);
				}

				return user;
			}
			catch (ApplicationException ex)
			{
				throw(ex);
			}
			catch (Exception ex)
			{
				System.Runtime.InteropServices.COMException COMEx = (System.Runtime.InteropServices.COMException) ex;

				if (COMEx.ErrorCode.Equals(Entities.Configuration.COM_EXCEPTION_UNKNOWN_USER_BAD_PASSWORD))
				{
					throw new ApplicationException(COMEx.Message, COMEx);
				}

				throw(ex);
			}
		}

		#endregion

		#region Edit User

		/// <summary>
		/// Create a new user in the specified OU.  If no OU is specified,
		/// then we throw and error
		/// </summary>        
		public string CreateUser(Entities.Configuration config, 
			string organizationalUnit, 
			string userName,
			string password,
			string firstName,
			string lastName,
			string description)        
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
		/// Update an existing user
		/// </summary>        
		public void UpdateUser(Entities.Configuration config,
			string samAccountName,
			string firstName,
			string lastName,
			string description)        
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
		/// Delete an Existing User
		/// </summary>  		
		public void DeleteUser(Entities.Configuration config,
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
		/// Unlock a user account.  There should be no methods to lock 
		/// the account out.  Lockout will be managed by ADSI.
		/// </summary>        
		public void UnlockUserAccount(Entities.Configuration config,
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
		/// Enable an existing user account
		/// </summary>        
		public void EnableUserAccount(Entities.Configuration config,
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
		/// reset password
		/// 
		/// method does not include any validation to make sure that the user
		/// types the correct old password.
		/// </summary>        
		public void ResetPassword(Entities.Configuration config,
			string userName, 
			string newPassword)        
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
		/// Disable an existing user account
		/// </summary>        
		public void DisableUserAccount(Entities.Configuration config,
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

		#endregion

		#region Fill User Object

		public User PopulateUser(DirectoryEntry de)
		{
			User user = new User();
                
			if (de.NativeGuid==null)
			{
				///if the native guid is null, 
				///there is no sense in moving forward
				throw new NotImplementedException("User does not exist.");
			}
			else
			{
				user.NativeGuid = de.NativeGuid;
			}

			if (de.Guid==Guid.Empty)
			{
				user.SetGuidNull();
			}
			else
			{
				user.Guid = de.Guid;
			}
				                   
			if (IsPropertyNull(de, "accountExpires"))
			{
				user.SetAccountExpiresNull();
			}
			else
			{
				user.AccountExpires = GetPropertyDateTimeSys32(de, "accountExpires");
					
				if (user.AccountExpires.Equals(DateTime.MinValue))
				{
					user.SetAccountExpiresNull();
				}
			}
                                        
			if (IsPropertyNull(de, "badPwdCount"))
			{
				user.SetBadPwdCountNull();
			}
			else
			{
				user.BadPwdCount = SetPropertyToShort( GetProperty(de, "badPwdCount") );
			}
                                        
			if (IsPropertyNull(de, "cn"))
			{
				user.SetCnNull();
			}
			else
			{
				user.Cn = GetProperty(de, "cn");
			}
                                        
			if (IsPropertyNull(de, "description"))
			{
				user.SetDescriptionNull();
			}
			else
			{
				user.Description = GetProperty(de, "description");
			}
                                        
			if (IsPropertyNull(de, "givenName"))
			{
				user.SetGivenNameNull();
			}
			else
			{
				user.GivenName = GetProperty(de, "givenName");
			}
                                        
			if (IsPropertyNull(de, "lockoutTime"))
			{
				user.SetLockoutTimeNull();
			}
			else
			{
				user.LockoutTime = GetPropertyDateTimeSys32(de, "lockoutTime");

				if (user.LockoutTime.Equals(DateTime.MinValue))
				{
					user.SetLockoutTimeNull();
				}
			}
                                        
			if (IsPropertyNull(de, "distinguishedName"))
			{
				user.SetDistinguishedNameNull();
			}
			else
			{
				user.DistinguishedName = GetProperty(de, "distinguishedName");
			}
                                        
			if (IsPropertyNull(de, "objectCategory"))
			{
				user.SetObjectCategoryNull();
			}
			else
			{
				user.ObjectCategory = GetProperty(de, "objectCategory");
			}
                                        
			if (IsPropertyNull(de, "pwdLastSet"))
			{
				user.SetPwdLastSetNull();
			}
			else
			{
				user.PwdLastSet = GetPropertyDateTimeSys32(de, "pwdLastSet");
					
				if (user.PwdLastSet.Equals(DateTime.MinValue))
				{
					user.SetPwdLastSetNull();
				}
			}
                                        
			if (IsPropertyNull(de, "name"))
			{
				user.SetNameNull();
			}
			else
			{
				user.Name = GetProperty(de, "name");
			}
                                        
			if (IsPropertyNull(de, "samAccountName"))
			{
				user.SetSamAccountNameNull();
			}
			else
			{
				user.SamAccountName = GetProperty(de, "samAccountName");
			}
                                        
			if (IsPropertyNull(de, "samAccountType"))
			{
				user.SetSamAccountTypeNull();
			}
			else
			{
				user.SamAccountType = SetPropertyToInt( GetProperty(de, "samAccountType") );
			}
                                        
			if (IsPropertyNull(de, "sn"))
			{
				user.SetSnNull();
			}
			else
			{
				user.Sn = GetProperty(de, "sn");
			}
                                        
			if (IsPropertyNull(de, "userPrincipalName"))
			{
				user.SetUserPrincipalNameNull();
			}
			else
			{
				user.UserPrincipalName = GetProperty(de, "userPrincipalName");
			}
                                        
			if (IsPropertyNull(de, "whenChanged"))
			{
				user.SetWhenChangedNull();
			}
			else
			{
				user.WhenChanged = SetPropertyToDateTime( GetProperty(de, "whenChanged") );
			}
                                        
			if (IsPropertyNull(de, "whenCreated"))
			{
				user.SetWhenCreatedNull();
			}
			else
			{
				user.WhenCreated = SetPropertyToDateTime( GetProperty(de, "whenCreated") );
			}
                
			//flag to check for disabled account
			user.AccountDisabled = IsAccountDisabled(de);
                
			//flat to check for an expired password        
			if (IsPropertyNull(de, "accountExpires"))
			{
				user.SetPasswordExpiredNull();
			}
			else
			{
				user.PasswordExpired = IsAccountExpired(de);
			}
                
			//flat to check for a locked out account        
			if (IsPropertyNull(de, "lockoutTime"))
			{
				//if the result is false, the value returned will return a null from IsPropertyNull
				user.AccountLockedOut = false;
			}
			else
			{
				DateTime dtLockout = GetPropertyDateTimeSys32(de, "lockoutTime");
				
				if (dtLockout.Equals(DateTime.MinValue))
					user.AccountLockedOut = false;
				else
					user.AccountLockedOut = IsAccountLockedOut( dtLockout );
			}  
                                                            
			return user;
		}


		#endregion


	}
}


