using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface ISpecTransform
    {
        List<CintRequestModel> TransformIseRequestToCintRequests(Project project);
    }
}
