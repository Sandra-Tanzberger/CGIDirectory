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
//	Reverse Engineering of this code is strictly prohibited.
// 
#endregion

using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;



namespace Graebel.Common.GCIDirectory.Utilities
{
	/// <summary>
	/// Utility class used to obtain configuration settings. Use this
	/// class to obtain config values from the default .NET config
	/// file or a custom config file.
	/// </summary>
	public class ConfigUtil
	{
		    
		/// <summary>
		/// Default name of the config file for business component configurations.
		/// </summary>
		public const string BUS_CONFIG_FILE = "gcidirectory";

		/// <summary>
		/// Obtains the value in the business object configuration file based
		/// on the key passed in. This searches for a config
		/// file named "businessComponents.config" in either the
		/// app base directory or in the directory specified by
		/// the "configurationManager.config.dir" setting in 
		/// the .NET config file.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetBusConfigValue(string key)
		{
			string rtnValue = ConfigMgr.GetConfigValue(
				BUS_CONFIG_FILE,
				key);            
			if (rtnValue == null)
			{
				throw new Exception("Value not found with key " + key +
					" in config file: " + BUS_CONFIG_FILE);
			}
			return rtnValue;
		}

		/// <summary>
		/// Obtains the value in the configuration file based
		/// on the key passed in. This does a 
		/// check to ensure the value is not null. Searches
		/// the default .NET configuration file.
		/// section groups = "mySectionGroup/mySection"
		/// singleTag = "mySection"
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static NameValueCollection GetConfigCollection(string key)
		{
			string path = (string)ConfigurationSettings.AppSettings["gcidirectory.config.dir"];

			NameValueCollection configCollection = ConfigMgr.GetConfigCollection(
				BUS_CONFIG_FILE,
				key);
			if (configCollection == null)
			{
				throw new Exception("Value not found with key " + key +
					" in config file: " + BUS_CONFIG_FILE);
			}

			return configCollection;
		}
	}
}
