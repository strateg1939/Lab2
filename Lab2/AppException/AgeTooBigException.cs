using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.AppException
{
    public class AgeTooBigException : Exception
    {
        public AgeTooBigException(string message) : base(message)
        {

        }
    }
}
