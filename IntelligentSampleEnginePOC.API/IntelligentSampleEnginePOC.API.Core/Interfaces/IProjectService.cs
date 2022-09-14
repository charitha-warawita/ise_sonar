using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Core.Model.Cint;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface IProjectService
    {
        Task<Project> CreateProject(Project project);
        Task<Project> LaunchProject(Project project);

        Task<ProjectList> GetProjects(int? status, int pageNumber, string? searchString, int recordCount);

        Task<Project?> GetAsync(long id);

        Task<List<Survey>> GetSurveysAsync(long id);
        
        Task<List<Cost>> GetCurrentCostAsync(long id);
    }
}
