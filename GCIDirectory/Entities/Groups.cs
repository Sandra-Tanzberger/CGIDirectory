using System;
using System.Collections;
using System.Runtime.Serialization;
//using Graebel.Common.Components.BusinessEntities;

namespace Graebel.Common.GCIDirectory.Entities
{
	/// <summary>
	/// Collection of GroupSimpleEntities.
	/// </summary>
	[Serializable()]
	public class Groups : CollectionBase
	{
		
        /// <summary>
        /// Adds a <c>Group</c> to the collection.
        /// </summary>
        /// <param name="Group">
        /// The <c>Group</c> to add to the collection.
        /// </param>
        /// <returns>void</returns>
        public virtual void Add(Group Group)
        {
            this.List.Add(Group);        
        }
    
        /// <summary>
        /// Indexer for the class, return the <c>Group</c> object 
        /// at the specified index.
        /// </summary>
        public virtual Group this[int index]
        {
            get 
            { 
                return (Group) this.List[index];  
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
        public Groups FilterByNativeGuid(string nativeGuid)
        {
            Groups rtnValue = new Groups();
            foreach (Group entity in this)
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
                    return ((Group)obj1).NativeGuid.CompareTo(((Group)obj2).NativeGuid);
                }
                else
                {
                    return ((Group)obj2).NativeGuid.CompareTo(((Group)obj1).NativeGuid);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the Guid.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Groups FilterByGuid(Guid guid)
        {
            Groups rtnValue = new Groups();
            foreach (Group entity in this)
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
                    return ((Group)obj1).Guid.CompareTo(((Group)obj2).Guid);
                }
                else
                {
                    return ((Group)obj2).Guid.CompareTo(((Group)obj1).Guid);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the Cn.
        /// </summary>
        /// <param name="cn"></param>
        /// <returns></returns>
        public Groups FilterByCn(string cn)
        {
            Groups rtnValue = new Groups();
            foreach (Group entity in this)
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
                    return ((Group)obj1).Cn.CompareTo(((Group)obj2).Cn);
                }
                else
                {
                    return ((Group)obj2).Cn.CompareTo(((Group)obj1).Cn);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the Description.
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public Groups FilterByDescription(string description)
        {
            Groups rtnValue = new Groups();
            foreach (Group entity in this)
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
                    return ((Group)obj1).Description.CompareTo(((Group)obj2).Description);
                }
                else
                {
                    return ((Group)obj2).Description.CompareTo(((Group)obj1).Description);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the GroupType.
        /// </summary>
        /// <param name="groupType"></param>
        /// <returns></returns>
        public Groups FilterByGroupType(int groupType)
        {
            Groups rtnValue = new Groups();
            foreach (Group entity in this)
            {
                if (entity.GroupType == groupType)
                {
                    rtnValue.Add(entity);
                }
            }
            return rtnValue;
        }
        
        /// <summary>
        /// Sorts the collection by GroupType in ascending order.
        /// </summary>
        public void SortByGroupTypeAsc()
        {
            GroupTypeComparer comparer = new GroupTypeComparer();
            comparer.EntitySortDirection = SortDirection.Ascending;
            InnerList.Sort(comparer);
        }
        
        /// <summary>
        /// Sorts the collection by GroupType in descending order.
        /// </summary>
        public void SortByGroupTypeDesc()
        {
            GroupTypeComparer comparer = new GroupTypeComparer();
            comparer.EntitySortDirection = SortDirection.Descending;
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Used to sort by GroupType.
        /// </summary>
        class GroupTypeComparer : EntityComparer
        {            
            public override int Compare(object obj1, object obj2)
            {
                if (EntitySortDirection == SortDirection.Ascending)
                {
                    return ((Group)obj1).GroupType.CompareTo(((Group)obj2).GroupType);
                }
                else
                {
                    return ((Group)obj2).GroupType.CompareTo(((Group)obj1).GroupType);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the DistinguishedName.
        /// </summary>
        /// <param name="distinguishedName"></param>
        /// <returns></returns>
        public Groups FilterByDistinguishedName(string distinguishedName)
        {
            Groups rtnValue = new Groups();
            foreach (Group entity in this)
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
                    return ((Group)obj1).DistinguishedName.CompareTo(((Group)obj2).DistinguishedName);
                }
                else
                {
                    return ((Group)obj2).DistinguishedName.CompareTo(((Group)obj1).DistinguishedName);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the ObjectCategory.
        /// </summary>
        /// <param name="objectCategory"></param>
        /// <returns></returns>
        public Groups FilterByObjectCategory(string objectCategory)
        {
            Groups rtnValue = new Groups();
            foreach (Group entity in this)
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
                    return ((Group)obj1).ObjectCategory.CompareTo(((Group)obj2).ObjectCategory);
                }
                else
                {
                    return ((Group)obj2).ObjectCategory.CompareTo(((Group)obj1).ObjectCategory);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the Name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Groups FilterByName(string name)
        {
            Groups rtnValue = new Groups();
            foreach (Group entity in this)
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
                    return ((Group)obj1).Name.CompareTo(((Group)obj2).Name);
                }
                else
                {
                    return ((Group)obj2).Name.CompareTo(((Group)obj1).Name);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the SamAccountName.
        /// </summary>
        /// <param name="samAccountName"></param>
        /// <returns></returns>
        public Groups FilterBySamAccountName(string samAccountName)
        {
            Groups rtnValue = new Groups();
            foreach (Group entity in this)
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
                    return ((Group)obj1).SamAccountName.CompareTo(((Group)obj2).SamAccountName);
                }
                else
                {
                    return ((Group)obj2).SamAccountName.CompareTo(((Group)obj1).SamAccountName);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the SamAccountType.
        /// </summary>
        /// <param name="samAccountType"></param>
        /// <returns></returns>
        public Groups FilterBySamAccountType(int samAccountType)
        {
            Groups rtnValue = new Groups();
            foreach (Group entity in this)
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
                    return ((Group)obj1).SamAccountType.CompareTo(((Group)obj2).SamAccountType);
                }
                else
                {
                    return ((Group)obj2).SamAccountType.CompareTo(((Group)obj1).SamAccountType);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the WhenChanged.
        /// </summary>
        /// <param name="whenChanged"></param>
        /// <returns></returns>
        public Groups FilterByWhenChanged(DateTime whenChanged)
        {
            Groups rtnValue = new Groups();
            foreach (Group entity in this)
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
                    return ((Group)obj1).WhenChanged.CompareTo(((Group)obj2).WhenChanged);
                }
                else
                {
                    return ((Group)obj2).WhenChanged.CompareTo(((Group)obj1).WhenChanged);
                }
            }
        }
                
        /// <summary>
        /// Filters this collection by the WhenCreated.
        /// </summary>
        /// <param name="whenCreated"></param>
        /// <returns></returns>
        public Groups FilterByWhenCreated(DateTime whenCreated)
        {
            Groups rtnValue = new Groups();
            foreach (Group entity in this)
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
                    return ((Group)obj1).WhenCreated.CompareTo(((Group)obj2).WhenCreated);
                }
                else
                {
                    return ((Group)obj2).WhenCreated.CompareTo(((Group)obj1).WhenCreated);
                }
            }
        }
                        
	}
}
