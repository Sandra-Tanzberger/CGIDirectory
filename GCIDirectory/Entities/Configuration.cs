using System;
using System.Collections.Specialized;
using System.DirectoryServices;
using System.Runtime.Serialization;

using Graebel.Common.GCIDirectory.Utilities;

namespace Graebel.Common.GCIDirectory.Entities
{
	/// <summary>
	/// Simple entity object for read-only views of data.
	/// </summary>
	[Serializable()]
    public class Configuration
	{         
		#region Internal Constants
        
		//namespace of the direcory accessor implementation classes
		internal static string PROVIDER_NAMESPACE_ADSI = "AdsiAdapter";
		internal static string PROVIDER_NAMESPACE_NT = "NtAdapter";
		private static string CONFIG_SECTION_GROUP = "providerRealms/";

		//domain names of supported domains
		internal static string DOMAIN_GRAEBEL = "domain";
		internal static string DOMAIN_GWEB = "gweb";
		internal static string DOMAIN_DWEB = "dweb";
		internal static string DOMAIN_DOMAIN = "domain";
		internal static string DOMAIN_GMII = "gmii";

		//error codes used to throw application exceptions
		internal static int COM_EXCEPTION_UNKNOWN_USER_BAD_PASSWORD = -2147023570;
		internal static int COM_EXCEPTION_INVALID_NEW_PASSWORD = -2147022651;

		#endregion

		#region Member Variables

		private string m_AdServer;				
		private string m_AdPath;					
		private string m_AdAdminUsername = string.Empty;						
		private string m_AdAdminPassword = string.Empty;	
		private string m_DirectoryProvider = string.Empty;
		private string m_Domain = string.Empty;	
		//private string m_AuthenticationTypeStr = string.Empty;
		//private AuthenticationTypes m_AuthenticationType;
		private string m_ProviderNamespace = string.Empty;
		
		#endregion

		internal Configuration(string domain)
		{
			try
			{
				//set this class up to look like the appropriate domain
				m_Domain = domain.ToLower();

				NameValueCollection configInfo = (NameValueCollection)ConfigUtil.GetConfigCollection(CONFIG_SECTION_GROUP+m_Domain);
				m_AdServer = configInfo.Get("Server");
				m_AdPath = configInfo.Get("Path");
				m_AdAdminUsername =  configInfo.Get("AdminUsername");
				m_AdAdminPassword =  configInfo.Get("AdminPassword");
				m_DirectoryProvider = configInfo.Get("DirectoryProvider");
				m_ProviderNamespace = configInfo.Get("ProviderNamespace");
			}
			catch (Exception ex)
			{
				throw(ex);
			}
		}

        /// <summary>
        /// Gets or sets the AdServer.
        /// </summary>
        internal string AdServer
        {
            get
            {
                return GetAdServer();
            }
        }

		/// <summary>
		/// Gets or sets the AdPath.
		/// </summary>
		internal string AdPath
		{
			get
			{
				return this.m_AdPath;
			}
		}
        
		/// <summary>
		/// Gets or sets the AdAdminUsername.
		/// </summary>
		internal string AdAdminUsername
		{
			get
			{
				return this.m_AdAdminUsername;
			}
		}

		/// <summary>
		/// Gets or sets the AdAdminPassword.
		/// </summary>
		internal string AdAdminPassword
		{
			get
			{
				return this.m_AdAdminPassword;
			}
		}

		/// <summary>
		/// Gets or sets the Domain.
		/// </summary>
		internal string Domain
		{
			get
			{
				return this.m_Domain;
			}
		}

//		/// <summary>
//		/// Gets or sets the Domain.
//		/// </summary>
//		internal AuthenticationTypes AuthenticationType
//		{
//			get
//			{
//				if (m_AuthenticationTypeStr.ToLower().Equals("authenticationtypes.secure"))
//					this.m_AuthenticationType = AuthenticationTypes.Secure;
//				else 
//					this.m_AuthenticationType = AuthenticationTypes.None;
//
//				return this.m_AuthenticationType;
//			}
//		}		

		/// <summary>
		/// Gets or sets the Directory Provider.
		/// </summary>
		internal string ProviderNamespace
		{
			get
			{
				return this.m_ProviderNamespace;
			}
		}

		/// <summary>
		/// Verifies the AD Server can be connected to.  If the connection fails
		/// return the generic path
		/// 
		/// LDAP://grizzley.gds.graebel.com
		/// LDAP://gds.graebel.com
		/// 
		/// </summary>
		private string GetAdServer()
		{
			try
			{
//				DirectoryEntry directoryEntry = new DirectoryEntry(this.m_AdServer, this.m_AdAdminUsername, this.m_AdAdminPassword, this.AuthenticationType);
//				DirectorySearcher search = new DirectorySearcher(directoryEntry);
//				search.Filter = "(&(SAMAccountName="+this.m_AdAdminUsername+")(objectcategory=person))";
//				SearchResult result = search.FindOne();			
				return this.m_AdServer;
			}
			catch
			{
				return this.m_AdPath;
			}
		}

	}
}
