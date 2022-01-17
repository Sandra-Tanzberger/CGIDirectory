using System;
using System.Collections;
using System.DirectoryServices;
using Graebel.Common.GCIDirectory.Entities;
using Graebel.Common.GCIDirectory.Exceptions;

using Graebel.Common.GCIDirectory.IDirectoryAdapters;
using ActiveDs;
using System.Text;

namespace Graebel.Common.GCIDirectory.AdsiAdapters
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
				using (DirectoryEntry directoryEntry = GetDirectoryEntry(config) )
				{                
					DirectorySearcher search = new DirectorySearcher(directoryEntry);
					search.Filter = "(&(SAMAccountName="+samAccountName+")(objectcategory=person))";
					User user = null;
					try
					{
						SearchResult result = search.FindOne();
						DirectoryEntry de = result.GetDirectoryEntry();                                                                                 
						user = PopulateUser(de);

						//test
//						string sSid = string.Empty;
//						//byte [] bSid = (byte [])de.Properties["objectSID"][0];
//						byte [] usrSID = (byte[])de.Properties["objectSID"].Value;
//						foreach(byte b in usrSID)
//						{
//							sSid+= string.Format("{0:x2}", b);
//						}
//						Int16 i = 0;
						//end test
					}
					catch (NullReferenceException)
					{
						//throw new ApplicationException("Unknown User Name.");
					}
			
                                
					return user;
				} 	
			}
			catch (Exception ex)
			{
				System.Runtime.InteropServices.COMException COMEx = (System.Runtime.InteropServices.COMException) ex;
				throw(COMEx);
			}
		}


		public Users UserSearch(Entities.Configuration config, string samAccountName)
		{			
			using (DirectoryEntry directoryEntry = GetDirectoryEntry(config) )
			{
				DirectorySearcher search = new DirectorySearcher(directoryEntry);
				search.Filter = "(&(SAMAccountName="+samAccountName+"*)(objectcategory=person))";
			
				System.DirectoryServices.SearchResultCollection results = search.FindAll();
				Users users = new Users();				

				if (results != null)					
				{
					foreach ( System.DirectoryServices.SearchResult res in results )
					{
						users.Add( PopulateUser(res.GetDirectoryEntry()));			
					}
				}
				return users;
			}						
		}


