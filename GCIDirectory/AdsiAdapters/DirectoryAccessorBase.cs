#region Copyright © 2003 - 2004 Graebel Companies, Inc.
//
//	This software is Copyright © 2003 - 2004 
//	Graebel Companies, Inc.
//	16346 East Airport Circle
//	Aurora, CO 80011
//
//	All rights reserved. 
//
//	This web site is provided by Graebel Companies, Inc. ("Graebel"). The contents 
//	hereof are protected by state, federal, and international copyright laws and 
//	may be used for Graebel related purposes only. Modification and redistribution
//	of this source code is subject to the terms and conditions set forth in this notice. 

//	Redistribution of compiled binary ("dll") is permitted provided that the 
//	compiled software is used to develop Graebel Companies, Inc related software.  
//	Reverse Engineering of this code is strictly prohibited .
// 
#endregion

using System;
using System.Collections;
using System.Diagnostics;
using System.DirectoryServices;

using ActiveDs;
using Graebel.Common.GCIDirectory.Entities;

namespace Graebel.Common.GCIDirectory.AdsiAdapters
{
	/// <summary>
	/// Base class for all DirectoryServices accessors. 
	/// </summary>
	public abstract class DirectoryAccessorBase
	{    
		/// <summary>
		/// This method will attempt to connect to the Primary Domain Controler
		/// and return a directory entry object.
		/// 
		/// If the primary domain conroler is unavailable, then this method
		/// will attempt to connect to the ad path.
		/// </summary>
		/// <param name="config"></param>
		/// <returns>DirectoryEntry</returns>
		protected static DirectoryEntry GetDirectoryEntry(Entities.Configuration config)
		{			
			DirectoryEntry directoryEntry = null;
			
			//attempt to connect to the pdc
			try
			{
				directoryEntry = GetDirectoryEntry(config.AdServer, config.AdAdminUsername, config.AdAdminPassword);
			}
			catch (Exception ex)
			{
				// If a COMException is thrown, then check for connectivity error
				System.Runtime.InteropServices.COMException COMEx = (System.Runtime.InteropServices.COMException) ex;
				if (!COMEx.ErrorCode.Equals(-2147016646))
					throw(COMEx);
			}
		
			//attempt to connect to the ldap path
			if (directoryEntry == null)
			{
				directoryEntry = GetDirectoryEntry(config.AdPath, config.AdAdminUsername, config.AdAdminPassword);
			}

			return directoryEntry;
		}	

		/// <summary>
		/// This method will attempt to connect to the LDAP server, It also verifies the 
		/// ldap server is alive. 
		/// </summary>
		/// <param name="path"></param>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		/// <returns>DirectoryEntry</returns>
		protected static DirectoryEntry GetDirectoryEntry(string path, string userName, string password)
		{			
			DirectoryEntry directoryEntry = null;
			
			try
			{
				directoryEntry = new DirectoryEntry(path, userName, password, AuthenticationTypes.Secure);
				DirectorySearcher search = new DirectorySearcher(directoryEntry);
				search.Filter = "(&(SAMAccountName="+userName+")(objectcategory=person))";
				SearchResult result = search.FindOne();	
		
				search = null;
				result = null;
			}
			catch (Exception ex)
			{
				// If a COMException is thrown, toss it up to the next layer
				System.Runtime.InteropServices.COMException COMEx = (System.Runtime.InteropServices.COMException) ex;
				throw(COMEx);
			}


			return directoryEntry;
		
		}	

