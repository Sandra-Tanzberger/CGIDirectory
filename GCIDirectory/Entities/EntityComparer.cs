using System;
using System.Collections;

namespace Graebel.Common.GCIDirectory.Entities
{
	/// <summary>
	/// Base class for comparing entities within
	/// custom collections.
	/// </summary>
	public abstract class EntityComparer : IComparer
	{
		private SortDirection m_sortDirection = SortDirection.Ascending;   
     
        /// <summary>
        /// Gets or sets the SortDirection for this comparer.
        /// </summary>
        public SortDirection EntitySortDirection
        {
            get
            {
                return m_sortDirection;   
            }
            set
            {
                m_sortDirection = value;
            }
        }

        /// <summary>
        /// To be implemented by concrete subclasses.
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public abstract int Compare(object obj1, object obj2);
	}

    /// <summary>
    /// Defines Sorting directions.
    /// </summary>
    public enum SortDirection
    {
        Ascending,
        Descending
    }
}
