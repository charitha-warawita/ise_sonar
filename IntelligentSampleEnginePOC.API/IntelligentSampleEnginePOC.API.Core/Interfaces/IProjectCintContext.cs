using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface IProjectCintContext
    {
        public long StoreProjectCintResponse(bool isSuccess, string statusCode, CintRequestModel request, CintResponse response); 
    }
}
