using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopServer.Exceptions
{
    public class InvalidFieldsException : Exception
    {
        public InvalidFieldsException()
        {
        }

        public InvalidFieldsException(string message) : base(message)
        {
        }

        public InvalidFieldsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
