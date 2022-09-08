using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message): base(message)
        {

        }
    }

    public class ISEApplicationException : Exception
    {
        public ISEApplicationException(string message): base (message)
        {

        }
    }
}
