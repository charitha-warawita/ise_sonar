using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Core.Model.Cint;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface ICintSamplingService
    {
        List<CintRequestModel> ConvertToCintRequests(Project project);
        Task<Project> CreateProject(Project project);
        Task<List<Survey>> GetSurveysAsync(long id);
    }
}
