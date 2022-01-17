using System;
using System.Runtime.Serialization;

namespace Graebel.Common.GCIDirectory.Entities
{
	/// <summary>
	/// Simple entity object for read-only views of data.
	/// </summary>
	[Serializable()]
    public class User
	{         
		        		
		private string m_nativeGuid = string.Empty;				
		private bool m_isNativeGuidNull;		
				
		private Guid m_guid;
		private bool m_isGuidNull;		
				
		private DateTime m_accountExpires;				
		private bool m_isAccountExpiresNull;		
				
		private short m_badPwdCount;				
		private bool m_isBadPwdCountNull;		
				
		private string m_cn = string.Empty;				
		private bool m_isCnNull;		
				
		private string m_description = string.Empty;				
		private bool m_isDescriptionNull;		
				
		private string m_givenName = string.Empty;				
		private bool m_isGivenNameNull;		
				
		private DateTime m_lockoutTime;				
		private bool m_isLockoutTimeNull;		
				
		private string m_distinguishedName = string.Empty;				
		private bool m_isDistinguishedNameNull;		
				
		private string m_objectCategory = string.Empty;				
		private bool m_isObjectCategoryNull;		
				
		private DateTime m_pwdLastSet;				
		private bool m_isPwdLastSetNull;		
				
		private string m_name = string.Empty;				
		private bool m_isNameNull;		
				
		private string m_samAccountName = string.Empty;				
		private bool m_isSamAccountNameNull;		
				
		private int m_samAccountType;				
		private bool m_isSamAccountTypeNull;		
				
		private string m_sn = string.Empty;				
		private bool m_isSnNull;		
				
		private string m_userPrincipalName = string.Empty;				
		private bool m_isUserPrincipalNameNull;		
				
		private DateTime m_whenChanged;				
		private bool m_isWhenChangedNull;		
				
		private DateTime m_whenCreated;				
		private bool m_isWhenCreatedNull;		
				
		private bool m_accountDisabled;				
		private bool m_isAccountDisabledNull;		
				
		private bool m_passwordExpired;				
		private bool m_isPasswordExpiredNull;		
				
		private bool m_accountLockedOut;				
		private bool m_isAccountLockedOutNull;		

		private string m_firstName;
		
		private string m_lastName;
		
		private string m_displayName;
		
		private Groups m_groups = null;
           
		/// <summary>
		/// get or set the first name
		/// </summary>
		public string FirstName
		{
			get
			{
				return m_firstName;
			}
			set
			{
				m_firstName = value;
			}
		}

		/// <summary>
		/// get or set the last name
		/// </summary>
		public string LastName
		{
			get
			{
				return m_lastName;
			}
			set
			{
				m_lastName = value;
			}
		}

		/// <summary>
		/// get or set the display name
		/// </summary>
		public string DisplayName
		{
			get
			{
				return m_displayName;
			}
			set
			{
				m_displayName = value;
			}
		}
			


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
        /// Gets or sets the AccountExpires.
        /// </summary>
        public DateTime AccountExpires
        {
            get
            {
                return m_accountExpires;
            }
            set
            {
                m_accountExpires = value;
            }
        }
                
