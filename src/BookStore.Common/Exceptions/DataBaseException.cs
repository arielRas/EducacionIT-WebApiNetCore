using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Exceptions
{
    public class DataBaseException : CriticalException
    {
        public DataBaseException(string message, Guid traceId) : base(message, traceId)
        {

        }

        public DataBaseException(string message, Exception innerException, Guid traceId) : base(message, innerException, traceId)
        {

        }
    }
}
