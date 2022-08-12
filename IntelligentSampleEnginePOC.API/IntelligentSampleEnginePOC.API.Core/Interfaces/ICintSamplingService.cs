using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface ICintSamplingService
    {
        CintRequestsModel ConvertToCintRequests(Project project);
        Task<Project> CreateProject(Project project);
    }
}
