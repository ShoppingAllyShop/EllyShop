using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Exceptions
{
    public class BusinessException: Exception
    {
        // Default constructor
        public BusinessException() : base("A custom exception has occurred.")
        {
        }

        // Constructor that accepts a custom message
        public BusinessException(string message) : base(message)
        {
        }
    }
}
