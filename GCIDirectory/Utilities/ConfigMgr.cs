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
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Graebel.Common.GCIDirectory.Utilities
{
	/// <summary>
	/// This class allows for custom configuration settings,
	/// instead of the default ConfigurationSettings .NET
	/// class. This allows configuration files to be named
	/// and placed in custom locations. The format
	/// of the XML config file is the same as the
	/// .NET config file (using the "appSettings")
	/// elements. 
	/// 
	/// This class works by passing in a config name,
	/// which needs to correspond to an actual config
	/// file located in a specified directory or
	/// the assembly's application domain directory.
	/// The config name passed in should not end
	/// in ".config", but the actual file must. This class
	/// also supports picking up any external changes made to the file
	/// and reloading the config values. To specify a specific
	/// directory to search for the config file, include
	/// "configurationManager.config.dir" in the default
	/// .NET config file.
	/// </summary>
	public class ConfigMgr
	{
		private const string APP_SETTINGS = "appSettings";
		private const string SECTION_GROUP = "sectionGroup";
		private const string CONFIG_SECTIONS = "configSections";
		private const string SECTION = "section";
		private const string SINGLE_TAG_SECTION_HANDLER = "System.Configuration.SingleTagSectionHandler";
		private const string NAME_VALUE_SECTION_HANDLER = "System.Configuration.NameValueSectionHandler,System";

		private const string CONFIG_FILE_EXTENSION = ".config";
        
		/// <summary>
		/// The key to look for in the .NET configuration settings which
		/// will tell us which directory to look in. If this is not set then
		/// the app domain base directory will be used. 
		/// </summary>
		private const string DEFAULT_CONFIG_DIR_KEY = "gcidirectory.config.dir";
        
		private static ConfigMgr s_instance = null;
        
		/// <summary>
		/// Hashtable of configuration file names
		/// and Collection objects, which has the
		/// config values in memory.
		/// </summary>
		private Hashtable m_configs = new Hashtable();

		/// <summary>
		/// ArrayList of FileWatchHandlers.
		/// </summary>
		private ArrayList m_fileWatchHandlers = new ArrayList();
    
		/// <summary>
		/// Private Constructor that ensures there
		/// is a single instance of this class.
		/// </summary>
		private ConfigMgr()
		{			
		}

		/// <summary>
		/// Returns the lone instance of this class.
		/// </summary>
		/// <returns></returns>
		private static ConfigMgr GetInstance()
		{   
			if (s_instance == null)
			{
				s_instance = new ConfigMgr();
			}
			return s_instance;
		}

		/// <summary>
		/// Obtains the configuration value based on the
		/// configuration name and key name passed in.
		/// </summary>
		/// <param name="config">The name of the configuration file
		/// without the ".config" suffix.</param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetConfigValue(string config,
			string key)
		{            
			return GetInstance().InternalGetConfigValue(config, key);
		}

		/// <summary>
		/// Obtains the configuration collection based on the
		/// configuration name passed in.
		/// </summary>
		/// <param name="config">The name of the configuration file
		/// without the ".config" suffix.</param>
		/// <returns></returns>
		public static NameValueCollection GetConfigCollection(string config,
			string key)
		{            
			return GetInstance().InternalGetConfigCollection(config, key);
		}

		/// <summary>
		/// Obtains the configuration collection based on the
		/// configuration name passed in.
		/// </summary>
		/// <param name="config">The name of the configuration file
		/// without the ".config" suffix.</param>
		/// <returns></returns>
		public NameValueCollection InternalGetConfigCollection(string config,
			string key)
		{            
			NameValueCollection configCollection = null;
			if (config != null && key != null)
			{
				//first check if value is in configs,
				//if not attempt to load using current path
				configCollection = (NameValueCollection)m_configs[key];
				if (configCollection == null)
				{
					LoadConfigFile(config, true);
					configCollection = (NameValueCollection)m_configs[key];                    
				}  
			}
			return configCollection;
		}


		/// <summary>
		/// Does the work of actually getting the config
		/// entry out of the in-memory collection.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public string InternalGetConfigValue(string config,
			string key)
		{
			string rtnValue = null;
			if (config != null && key != null)
			{
				//first check if value is in configs,
				//if not attempt to load using current path
				NameValueCollection configCollection = (NameValueCollection)m_configs[config];
				if (configCollection == null)
				{
					LoadConfigFile(config, true);
					configCollection = (NameValueCollection)m_configs[config];                    
				}
				//get config value                
				if (configCollection != null)
				{
					//todo: should add collection reference here...
					rtnValue = configCollection[key];
				}
			}
			return rtnValue;
		}

		/// <summary>
		/// Static member that is used to reload the config file.
		/// </summary>
		/// <param name="configFile"></param>
		public static void LoadConfigFile(FileInfo configFile)
		{            
			if (configFile != null)
			{
				GetInstance().InternalLoadConfigFile(Path.GetFileNameWithoutExtension(
					configFile.FullName), 
					configFile);                
			}
		}

		/// <summary>
		/// Attempts to find the configuration file and
		/// load the values in memory.
		/// </summary>
		/// <param name="config"></param>
		private void LoadConfigFile(string config,
			bool setWatchHandler)
		{
			string path = ConfigurationSettings.AppSettings[DEFAULT_CONFIG_DIR_KEY];                        
			if (path == null)
			{
				path = AppDomain.CurrentDomain.BaseDirectory;
			}
			FileInfo file = new FileInfo(path + Path.DirectorySeparatorChar + 
				config + CONFIG_FILE_EXTENSION); 
			InternalLoadConfigFile(config, file);
			if (setWatchHandler)
			{
				//set watch handler              
				ArrayList.Synchronized(m_fileWatchHandlers).Add(
					new ConfigFileWatchHandler(file));
			}
		}

		/// <summary>
		/// Attempts to find the configuration file and
		/// load the values in memory.
		/// </summary>
		/// <param name="config"></param>
		private void InternalLoadConfigFile(string config,
			FileInfo configFile)
		{            
			if (configFile != null)
			{
				if (configFile.Exists)
				{
					XmlDocument xml = new XmlDocument();
					FileStream reader = configFile.OpenRead();
					try
					{
						xml.Load(reader);                    
					}
					finally
					{
						reader.Close();
					}
					//add
					LoadAppSettings(config, xml);   
                 
					//nodelist/nodelist
					LoadSectionGroupSettings(xml);

					//nodelist
					LoadSingleTagSettings(xml);
				}              
			}
		}

		/// <summary>
		/// Reads the config file and loads the Hashtable.
		/// </summary>
		/// <param name="dest"></param>
		/// <param name="source"></param>
		private void LoadAppSettings(string config,
			XmlDocument source)
		{                  
			XmlNodeList appSettingsList = source.GetElementsByTagName(APP_SETTINGS);
			if (appSettingsList != null && appSettingsList.Count == 1)
			{
				LoadHashTable(appSettingsList, config);                      
			}
		}

		/// <summary>
		/// Reads the config file and loads the Hashtable.
		/// </summary>
		/// <param name="source"></param>
		private void LoadSectionGroupSettings(XmlDocument source)
		{  
			//get a list of all the custom configSections
			/*			
			<configSections>
				<sectionGroup name="providerRealms">
					...
				</sectionGroup>
				<sectionGroup name="providerRealms">
					...
				</sectionGroup>			
			</configSections>
			*/
			XmlNodeList configSectionList = source.SelectNodes("//"+CONFIG_SECTIONS+"/"+SECTION_GROUP);

			if (configSectionList != null)
			{
				foreach (XmlNode configSection in configSectionList)
				{
					//extract the name in which we will use as part of our table name
					string sSectionGroupName = ((XmlElement)configSection).GetAttribute("name");
				
					//get a list of all the custom configSections
					/*
					<sectionGroup name="providerRealms">
						<section name="graebel" type="System.Configuration.NameValueSectionHandler,System" />
						<section name="dweb" type="System.Configuration.NameValueSectionHandler,System" />
					</sectionGroup>
						
					*/
					XmlNodeList sectionList = configSection.SelectNodes("//"+CONFIG_SECTIONS+"/"+SECTION_GROUP+"/"+SECTION+"[@type='"+NAME_VALUE_SECTION_HANDLER+"']");

					if (sectionList != null)
					{
						//loop through each of the configSections
						foreach (XmlNode section in sectionList)
						{
							//extract the name in which we will use as part of our table name
							string sSection = ((XmlElement)section).GetAttribute("name");

							//build the table for storage
							string hashEntry = sSectionGroupName+"/"+sSection;


							//get a list from the root xml doc based on the settings within the configSection
							XmlNodeList sectionGroup = source.SelectNodes( "//"+sSectionGroupName+"/"+sSection);
							if (sectionGroup != null && sectionGroup.Count == 1)
							{
								LoadHashTable(sectionGroup, hashEntry);
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Reads the config file and loads the Hashtable.
		/// </summary>
		/// <param name="source"></param>
		private void LoadSingleTagSettings(XmlDocument source)
		{                  
			//get a list to all the custom configSections
			/*
			<configSections>
				...
				<section name="NUnitsingleTagSection" type="System.Configuration.SingleTagSectionHandler" />        
			</configSections>
			*/
			XmlNodeList configSectionList = source.SelectNodes("//"+CONFIG_SECTIONS+"/"+SECTION+"[@type='"+SINGLE_TAG_SECTION_HANDLER+"']");
			if (configSectionList != null)
			{
				foreach (XmlNode configSectionItem in configSectionList)
				{
					string sConfigSectionName = ((XmlElement)configSectionItem).GetAttribute("name");

					//select the section node withe attributes
					/*
					<configuration>
						...
						<NUnitsingleTagSection setting1="value1" setting2="value2" setting3="value3" />
					</configuration>
					*/
					XmlNodeList section = source.SelectNodes("//" + sConfigSectionName);
					if (section != null && section.Count == 1)
					{
						XmlDocument doc = new XmlDocument();
						XmlNode root = doc.CreateElement(sConfigSectionName);
						doc.AppendChild(root);

						foreach (XmlAttribute att in section[0].Attributes)
						{
							XmlElement add = (XmlElement)root.AppendChild(doc.CreateElement("add"));
							add.SetAttribute("key", att.Name);
							add.SetAttribute("value", att.Value);
						}				

						LoadHashTable(doc.ChildNodes, sConfigSectionName);                      
					}
				}
			}
		}

		private void LoadHashTable(XmlNodeList appSettingsList, string hashEntry)
		{
			NameValueCollection table = new NameValueCollection();
			XmlNodeList configValueNodes = appSettingsList[0].ChildNodes;
			XmlAttribute xmlKey = null;
			XmlAttribute xmlValue = null;
			foreach (XmlNode node in configValueNodes)
			{                                        
				if (node.NodeType == XmlNodeType.Element)
				{                                                
					if (node.Name.ToLower().Equals("add"))
					{
						xmlKey = node.Attributes["key"];
						xmlValue = node.Attributes["value"];
						if (xmlKey != null && xmlValue != null)
						{
							try
							{
								table.Add(xmlKey.Value,
									xmlValue.Value);
							}
							catch (ArgumentException)
							{
								//don't blow up if we have a duplicate key being added
							}
						}
					}
				}
			} 

			//replace entry in main Hashtable
			Hashtable lockedHash = Hashtable.Synchronized(m_configs);
			lockedHash.Remove(hashEntry);
			lockedHash.Add(hashEntry, table);  
		}

		/// <summary>
		/// Private class used to watch any changes to the config
		/// file and re-load upon such changes.
		/// </summary>
		private class ConfigFileWatchHandler
		{
			/// <summary>
			/// Hold the FileInfo that contains the configuration entries.
			/// </summary>
			private FileInfo m_configFile;

			/// <summary>
			/// Construct a new watch handler.
			/// </summary>
			/// <param name="hierarchy">the hierarchy to configure</param>
			/// <param name="configFile">the config file to watch</param>
			internal ConfigFileWatchHandler(FileInfo configFile)
			{
				m_configFile = configFile;

				// Create a new FileSystemWatcher and set its properties.
				FileSystemWatcher watcher = new FileSystemWatcher();

				watcher.Path = m_configFile.DirectoryName;
				watcher.Filter = m_configFile.Name;

				// Set the notification filters
				watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite | NotifyFilters.FileName;

				// Add event handlers.
				watcher.Changed += new FileSystemEventHandler(ConfigureAndWatchHandler_OnChanged);
				watcher.Created += new FileSystemEventHandler(ConfigureAndWatchHandler_OnChanged);
				watcher.Deleted += new FileSystemEventHandler(ConfigureAndWatchHandler_OnChanged);

				// Begin watching.
				watcher.EnableRaisingEvents = true;
			}

			/// <summary>
			/// Event handler used by <see cref="ConfigureAndWatchHandler"/>.
			/// </summary>
			/// <param name="source">the <see cref="FileSystemWatcher"/> firing the event</param>
			/// <param name="e">the argument indicates the file that caused the event to be fired</param>
			/// <remarks>
			/// This handler reloads the configuration from the file when the event is fired.
			/// </remarks>
			private void ConfigureAndWatchHandler_OnChanged(object source, FileSystemEventArgs e)
			{
				// Sleep here to allow the writer to complete
				System.Threading.Thread.Sleep(500);
				ConfigMgr.LoadConfigFile(m_configFile);
			}            
		}        
	}
}
