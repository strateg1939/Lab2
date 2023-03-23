using System;

namespace Lab2.AppException
{
    public class IncorrectEmailException : Exception
    {
        public IncorrectEmailException(string message) : base(message)
        {
        }
    }
}
