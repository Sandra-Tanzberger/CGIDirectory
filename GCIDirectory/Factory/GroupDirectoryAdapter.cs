using System;

namespace Graebel.Common.GCIDirectory.Factory
{
	/// <summary>
	/// Summary description for IGroupDirectoryAdapter.
	/// </summary>
	public class GroupDirectoryAdapter
	{
		internal static IDirectoryAdapters.IGroupDirectoryAdapter Create(string provider)
		{		
			IDirectoryAdapters.IGroupDirectoryAdapter iGroupDirectoryAdapter = null;
			switch(provider)       
			{         
				case "AdsiAdapter": //Configuration.PROVIDER_NAMESPACE_ADSI:   
					iGroupDirectoryAdapter  = new AdsiAdapters.GroupDirectoryAdapter();
					break;                  
				case "NtAdapter": //Configuration.PROVIDER_NAMESPACE_ADSI:   
					iGroupDirectoryAdapter = new NtAdapters.GroupDirectoryAdapter();					
					break;      
				case "RpAdapter": //Configuration.PROVIDER_NAMESPACE_ADSI:   
					iGroupDirectoryAdapter = new RpAdapters.GroupDirectoryAdapter();					
					break;      
			}
 
			return iGroupDirectoryAdapter;
		}		
	}
}
