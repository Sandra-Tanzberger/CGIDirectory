using System;
using System.Runtime.Serialization;

namespace Graebel.Common.GCIDirectory.Entities
{
	/// <summary>
	/// Simple entity object for read-only views of data.
	/// </summary>
	[Serializable()]
    public class Group
	{         
        #region Norca Auto-Generated Code
		
		        		
		private string m_nativeGuid = string.Empty;				
		private bool m_isNativeGuidNull;		
				
		private Guid m_guid = Guid.Empty;				
		private bool m_isGuidNull;		
				
		private string m_cn = string.Empty;				
		private bool m_isCnNull;		
				
		private string m_description = string.Empty;				
		private bool m_isDescriptionNull;		
				
		private int m_groupType;				
		private bool m_isGroupTypeNull;		
				
		private string m_distinguishedName = string.Empty;				
		private bool m_isDistinguishedNameNull;		
				
		private string m_objectCategory = string.Empty;				
		private bool m_isObjectCategoryNull;		
				
		private string m_name = string.Empty;				
		private bool m_isNameNull;		
				
		private string m_samAccountName = string.Empty;				
		private bool m_isSamAccountNameNull;		
				
		private int m_samAccountType;				
		private bool m_isSamAccountTypeNull;		
				
		private DateTime m_whenChanged;				
		private bool m_isWhenChangedNull;		
				
		private DateTime m_whenCreated;				
		private bool m_isWhenCreatedNull;		
		
                
        /// <summary>
        /// Gets or sets the NativeGuid.
        /// </summary>
        public string NativeGuid
        {
            get
            {
                return m_nativeGuid;
            }
            set
            {
                m_nativeGuid = value;
            }
        }
        
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsNativeGuidNull()
        {
            bool rtnValue = false;
            if (m_isNativeGuidNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetNativeGuidNull()
        {            
            m_isNativeGuidNull = true;           
        }
                
        /// <summary>
        /// Gets or sets the Guid.
        /// </summary>
        public Guid Guid
        {
            get
            {
                return m_guid;
            }
            set
            {
                m_guid = value;
            }
        }
        
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsGuidNull()
        {
            bool rtnValue = false;
            if (m_isGuidNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetGuidNull()
        {            
            m_isGuidNull = true;           
        }
                
        /// <summary>
        /// Gets or sets the Cn.
        /// </summary>
        public string Cn
        {
            get
            {
                return m_cn;
            }
            set
            {
                m_cn = value;
            }
        }
        
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsCnNull()
        {
            bool rtnValue = false;
            if (m_isCnNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetCnNull()
        {            
            m_isCnNull = true;           
        }
                
        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description
        {
            get
            {
                return m_description;
            }
            set
            {
                m_description = value;
            }
        }
        
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsDescriptionNull()
        {
            bool rtnValue = false;
            if (m_isDescriptionNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetDescriptionNull()
        {            
            m_isDescriptionNull = true;           
        }
                
        /// <summary>
        /// Gets or sets the GroupType.
        /// </summary>
        public int GroupType
        {
            get
            {
                return m_groupType;
            }
            set
            {
                m_groupType = value;
            }
        }
                
        /// <summary>
        /// Gets or sets the GroupType as a string.
        /// </summary>
        public string GroupTypeString
        {
            get
            {
                string rtnValue = string.Empty;
                if (!m_isGroupTypeNull)
                {
                    rtnValue = m_groupType.ToString();
                }
                return rtnValue;            
            }           
        }
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsGroupTypeNull()
        {
            bool rtnValue = false;
            if (m_isGroupTypeNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetGroupTypeNull()
        {            
            m_isGroupTypeNull = true;           
        }
                
        /// <summary>
        /// Gets or sets the DistinguishedName.
        /// </summary>
        public string DistinguishedName
        {
            get
            {
                return m_distinguishedName;
            }
            set
            {
                m_distinguishedName = value;
            }
        }
        
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsDistinguishedNameNull()
        {
            bool rtnValue = false;
            if (m_isDistinguishedNameNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetDistinguishedNameNull()
        {            
            m_isDistinguishedNameNull = true;           
        }
                
        /// <summary>
        /// Gets or sets the ObjectCategory.
        /// </summary>
        public string ObjectCategory
        {
            get
            {
                return m_objectCategory;
            }
            set
            {
                m_objectCategory = value;
            }
        }
        
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsObjectCategoryNull()
        {
            bool rtnValue = false;
            if (m_isObjectCategoryNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetObjectCategoryNull()
        {            
            m_isObjectCategoryNull = true;           
        }
                
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }
        
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsNameNull()
        {
            bool rtnValue = false;
            if (m_isNameNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetNameNull()
        {            
            m_isNameNull = true;           
        }
                
        /// <summary>
        /// Gets or sets the SamAccountName.
        /// </summary>
        public string SamAccountName
        {
            get
            {
                return m_samAccountName;
            }
            set
            {
                m_samAccountName = value;
            }
        }
        
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsSamAccountNameNull()
        {
            bool rtnValue = false;
            if (m_isSamAccountNameNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetSamAccountNameNull()
        {            
            m_isSamAccountNameNull = true;           
        }
                
        /// <summary>
        /// Gets or sets the SamAccountType.
        /// </summary>
        public int SamAccountType
        {
            get
            {
                return m_samAccountType;
            }
            set
            {
                m_samAccountType = value;
            }
        }
                
        /// <summary>
        /// Gets or sets the SamAccountType as a string.
        /// </summary>
        public string SamAccountTypeString
        {
            get
            {
                string rtnValue = string.Empty;
                if (!m_isSamAccountTypeNull)
                {
                    rtnValue = m_samAccountType.ToString();
                }
                return rtnValue;            
            }           
        }
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsSamAccountTypeNull()
        {
            bool rtnValue = false;
            if (m_isSamAccountTypeNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetSamAccountTypeNull()
        {            
            m_isSamAccountTypeNull = true;           
        }
                
        /// <summary>
        /// Gets or sets the WhenChanged.
        /// </summary>
        public DateTime WhenChanged
        {
            get
            {
                return m_whenChanged;
            }
            set
            {
                m_whenChanged = value;
            }
        }
                
        /// <summary>
        /// Gets or sets the WhenChanged as a string.
        /// </summary>
        public string WhenChangedString
        {
            get
            {
                string rtnValue = string.Empty;
                if (!m_isWhenChangedNull)
                {
                    rtnValue = m_whenChanged.ToString();
                }
                return rtnValue;            
            }           
        }
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsWhenChangedNull()
        {
            bool rtnValue = false;
            if (m_isWhenChangedNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetWhenChangedNull()
        {            
            m_isWhenChangedNull = true;           
        }
                
        /// <summary>
        /// Gets or sets the WhenCreated.
        /// </summary>
        public DateTime WhenCreated
        {
            get
            {
                return m_whenCreated;
            }
            set
            {
                m_whenCreated = value;
            }
        }
                
        /// <summary>
        /// Gets or sets the WhenCreated as a string.
        /// </summary>
        public string WhenCreatedString
        {
            get
            {
                string rtnValue = string.Empty;
                if (!m_isWhenCreatedNull)
                {
                    rtnValue = m_whenCreated.ToString();
                }
                return rtnValue;            
            }           
        }
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsWhenCreatedNull()
        {
            bool rtnValue = false;
            if (m_isWhenCreatedNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetWhenCreatedNull()
        {            
            m_isWhenCreatedNull = true;           
        }
          		
		#endregion
	}
}