/*
		/// <summary>
		///Search//craete an enum list of properties
		/// </summary>        
		public User SearchUsers(Entities.Configuration config, 
			string sEnum,
			string sValue)        
		{
			try
			{
				using (DirectoryEntry directoryEntry = GetDirectoryEntry(config) )
				{                
					DirectorySearcher search = new DirectorySearcher(directoryEntry);
					//search by enum
					search.Filter = "("+sEnum.ToString()+" like "+sValue+")";
					User user = null;
					try
					{
						SearchResult result = search.FindAll();
						foreach ()
						DirectoryEntry de = result.GetDirectoryEntry();                                                                                 
						user = PopulateUser(de);

						//test
						//						string sSid = string.Empty;
						//						//byte [] bSid = (byte [])de.Properties["objectSID"][0];
						//						byte [] usrSID = (byte[])de.Properties["objectSID"].Value;
						//						foreach(byte b in usrSID)
						//						{
						//							sSid+= string.Format("{0:x2}", b);
						//						}
						//						Int16 i = 0;
						//end test
					}
					catch (NullReferenceException)
					{
						//throw new ApplicationException("Unknown User Name.");
					}
			
                                
					return user;
				} 	
			}
			catch (Exception ex)
			{
				System.Runtime.InteropServices.COMException COMEx = (System.Runtime.InteropServices.COMException) ex;
				throw(COMEx);
			}
		}
*/
		/// <summary>
		/// Obtains a single User Object.  This method is intended to be used to
		/// query ad by the distinguished name
		/// </summary>        
		public User RetrieveUser(string adminUserName,
			string adminPassword,
			string distinguishedName)        
		{
			try
			{
				using (DirectoryEntry directoryEntry = new DirectoryEntry(distinguishedName, adminUserName, adminPassword, AuthenticationTypes.Secure) )
				{          
					DirectorySearcher search = new DirectorySearcher(directoryEntry);
					search.Filter = "(objectcategory=person)";
					SearchResult result = search.FindOne();
			
					DirectoryEntry deUser = result.GetDirectoryEntry();                                                                                 

					User user = PopulateUser(deUser);
	                                
					return user;
				}			
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
				DirectoryEntry directoryEntry = null;

				//try to connect to the primary server
//				try
//				{
					directoryEntry = GetDirectoryEntry(config.AdServer, userName, password, config.Domain);
//				}
//				catch (Exception ex)
//				{
//					if (ex is System.Runtime.InteropServices.COMException)
//					{
//						// If a COMException is thrown, then check for connectivity error
//						System.Runtime.InteropServices.COMException COMEx = (System.Runtime.InteropServices.COMException) ex;
//						if (!COMEx.ErrorCode.Equals(-2147016646))
//							throw(COMEx);
//					}
//					else
//					{
//						throw(ex);
//					}
//				}

				

				//if no connection to the ad server was made, try connecting to the path
				if (directoryEntry == null)
				{
					directoryEntry = GetDirectoryEntry(config.AdPath, userName, password, config.Domain);
				}

				if (directoryEntry != null)
				{    	
					DirectorySearcher search = new DirectorySearcher(directoryEntry);
					search.Filter = "(&(SAMAccountName="+userName+")(objectcategory=person))";
					SearchResult result = search.FindOne();				
					DirectoryEntry de = result.GetDirectoryEntry();

					user = PopulateUser(de);

				}

				if (user.AccountLockedOut)
				{
					//throw an exception to insure that the user cannot log into the system.
					throw new ApplicationException("User Account is Locked Out.");
				}
				if (user.AccountDisabled)
				{
					//throw an exception to insure that the user cannot log into the system.
					throw new ApplicationException("User Account is Disabled.");
				}

				return user;
			}
			catch (ApplicationException ex)
			{
				throw(ex);
			}
			catch (System.Runtime.InteropServices.COMException COMEx)
			{
		
				//System.Runtime.InteropServices.COMException COMEx = (System.Runtime.InteropServices.COMException) ex;

				if (COMEx.ErrorCode.Equals(Entities.Configuration.COM_EXCEPTION_UNKNOWN_USER_BAD_PASSWORD))
				{
					throw new ApplicationException(COMEx.Message, COMEx);
				}
				else
				{
					throw(COMEx);
				}
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
				using (DirectoryEntry directoryEntry = GetDirectoryEntry(config) )
				{
					DirectorySearcher search = new DirectorySearcher(directoryEntry);
					search.Filter = "(&(name="+organizationalUnit+")(objectClass=organizationalUnit))";
			
					SearchResult result = search.FindOne();
					DirectoryEntry ou = result.GetDirectoryEntry();

					// Create the user and set properties
					DirectoryEntry user = ou.Children.Add("cn="+firstName+" "+lastName, "user");
					user.Properties["sAMAccountName"].Add(userName);
					user.Properties["userPrincipalName"].Add(userName);				
					user.Properties["sn"].Add(lastName);
					user.Properties["givenName"].Add(firstName);
					user.Properties["description"].Add(description);
					user.CommitChanges();

					// User has to be saved prior to this step
					user.Invoke("SetPassword", new object[] {password} );

					// Create a normal account and enable it - ADS_UF_NORMAL_ACCOUNT
					user.Properties["userAccountControl"].Value = ADS_USER_FLAG.ADS_UF_NORMAL_ACCOUNT | 
						ADS_USER_FLAG.ADS_UF_DONT_EXPIRE_PASSWD; 
					user.CommitChanges();

					return GetProperty(user, "distinguishedName");
				}
			}
			catch (System.Runtime.InteropServices.COMException ex)
			{				
				if (ex.ErrorCode.Equals(-2147019886))
					throw new DirectoryException(DirectoryExceptionType.ObjectExists, ex, ex.Message);
				else
					throw ex;
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
				//using (DirectoryEntry directoryEntry = new DirectoryEntry(config.AdServer, config.AdAdminUsername, config.AdAdminPassword, AuthenticationTypes.Secure) )
//				using (DirectoryEntry directoryEntry = GetDirectoryEntry(config) )
//				{                
//					DirectorySearcher search = new DirectorySearcher(directoryEntry);
//					search.Filter = "(&(SAMAccountName="+samAccountName+")(objectcategory=person))";
//
//					SearchResult result = search.FindOne();
//				
//					// locate the user and set properties
//					DirectoryEntry user = result.GetDirectoryEntry();    
//					//user.Properties["cn"].Value = firstName+" "+lastName;                                                        
//					//user.Properties["sn"].Value = lastName;
//					//user.Properties["givenName"].Value = firstName;
//					user.Properties["description"].Value = description;
//					user.CommitChanges();
//				}
			}
			catch (Exception ex)
			{
				throw (ex);
			}
		}

		/// <summary>
		/// Delete an existing user
		/// </summary>        
		public void DeleteUser(Entities.Configuration config,
			string samAccountName)        
		{
			try
			{
				using (DirectoryEntry directoryEntry = GetDirectoryEntry(config) )
				{                
					DirectorySearcher search = new DirectorySearcher(directoryEntry);
					search.Filter = "(&(SAMAccountName="+samAccountName+")(objectcategory=person))";

					SearchResult result = search.FindOne();
				
					// locate the user and set properties
					DirectoryEntry user = result.GetDirectoryEntry();    
					user.DeleteTree();
					user.CommitChanges();
				}
			}
			catch (Exception ex)
			{
				System.Runtime.InteropServices.COMException COMEx = (System.Runtime.InteropServices.COMException) ex;
				throw(COMEx);
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
				using (DirectoryEntry directoryEntry = GetDirectoryEntry(config) )
				{                
					DirectorySearcher search = new DirectorySearcher(directoryEntry);
					search.Filter = "(&(SAMAccountName="+samAccountName+")(objectcategory=person))";

					SearchResult result = search.FindOne();
					DirectoryEntry user = result.GetDirectoryEntry();    

					//set the lockout flag
					user.Properties["userAccountControl"][0]= ADS_USER_FLAG.ADS_UF_LOCKOUT;
					
					//reset the lockout time to 0
					IADsLargeInteger li;
				
					li = (IADsLargeInteger)user.Properties["lockoutTime"][0];   
					li.HighPart = 0;
					li.LowPart = 0;
					user.Properties["LockoutTime"][0] = li;
					user.CommitChanges();
				}
			}
			catch (Exception)
			{
				//do nothing
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
				using (DirectoryEntry directoryEntry = GetDirectoryEntry(config) )
				{                
					DirectorySearcher search = new DirectorySearcher(directoryEntry);
					search.Filter = "(&(SAMAccountName="+samAccountName+")(objectcategory=person))";

					SearchResult result = search.FindOne();
					DirectoryEntry user = result.GetDirectoryEntry();    

					//enable the account by resetting all the account options excluding the disable flag
					user.Properties["userAccountControl"][0]=ADS_USER_FLAG.ADS_UF_NORMAL_ACCOUNT|ADS_USER_FLAG.ADS_UF_DONT_EXPIRE_PASSWD;
					user.CommitChanges();
				}
			}
			catch (Exception ex)
			{
				System.Runtime.InteropServices.COMException COMEx = (System.Runtime.InteropServices.COMException) ex;
				throw(COMEx);
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
				using (DirectoryEntry directoryEntry = GetDirectoryEntry(config) )
				{                
					DirectorySearcher search = new DirectorySearcher(directoryEntry);
					search.Filter = "(&(SAMAccountName="+userName+")(objectcategory=person))";

					SearchResult result = search.FindOne();
					DirectoryEntry user = result.GetDirectoryEntry();    

					user.Invoke("SetPassword", new object[] {newPassword} );
					user.CommitChanges();
				}
			}
			catch (System.Reflection.TargetInvocationException ex)
			{
				if (ex.InnerException != null && ex.InnerException is System.Runtime.InteropServices.COMException)
				{
					// If a COMException is thrown, then check for connectivity error
					System.Runtime.InteropServices.COMException COMEx = (System.Runtime.InteropServices.COMException) ex.InnerException;
					if (COMEx.ErrorCode.Equals(Entities.Configuration.COM_EXCEPTION_INVALID_NEW_PASSWORD))
					{
						throw new ApplicationException("User Services attempted to violate password rules.", ex);
					}
					else
					{
						throw(ex);
					}
				}
				else
				{
					throw(ex);
				}
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
				using (DirectoryEntry directoryEntry = GetDirectoryEntry(config) )
				{                
					DirectorySearcher search = new DirectorySearcher(directoryEntry);
					search.Filter = "(&(SAMAccountName="+samAccountName+")(objectcategory=person))";

					SearchResult result = search.FindOne();
					DirectoryEntry user = result.GetDirectoryEntry();    

					// disable the account by resetting all the default properties
					user.Properties["userAccountControl"][0]=ADS_USER_FLAG.ADS_UF_NORMAL_ACCOUNT|ADS_USER_FLAG.ADS_UF_DONT_EXPIRE_PASSWD|ADS_USER_FLAG.ADS_UF_ACCOUNTDISABLE;
					user.CommitChanges();
				}
			}
			catch (Exception ex)
			{
				System.Runtime.InteropServices.COMException COMEx = (System.Runtime.InteropServices.COMException) ex;
				throw(COMEx);
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
				throw new ApplicationException("User does not exist.");
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

			//
			if (IsPropertyNull(de, "firstName"))
			{
				user.SetFirstNameNull();
			}
			else
			{
				user.FirstName = GetProperty( de , "firstName") ;
			}

			if (IsPropertyNull(de, "lastName"))
			{
				user.SetLastNameNull();
			}
			else
			{
				user.LastName = GetProperty( de , "lastName") ;
			}

			if (IsPropertyNull(de, "displayName"))
			{
				user.SetDisplayNameNull();
			}
			else
			{
				user.DisplayName = GetProperty( de , "displayName") ;
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


