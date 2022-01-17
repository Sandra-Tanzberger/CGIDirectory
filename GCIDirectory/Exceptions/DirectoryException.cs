using System;

namespace Graebel.Common.GCIDirectory.Exceptions
{
	/// <summary>
	/// Summary description for DirectoryExceptions.
	/// </summary>
	public class DirectoryException : ApplicationException
	{
		
		private DirectoryExceptionType m_DirectoryExceptionType;

		public DirectoryExceptionType DirectoryExceptionType
		{
			get 
			{
				return m_DirectoryExceptionType;
			}
		}

		/// <summary>
		/// DirectoryException constructor that does not take
		/// an inner exception.
		/// </summary>
		/// <param name="directoryExceptionType"></param>
		/// <param name="exceptionMessage"></param>
		public DirectoryException(
			string exceptionMessage) : base(exceptionMessage)
		{
			m_DirectoryExceptionType = DirectoryExceptionType.General;

		}
		
		public DirectoryException(DirectoryExceptionType directoryExceptionType,
			Exception exception, 
			string exceptionMessage) : base(exceptionMessage, exception)
		{
			m_DirectoryExceptionType = directoryExceptionType;

		}
	}

	public enum DirectoryExceptionType
	{
		General,
		ObjectExists,
		FailedLogin,
		PasswordRuleViolation
	}

}