        /// <summary>
        /// Gets or sets the AccountExpires as a string.
        /// </summary>
        public string AccountExpiresString
        {
            get
            {
                string rtnValue = string.Empty;
                if (!m_isAccountExpiresNull)
                {
                    rtnValue = m_accountExpires.ToString();
                }
                return rtnValue;            
            }           
        }
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsAccountExpiresNull()
        {
            bool rtnValue = false;
            if (m_isAccountExpiresNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetAccountExpiresNull()
        {            
            m_isAccountExpiresNull = true;           
        }
                
        /// <summary>
        /// Gets or sets the BadPwdCount.
        /// </summary>
        public short BadPwdCount
        {
            get
            {
                return m_badPwdCount;
            }
            set
            {
                m_badPwdCount = value;
            }
        }
                
        /// <summary>
        /// Gets or sets the BadPwdCount as a string.
        /// </summary>
        public string BadPwdCountString
        {
            get
            {
                string rtnValue = string.Empty;
                if (!m_isBadPwdCountNull)
                {
                    rtnValue = m_badPwdCount.ToString();
                }
                return rtnValue;            
            }           
        }
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsBadPwdCountNull()
        {
            bool rtnValue = false;
            if (m_isBadPwdCountNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetBadPwdCountNull()
        {            
            m_isBadPwdCountNull = true;           
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
        /// Gets or sets the GivenName.
        /// </summary>
        public string GivenName
        {
            get
            {
                return m_givenName;
            }
            set
            {
                m_givenName = value;
            }
        }
        
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsGivenNameNull()
        {
            bool rtnValue = false;
            if (m_isGivenNameNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetGivenNameNull()
        {            
            m_isGivenNameNull = true;           
        }
                
        /// <summary>
        /// Gets or sets the LockoutTime.
        /// </summary>
        public DateTime LockoutTime
        {
            get
            {
                return m_lockoutTime;
            }
            set
            {
                m_lockoutTime = value;
            }
        }
                
        /// <summary>
        /// Gets or sets the LockoutTime as a string.
        /// </summary>
        public string LockoutTimeString
        {
            get
            {
                string rtnValue = string.Empty;
                if (!m_isLockoutTimeNull)
                {
                    rtnValue = m_lockoutTime.ToString();
                }
                return rtnValue;            
            }           
        }
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsLockoutTimeNull()
        {
            bool rtnValue = false;
            if (m_isLockoutTimeNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetLockoutTimeNull()
        {            
            m_isLockoutTimeNull = true;           
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
        /// Gets or sets the PwdLastSet.
        /// </summary>
        public DateTime PwdLastSet
        {
            get
            {
                return m_pwdLastSet;
            }
            set
            {
                m_pwdLastSet = value;
            }
        }
                
        /// <summary>
        /// Gets or sets the PwdLastSet as a string.
        /// </summary>
        public string PwdLastSetString
        {
            get
            {
                string rtnValue = string.Empty;
                if (!m_isPwdLastSetNull)
                {
                    rtnValue = m_pwdLastSet.ToString();
                }
                return rtnValue;            
            }           
        }
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsPwdLastSetNull()
        {
            bool rtnValue = false;
            if (m_isPwdLastSetNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetPwdLastSetNull()
        {            
            m_isPwdLastSetNull = true;           
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
        /// Gets or sets the Sn.
        /// </summary>
        public string Sn
        {
            get
            {
                return m_sn;
            }
            set
            {
                m_sn = value;
            }
        }


		public bool IsDisplayNameNull()
		{
			bool rtnValue = false;
			if (m_displayName == null)
			{
				rtnValue = true;
			}
			return rtnValue;
		}

		public void SetDisplayNameNull()
		{
			m_displayName = null;
		}

		public bool IsFirstNameNull()
		{
			bool rtnValue = false;
			if (m_firstName == null)
			{
				rtnValue = true;
			}
			return rtnValue;
		}

		public void SetLastNameNull()
		{
			m_lastName = null;
		}

		public bool IsLastNameNull()
		{
			bool rtnValue = false;
			if (m_lastName == null)
			{
				rtnValue = true;
			}
			return rtnValue;
		}

		public void SetFirstNameNull()
		{
			m_firstName = null;
		}


        
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsSnNull()
        {
            bool rtnValue = false;
            if (m_isSnNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetSnNull()
        {            
            m_isSnNull = true;           
        }
                
        /// <summary>
        /// Gets or sets the UserPrincipalName.
        /// </summary>
        public string UserPrincipalName
        {
            get
            {
                return m_userPrincipalName;
            }
            set
            {
                m_userPrincipalName = value;
            }
        }
        
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsUserPrincipalNameNull()
        {
            bool rtnValue = false;
            if (m_isUserPrincipalNameNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetUserPrincipalNameNull()
        {            
            m_isUserPrincipalNameNull = true;           
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
                
        /// <summary>
        /// Gets or sets the AccountDisabled.
        /// </summary>
        public bool AccountDisabled
        {
            get
            {
                return m_accountDisabled;
            }
            set
            {
                m_accountDisabled = value;
            }
        }
                
        /// <summary>
        /// Gets or sets the AccountDisabled as a string.
        /// </summary>
        public string AccountDisabledString
        {
            get
            {
                string rtnValue = string.Empty;
                if (!m_isAccountDisabledNull)
                {
                    rtnValue = m_accountDisabled.ToString();
                }
                return rtnValue;            
            }           
        }
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsAccountDisabledNull()
        {
            bool rtnValue = false;
            if (m_isAccountDisabledNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetAccountDisabledNull()
        {            
            m_isAccountDisabledNull = true;           
        }
                
        /// <summary>
        /// Gets or sets the PasswordExpired.
        /// </summary>
        public bool PasswordExpired
        {
            get
            {
                return m_passwordExpired;
            }
            set
            {
                m_passwordExpired = value;
            }
        }
                
        /// <summary>
        /// Gets or sets the PasswordExpired as a string.
        /// </summary>
        public string PasswordExpiredString
        {
            get
            {
                string rtnValue = string.Empty;
                if (!m_isPasswordExpiredNull)
                {
                    rtnValue = m_passwordExpired.ToString();
                }
                return rtnValue;            
            }           
        }
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsPasswordExpiredNull()
        {
            bool rtnValue = false;
            if (m_isPasswordExpiredNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetPasswordExpiredNull()
        {            
            m_isPasswordExpiredNull = true;           
        }
                
        /// <summary>
        /// Gets or sets the AccountLockedOut.
        /// </summary>
        public bool AccountLockedOut
        {
            get
            {
                return m_accountLockedOut;
            }
            set
            {
                m_accountLockedOut = value;
            }
        }
                
        /// <summary>
        /// Gets or sets the AccountLockedOut as a string.
        /// </summary>
        public string AccountLockedOutString
        {
            get
            {
                string rtnValue = string.Empty;
                if (!m_isAccountLockedOutNull)
                {
                    rtnValue = m_accountLockedOut.ToString();
                }
                return rtnValue;            
            }           
        }
        /// <summary>
        /// Returns true if the value is null.
        /// </summary>
        public bool IsAccountLockedOutNull()
        {
            bool rtnValue = false;
            if (m_isAccountLockedOutNull)
            {
                rtnValue = true;
            }
            return rtnValue;
        }

        /// <summary>
        /// Sets the value to null.
        /// </summary>
        public void SetAccountLockedOutNull()
        {            
            m_isAccountLockedOutNull = true;           
        }

		/// <summary>
		/// hold a collection of Groups the user belongs to.
		/// </summary>
		public Groups Groups
		{            
			 get
            {
                return m_groups;
            }
            set
            {
                m_groups = value;
            }         
		}
          		
	}
}
