using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;

namespace Graebel.Common.GCIDirectory.Utilities
{
	/// <summary>
	/// Summary description for SidWrapper.
	/// </summary>
	public class SidWrapper
	{
		[DllImport("advapi32", CharSet=CharSet.Auto, SetLastError=true)]
		static extern bool  LookupAccountSid
			(
			string lpSystemName,		// name of local or remote computer
			IntPtr pSid,                // security identifier
			StringBuilder Account,      // account name buffer
			ref int cbName,             // size of account name buffer
			StringBuilder  DomainName,	// domain name
			ref int cbDomainName,		// size of domain name buffer
			ref int peUse				// SID type
			);

		[DllImport("advapi32", CharSet=CharSet.Auto)]
		private static extern int ConvertSidToStringSid(
			IntPtr pSID, ref IntPtr pStringSid);
		
		[DllImport("advapi32", CharSet=CharSet.Auto)]
		private static extern bool ConvertStringSidToSid(
			string pStringSid, ref IntPtr pSID);

		[DllImport("kernel32", CharSet=CharSet.Auto)]
		private static extern IntPtr  LocalFree( IntPtr hMem);

		public static void ConvertSid(string sid, ref string userName, ref string domain) 
		{
			string sAccount = string.Empty;

			IntPtr pSid = IntPtr.Zero;
			// Change Sid into something meaningfull on your machine
			bool success = ConvertStringSidToSid(sid, ref pSid);
			if (success)
			{				
				DumpAccountFromSid(pSid, ref userName, ref domain);							
			}
			// free pSid, ConvertStringSidToSid allocates the buffer, the caller has to free the buffer when done !!!!!
			LocalFree( pSid );
		}

		private static void DumpAccountFromSid(IntPtr SID, ref string userName, ref string domain)
		{
			const int ERROR_NO_MORE_ITEMS = 259;
			int cchAccount = 0;
			int cchDomain = 0;
			int snu = 0 ;

			// Caller allocated buffer
			StringBuilder Account=  null;
			StringBuilder Domain = null;
			// Lookup account name on local system
			// First call to get required buffer sizes
			bool ret = LookupAccountSid(null, SID,  Account, ref cchAccount, Domain, ref cchDomain, ref snu);
			if ( ret == true )
				if ( Marshal.GetLastWin32Error() == ERROR_NO_MORE_ITEMS )
				{
					throw new Exception("Win32 Error DumpAccountFromSid: No More Items");
				}
			Account = new StringBuilder( cchAccount );
			Domain = new StringBuilder( cchDomain );
			ret = LookupAccountSid(null, SID,  Account, ref cchAccount,  Domain, ref cchDomain, ref snu);
			if (ret)
			{
				domain = Domain.ToString();
				userName = Account.ToString();
			}
			else
			{
				throw new Exception("Win32 Error in DumpAccountFromSid:" + Marshal.GetLastWin32Error());
			}

		}

				
		/// <summary>
		/// Takes a byte[] sid obtained out of an DirectoryEntry with "objectSID" and
		/// converts it to the string SID, such as "S-12-234 etc".
		/// </summary>
		/// <remarks>From code sample posted at http://groups.google.com/groups?q=ad+get+sid+bind+string+c%23&hl=en&lr=&ie=UTF-8&selm=ebNhf0jmCHA.1324%40tkmsftngp04&rnum=1</remarks>
		/// <param name="sid">Byte array SID</param>
		/// <returns>String version of SID.</returns>
		public static string ConvertSidToStringSid(byte[] sid)
		{
			string sidString = string.Empty;
			IntPtr sidPtr = IntPtr.Zero;
			IntPtr sidStringPtr = IntPtr.Zero;			
			
			sidPtr = Marshal.AllocHGlobal(sid.Length);
			try
			{
				Marshal.Copy(sid, 0, sidPtr, sid.Length);			
				int res = ConvertSidToStringSid(sidPtr, ref sidStringPtr);
				if (res == 0)
				{
					throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
				}
				sidString = Marshal.PtrToStringAuto(sidStringPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(sidPtr);
				Marshal.FreeHGlobal(sidStringPtr);
			}
			return sidString;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userDirectoryEntry"></param>
		/// <returns></returns>
		private string ConvertSidToOctetString(byte[] sid)
		{			
			StringBuilder octetSid = new StringBuilder();			
			for(uint i = 0; i < sid.Length; i++)
			{
				octetSid.AppendFormat("\\{0:x2}",sid[i]);
			}
			return octetSid.ToString();			
		}

	}
}