		/// <summary>
		/// This method will attempt to connect to the LDAP server, It also verifies the 
		/// ldap server is alive.   This method should be used for logging onto the system
		/// </summary>
		/// <param name="path"></param>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		/// <param name="domain"></param>
		/// <returns>DirectoryEntry</returns>
		protected static DirectoryEntry GetDirectoryEntry(string path, string userName, string password, string domain)
		{			
			DirectoryEntry directoryEntry = null;
			
			try
			{
				directoryEntry = new DirectoryEntry(path, domain+@"\"+userName, password, AuthenticationTypes.Secure);
				DirectorySearcher search = new DirectorySearcher(directoryEntry);
				search.Filter = "(&(SAMAccountName="+userName+")(objectcategory=person))";
				SearchResult result = search.FindOne();	
		
				search = null;
				result = null;
			}
			catch (Exception ex)
			{
				// If a COMException is thrown, toss it up to the next layer
				System.Runtime.InteropServices.COMException COMEx = (System.Runtime.InteropServices.COMException) ex;
				throw(COMEx);
			}


			return directoryEntry;
		
		}	

		/// <summary>
		/// This will retreive the specified poperty value from the 
		/// DirectoryEntry object (if the property exists)
		/// </summary>
		/// <param name="de"></param>
		/// <param name="propertyName"></param>
		/// <returns>string</returns>
		protected static string GetProperty(DirectoryEntry de, string propertyName)
		{	

			try
			{
				return de.Properties[propertyName][0].ToString() ;
			}
			catch
			{
				return string.Empty;
			}


		}

		/// <summary>
		/// retrieve meaningful data from the com interop interfaces
		/// accountExpires, lastLogon,lockoutTime, and msExchMailboxSecurityDescriptor
		/// </summary>
		/// <param name="de"></param>
		/// <param name="propertyName"></param>
		/// <returns>string</returns>
		protected static DateTime GetPropertyDateTimeSys32(DirectoryEntry de, string propertyName)
		{	

			try
			{

				IADsLargeInteger li = (IADsLargeInteger)de.Properties[propertyName][0];    
				long date = (long)li.HighPart << 32 | (uint)li.LowPart;

				if (date == 0)
					return DateTime.MinValue;

				return DateTime.FromFileTime(date);

			}
			catch (Exception ex)
			{
				string v = ex.Message;
				return DateTime.MinValue;
			}
		}

		/// <summary>
		/// Returns true or false
		/// </summary>
		/// <param name="de"></param>
		/// <param name="propertyName"></param>
		/// <returns>bool</returns>
		protected static bool IsPropertyNull(DirectoryEntry de, string propertyName)
		{
			if (de.Properties.Contains(propertyName))
			{
				//check to see if the value is equal to string.Empty
				if (de.Properties[propertyName][0].ToString().Trim().Equals(string.Empty))
					return true;
				else
					return false;
			}
			else
			{
				return true;
			}
		}	

		/// <summary>
		/// This will cast the value as a datetime
		/// </summary>
		/// <param name="property"></param>
		/// <returns>DateTime</returns>
		protected static DateTime SetPropertyToDateTime(string property)
		{			
			try
			{
				return DateTime.Parse( property );
			}
			catch
			{
				return DateTime.MinValue;
			}
		}		

		/// <summary>
		/// This will cast the value as an int
		/// </summary>
		/// <param name="property"></param>
		/// <returns>int</returns>
		protected static int SetPropertyToInt(string property)
		{			
			try
			{
				return int.Parse( property );
			}
			catch
			{
				return 0;
			}
		}		

		/// <summary>
		/// This will cast the value as a short
		/// </summary>
		/// <param name="property"></param>
		/// <returns>short</returns>
		protected static short SetPropertyToShort(string property)
		{			
			try
			{
				return short.Parse( property );
			}
			catch
			{
				return 0;
			}
		}		

		/// <summary>
		/// This will cast the value as a bool
		/// </summary>
		/// <param name="property"></param>
		/// <returns>bool</returns>
		protected static bool SetPropertyToBool(string property)
		{			
			try
			{
				return bool.Parse( property );
			}
			catch
			{
				return false;
			}
		}		

		/// <summary>
		/// This will cast the value as a bool
		/// </summary>
		/// <param name="property"></param>
		/// <returns>bool</returns>
		protected static bool IsAccountLockedOut(DateTime lockoutAccount)
		{			
			try
			{
				if ( (lockoutAccount < DateTime.Today)||(!lockoutAccount.Equals(DateTime.Today)) )
					return true;
				else
					return false;

			}
			catch
			{
				return false;
			}
		}		
		
		/// <summary>
		/// This will check the account control value to verify the account is disabled
		/// </summary>
		/// <param name="de"></param>
		/// <returns>bool</returns>
		protected static bool IsAccountDisabled(DirectoryEntry de)
		{			
			try
			{
				//0x0002's binary expression is 0000,0000,0000,0010, so ~0x0002 is 
				//1111,1111,1111,1101.
				//val & ~ADS_UF_ACCOUNTDISABLE equals val&1111,1111,1111,1101 which makes all 
				//the other bits stay the same value as before, only the second bit becomes 0.
				//Then, when invoke CommitChanges(), .Net Framework will check second bit of 
				//userAccountControl property, and 0 means enable.
				//
				//Alike, val | ADS_UF_ACCOUNTDISABLE equals valu| 0000,0000,0000,0010 which 
				//makes all bits stay the same, second bit becomes 1.
				//This makes diable the user account.

				//convert the accountControl value so that a logical operation can be performed
				//to check of the Disabled option exists.
				int userAccountControl = Convert.ToInt32(de.Properties["userAccountControl"][0]);

				//convert the hex enum to an int
				//66048 means the account is not disabled
				//66050 means the account is disabled
				int userAccountControl_Disabled= Convert.ToInt32(ADS_USER_FLAG.ADS_UF_ACCOUNTDISABLE);

				//create the flag to test
				int flagExists = userAccountControl & userAccountControl_Disabled;

				//if a match is found, then the disabled flag exists within the control flags
				if (flagExists > 0)
				{
					return true;
				}
				else
				{
					return false;
				}

			}
			catch
			{
				return false;
			}
		}		
				
		/// <summary>
		/// This will cast the value as a bool
		/// </summary>
		/// <param name="property"></param>
		/// <returns>bool</returns>
		protected static bool IsAccountExpired(DirectoryEntry de)
		{			
			try
			{

				DateTime accountExpires = SetPropertyToDateTime( GetProperty(de, "accountExpires") );

				if ((accountExpires < DateTime.Today)&&(!accountExpires.Equals(DateTime.MinValue)))
					return true;
				else
					return false;
			}
			catch
			{
				return true;
			}
		}		
        
    
	}
}