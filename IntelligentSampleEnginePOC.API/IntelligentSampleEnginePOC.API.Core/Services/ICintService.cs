using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Services
{
    public interface ICintService
    {
        CintRequest ConvertProjectToCintRequest(Project project);

        Task<bool> CallCintApi(CintRequest request);
    }
}
