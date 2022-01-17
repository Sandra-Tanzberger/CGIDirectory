using System;
using System.Collections;
using System.Runtime.Serialization;
//using Graebel.Common.Components.BusinessEntities;

namespace Graebel.Common.GCIDirectory.Entities
{
	/// <summary>
	/// Collection of UserSimpleEntities.
	/// </summary>
	[Serializable()]
	public class Users : CollectionBase
	{
		
        /// <summary>
        /// Adds a <c>User</c> to the collection.
        /// </summary>
        /// <param name="User">
        /// The <c>User</c> to add to the collection.
        /// </param>
        /// <returns>void</returns>
        public virtual void Add(User User)
        {
            this.List.Add(User);        
        }
    
        /// <summary>
        /// Indexer for the class, return the <c>User</c> object 
        /// at the specified index.
        /// </summary>
        public virtual User this[int index]
        {
            get 
            { 
                return (User) this.List[index];  
            }
            set
            {
                this.List.Insert(index, value);
            }
        }        
                
        /// <summary>
        /// Filters this collection by the NativeGuid.
        /// </summary>
        /// <param name="nativeGuid"></param>
        /// <returns></returns>
        public Users FilterByNativeGuid(string nativeGuid)
        {
            Users rtnValue = new Users();
            foreach (User entity in this)
            {
                if (entity.NativeGuid == nativeGuid)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by NativeGuid in ascending order.
        /// </summary>
        public void SortByNativeGuidAsc()
        {
            NativeGuidComparer comparer = new NativeGuidComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by NativeGuid in descending order.
        /// </summary>
        public void SortByNativeGuidDesc()
        {
            NativeGuidComparer comparer = new NativeGuidComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by NativeGuid.
        /// </summary>
        class NativeGuidComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((User)obj1).NativeGuid.CompareTo(((User)obj2).NativeGuid);
                }
                else
                {
                    return ((User)obj2).NativeGuid.CompareTo(((User)obj1).NativeGuid);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the Guid.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Users FilterByGuid(Guid guid)
        {
            Users rtnValue = new Users();
            foreach (User entity in this)
            {
                if (entity.Guid == guid)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by Guid in ascending order.
        /// </summary>
        public void SortByGuidAsc()
        {
            GuidComparer comparer = new GuidComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by Guid in descending order.
        /// </summary>
        public void SortByGuidDesc()
        {
            GuidComparer comparer = new GuidComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by Guid.
        /// </summary>
        class GuidComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((User)obj1).Guid.CompareTo(((User)obj2).Guid);
                }
                else
                {
                    return ((User)obj2).Guid.CompareTo(((User)obj1).Guid);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the AccountExpires.
        /// </summary>
        /// <param name="accountExpires"></param>
        /// <returns></returns>
        public Users FilterByAccountExpires(DateTime accountExpires)
        {
            Users rtnValue = new Users();
            foreach (User entity in this)
            {
                if (entity.AccountExpires == accountExpires)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by AccountExpires in ascending order.
        /// </summary>
        public void SortByAccountExpiresAsc()
        {
            AccountExpiresComparer comparer = new AccountExpiresComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by AccountExpires in descending order.
        /// </summary>
        public void SortByAccountExpiresDesc()
        {
            AccountExpiresComparer comparer = new AccountExpiresComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by AccountExpires.
        /// </summary>
        class AccountExpiresComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((User)obj1).AccountExpires.CompareTo(((User)obj2).AccountExpires);
                }
                else
                {
                    return ((User)obj2).AccountExpires.CompareTo(((User)obj1).AccountExpires);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the BadPwdCount.
        /// </summary>
        /// <param name="badPwdCount"></param>
        /// <returns></returns>
        public Users FilterByBadPwdCount(short badPwdCount)
        {
            Users rtnValue = new Users();
            foreach (User entity in this)
            {
                if (entity.BadPwdCount == badPwdCount)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by BadPwdCount in ascending order.
        /// </summary>
        public void SortByBadPwdCountAsc()
        {
            BadPwdCountComparer comparer = new BadPwdCountComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by BadPwdCount in descending order.
        /// </summary>
        public void SortByBadPwdCountDesc()
        {
            BadPwdCountComparer comparer = new BadPwdCountComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by BadPwdCount.
        /// </summary>
        class BadPwdCountComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((User)obj1).BadPwdCount.CompareTo(((User)obj2).BadPwdCount);
                }
                else
                {
                    return ((User)obj2).BadPwdCount.CompareTo(((User)obj1).BadPwdCount);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the Cn.
        /// </summary>
        /// <param name="cn"></param>
        /// <returns></returns>
        public Users FilterByCn(string cn)
        {
            Users rtnValue = new Users();
            foreach (User entity in this)
            {
                if (entity.Cn == cn)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by Cn in ascending order.
        /// </summary>
        public void SortByCnAsc()
        {
            CnComparer comparer = new CnComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by Cn in descending order.
        /// </summary>
        public void SortByCnDesc()
        {
            CnComparer comparer = new CnComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by Cn.
        /// </summary>
        class CnComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((User)obj1).Cn.CompareTo(((User)obj2).Cn);
                }
                else
                {
                    return ((User)obj2).Cn.CompareTo(((User)obj1).Cn);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the Description.
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public Users FilterByDescription(string description)
        {
            Users rtnValue = new Users();
            foreach (User entity in this)
            {
                if (entity.Description == description)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by Description in ascending order.
        /// </summary>
        public void SortByDescriptionAsc()
        {
            DescriptionComparer comparer = new DescriptionComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by Description in descending order.
        /// </summary>
        public void SortByDescriptionDesc()
        {
            DescriptionComparer comparer = new DescriptionComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by Description.
        /// </summary>
        class DescriptionComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((User)obj1).Description.CompareTo(((User)obj2).Description);
                }
                else
                {
                    return ((User)obj2).Description.CompareTo(((User)obj1).Description);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the GivenName.
        /// </summary>
        /// <param name="givenName"></param>
        /// <returns></returns>
        public Users FilterByGivenName(string givenName)
        {
            Users rtnValue = new Users();
            foreach (User entity in this)
            {
                if (entity.GivenName == givenName)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by GivenName in ascending order.
        /// </summary>
        public void SortByGivenNameAsc()
        {
            GivenNameComparer comparer = new GivenNameComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by GivenName in descending order.
        /// </summary>
        public void SortByGivenNameDesc()
        {
            GivenNameComparer comparer = new GivenNameComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by GivenName.
        /// </summary>
        class GivenNameComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((User)obj1).GivenName.CompareTo(((User)obj2).GivenName);
                }
                else
                {
                    return ((User)obj2).GivenName.CompareTo(((User)obj1).GivenName);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the LockoutTime.
        /// </summary>
        /// <param name="lockoutTime"></param>
        /// <returns></returns>
        public Users FilterByLockoutTime(DateTime lockoutTime)
        {
            Users rtnValue = new Users();
            foreach (User entity in this)
            {
                if (entity.LockoutTime == lockoutTime)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by LockoutTime in ascending order.
        /// </summary>
        public void SortByLockoutTimeAsc()
        {
            LockoutTimeComparer comparer = new LockoutTimeComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by LockoutTime in descending order.
        /// </summary>
        public void SortByLockoutTimeDesc()
        {
            LockoutTimeComparer comparer = new LockoutTimeComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by LockoutTime.
        /// </summary>
        class LockoutTimeComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((User)obj1).LockoutTime.CompareTo(((User)obj2).LockoutTime);
                }
                else
                {
                    return ((User)obj2).LockoutTime.CompareTo(((User)obj1).LockoutTime);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the DistinguishedName.
        /// </summary>
        /// <param name="distinguishedName"></param>
        /// <returns></returns>
        public Users FilterByDistinguishedName(string distinguishedName)
        {
            Users rtnValue = new Users();
            foreach (User entity in this)
            {
                if (entity.DistinguishedName == distinguishedName)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by DistinguishedName in ascending order.
        /// </summary>
        public void SortByDistinguishedNameAsc()
        {
            DistinguishedNameComparer comparer = new DistinguishedNameComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by DistinguishedName in descending order.
        /// </summary>
        public void SortByDistinguishedNameDesc()
        {
            DistinguishedNameComparer comparer = new DistinguishedNameComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by DistinguishedName.
        /// </summary>
        class DistinguishedNameComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((User)obj1).DistinguishedName.CompareTo(((User)obj2).DistinguishedName);
                }
                else
                {
                    return ((User)obj2).DistinguishedName.CompareTo(((User)obj1).DistinguishedName);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the ObjectCategory.
        /// </summary>
        /// <param name="objectCategory"></param>
        /// <returns></returns>
        public Users FilterByObjectCategory(string objectCategory)
        {
            Users rtnValue = new Users();
            foreach (User entity in this)
            {
                if (entity.ObjectCategory == objectCategory)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by ObjectCategory in ascending order.
        /// </summary>
        public void SortByObjectCategoryAsc()
        {
            ObjectCategoryComparer comparer = new ObjectCategoryComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by ObjectCategory in descending order.
        /// </summary>
        public void SortByObjectCategoryDesc()
        {
            ObjectCategoryComparer comparer = new ObjectCategoryComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by ObjectCategory.
        /// </summary>
        class ObjectCategoryComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((User)obj1).ObjectCategory.CompareTo(((User)obj2).ObjectCategory);
                }
                else
                {
                    return ((User)obj2).ObjectCategory.CompareTo(((User)obj1).ObjectCategory);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the PwdLastSet.
        /// </summary>
        /// <param name="pwdLastSet"></param>
        /// <returns></returns>
        public Users FilterByPwdLastSet(DateTime pwdLastSet)
        {
            Users rtnValue = new Users();
            foreach (User entity in this)
            {
                if (entity.PwdLastSet == pwdLastSet)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by PwdLastSet in ascending order.
        /// </summary>
        public void SortByPwdLastSetAsc()
        {
            PwdLastSetComparer comparer = new PwdLastSetComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by PwdLastSet in descending order.
        /// </summary>
        public void SortByPwdLastSetDesc()
        {
            PwdLastSetComparer comparer = new PwdLastSetComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by PwdLastSet.
        /// </summary>
        class PwdLastSetComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((User)obj1).PwdLastSet.CompareTo(((User)obj2).PwdLastSet);
                }
                else
                {
                    return ((User)obj2).PwdLastSet.CompareTo(((User)obj1).PwdLastSet);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the Name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Users FilterByName(string name)
        {
            Users rtnValue = new Users();
            foreach (User entity in this)
            {
                if (entity.Name == name)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by Name in ascending order.
        /// </summary>
        public void SortByNameAsc()
        {
            NameComparer comparer = new NameComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by Name in descending order.
        /// </summary>
        public void SortByNameDesc()
        {
            NameComparer comparer = new NameComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by Name.
        /// </summary>
        class NameComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((User)obj1).Name.CompareTo(((User)obj2).Name);
                }
                else
                {
                    return ((User)obj2).Name.CompareTo(((User)obj1).Name);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the SamAccountName.
        /// </summary>
        /// <param name="samAccountName"></param>
        /// <returns></returns>
        public Users FilterBySamAccountName(string samAccountName)
        {
            Users rtnValue = new Users();
            foreach (User entity in this)
            {
                if (entity.SamAccountName == samAccountName)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by SamAccountName in ascending order.
        /// </summary>
        public void SortBySamAccountNameAsc()
        {
            SamAccountNameComparer comparer = new SamAccountNameComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by SamAccountName in descending order.
        /// </summary>
        public void SortBySamAccountNameDesc()
        {
            SamAccountNameComparer comparer = new SamAccountNameComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by SamAccountName.
        /// </summary>
        class SamAccountNameComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((User)obj1).SamAccountName.CompareTo(((User)obj2).SamAccountName);
                }
                else
                {
                    return ((User)obj2).SamAccountName.CompareTo(((User)obj1).SamAccountName);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the SamAccountType.
        /// </summary>
        /// <param name="samAccountType"></param>
        /// <returns></returns>
        public Users FilterBySamAccountType(int samAccountType)
        {
            Users rtnValue = new Users();
            foreach (User entity in this)
            {
                if (entity.SamAccountType == samAccountType)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by SamAccountType in ascending order.
        /// </summary>
        public void SortBySamAccountTypeAsc()
        {
            SamAccountTypeComparer comparer = new SamAccountTypeComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by SamAccountType in descending order.
        /// </summary>
        public void SortBySamAccountTypeDesc()
        {
            SamAccountTypeComparer comparer = new SamAccountTypeComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by SamAccountType.
        /// </summary>
        class SamAccountTypeComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((User)obj1).SamAccountType.CompareTo(((User)obj2).SamAccountType);
                }
                else
                {
                    return ((User)obj2).SamAccountType.CompareTo(((User)obj1).SamAccountType);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the Sn.
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        public Users FilterBySn(string sn)
        {
            Users rtnValue = new Users();
            foreach (User entity in this)
            {
                if (entity.Sn == sn)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by Sn in ascending order.
        /// </summary>
        public void SortBySnAsc()
        {
            SnComparer comparer = new SnComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by Sn in descending order.
        /// </summary>
        public void SortBySnDesc()
        {
            SnComparer comparer = new SnComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by Sn.
        /// </summary>
        class SnComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((User)obj1).Sn.CompareTo(((User)obj2).Sn);
                }
                else
                {
                    return ((User)obj2).Sn.CompareTo(((User)obj1).Sn);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the UserPrincipalName.
        /// </summary>
        /// <param name="userPrincipalName"></param>
        /// <returns></returns>
        public Users FilterByUserPrincipalName(string userPrincipalName)
        {
            Users rtnValue = new Users();
            foreach (User entity in this)
            {
                if (entity.UserPrincipalName == userPrincipalName)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by UserPrincipalName in ascending order.
        /// </summary>
        public void SortByUserPrincipalNameAsc()
        {
            UserPrincipalNameComparer comparer = new UserPrincipalNameComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by UserPrincipalName in descending order.
        /// </summary>
        public void SortByUserPrincipalNameDesc()
        {
            UserPrincipalNameComparer comparer = new UserPrincipalNameComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by UserPrincipalName.
        /// </summary>
        class UserPrincipalNameComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((User)obj1).UserPrincipalName.CompareTo(((User)obj2).UserPrincipalName);
                }
                else
                {
                    return ((User)obj2).UserPrincipalName.CompareTo(((User)obj1).UserPrincipalName);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the WhenChanged.
        /// </summary>
        /// <param name="whenChanged"></param>
        /// <returns></returns>
        public Users FilterByWhenChanged(DateTime whenChanged)
        {
            Users rtnValue = new Users();
            foreach (User entity in this)
            {
                if (entity.WhenChanged == whenChanged)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by WhenChanged in ascending order.
        /// </summary>
        public void SortByWhenChangedAsc()
        {
            WhenChangedComparer comparer = new WhenChangedComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by WhenChanged in descending order.
        /// </summary>
        public void SortByWhenChangedDesc()
        {
            WhenChangedComparer comparer = new WhenChangedComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by WhenChanged.
        /// </summary>
        class WhenChangedComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((User)obj1).WhenChanged.CompareTo(((User)obj2).WhenChanged);
                }
                else
                {
                    return ((User)obj2).WhenChanged.CompareTo(((User)obj1).WhenChanged);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the WhenCreated.
        /// </summary>
        /// <param name="whenCreated"></param>
        /// <returns></returns>
        public Users FilterByWhenCreated(DateTime whenCreated)
        {
            Users rtnValue = new Users();
            foreach (User entity in this)
            {
                if (entity.WhenCreated == whenCreated)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by WhenCreated in ascending order.
        /// </summary>
        public void SortByWhenCreatedAsc()
        {
            WhenCreatedComparer comparer = new WhenCreatedComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by WhenCreated in descending order.
        /// </summary>
        public void SortByWhenCreatedDesc()
        {
            WhenCreatedComparer comparer = new WhenCreatedComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by WhenCreated.
        /// </summary>
        class WhenCreatedComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((User)obj1).WhenCreated.CompareTo(((User)obj2).WhenCreated);
                }
                else
                {
                    return ((User)obj2).WhenCreated.CompareTo(((User)obj1).WhenCreated);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the AccountDisabled.
        /// </summary>
        /// <param name="accountDisabled"></param>
        /// <returns></returns>
        public Users FilterByAccountDisabled(bool accountDisabled)
        {
            Users rtnValue = new Users();
            foreach (User entity in this)
            {
                if (entity.AccountDisabled == accountDisabled)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by AccountDisabled in ascending order.
        /// </summary>
        public void SortByAccountDisabledAsc()
        {
            AccountDisabledComparer comparer = new AccountDisabledComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by AccountDisabled in descending order.
        /// </summary>
        public void SortByAccountDisabledDesc()
        {
            AccountDisabledComparer comparer = new AccountDisabledComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by AccountDisabled.
        /// </summary>
        class AccountDisabledComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((User)obj1).AccountDisabled.CompareTo(((User)obj2).AccountDisabled);
                }
                else
                {
                    return ((User)obj2).AccountDisabled.CompareTo(((User)obj1).AccountDisabled);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the PasswordExpired.
        /// </summary>
        /// <param name="passwordExpired"></param>
        /// <returns></returns>
        public Users FilterByPasswordExpired(bool passwordExpired)
        {
            Users rtnValue = new Users();
            foreach (User entity in this)
            {
                if (entity.PasswordExpired == passwordExpired)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by PasswordExpired in ascending order.
        /// </summary>
        public void SortByPasswordExpiredAsc()
        {
            PasswordExpiredComparer comparer = new PasswordExpiredComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by PasswordExpired in descending order.
        /// </summary>
        public void SortByPasswordExpiredDesc()
        {
            PasswordExpiredComparer comparer = new PasswordExpiredComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by PasswordExpired.
        /// </summary>
        class PasswordExpiredComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((User)obj1).PasswordExpired.CompareTo(((User)obj2).PasswordExpired);
                }
                else
                {
                    return ((User)obj2).PasswordExpired.CompareTo(((User)obj1).PasswordExpired);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the AccountLockedOut.
        /// </summary>
        /// <param name="accountLockedOut"></param>
        /// <returns></returns>
        public Users FilterByAccountLockedOut(bool accountLockedOut)
        {
            Users rtnValue = new Users();
            foreach (User entity in this)
            {
                if (entity.AccountLockedOut == accountLockedOut)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by AccountLockedOut in ascending order.
        /// </summary>
        public void SortByAccountLockedOutAsc()
        {
            AccountLockedOutComparer comparer = new AccountLockedOutComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by AccountLockedOut in descending order.
        /// </summary>
        public void SortByAccountLockedOutDesc()
        {
            AccountLockedOutComparer comparer = new AccountLockedOutComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by AccountLockedOut.
        /// </summary>
        class AccountLockedOutComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((User)obj1).AccountLockedOut.CompareTo(((User)obj2).AccountLockedOut);
                }
                else
                {
                    return ((User)obj2).AccountLockedOut.CompareTo(((User)obj1).AccountLockedOut);
                }
            }
        }
                        
	}
}
