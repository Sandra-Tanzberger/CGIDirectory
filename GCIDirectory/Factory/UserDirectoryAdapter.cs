using System;
using Graebel.Common.GCIDirectory.Entities;

namespace Graebel.Common.GCIDirectory.Factory
{
	/// <summary>
	/// Summary description for UserDirectoryAdapter.
	/// </summary>
	public class UserDirectoryAdapter
	{
		internal static IDirectoryAdapters.IUserDirectoryAdapter Create(string provider)
		{		
			IDirectoryAdapters.IUserDirectoryAdapter iuserDirectoryAdapter = null;
			switch(provider)       
			{         
				case "AdsiAdapter": //Configuration.PROVIDER_NAMESPACE_ADSI:   
					iuserDirectoryAdapter  = new AdsiAdapters.UserDirectoryAdapter();
					break;                  
				case "NtAdapter": //Configuration.PROVIDER_NAMESPACE_ADSI:   
					iuserDirectoryAdapter = new NtAdapters.UserDirectoryAdapter();					
					break;      
				case "RpAdapter": //Configuration.PROVIDER_NAMESPACE_ADSI:   
					iuserDirectoryAdapter = new RpAdapters.UserDirectoryAdapter();					
					break;      
			}
 
			return iuserDirectoryAdapter;
		}	
	}
}
