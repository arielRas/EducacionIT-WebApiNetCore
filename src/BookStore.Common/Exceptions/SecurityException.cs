using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Exceptions
{
    public class SecurityException : CriticalException
    {
        public SecurityException(string message, Guid traceId) : base(message, traceId)
        {

        }

        public SecurityException(string message, Exception innerException, Guid traceId) : base(message, innerException, traceId)
        {

        }
    }
}
