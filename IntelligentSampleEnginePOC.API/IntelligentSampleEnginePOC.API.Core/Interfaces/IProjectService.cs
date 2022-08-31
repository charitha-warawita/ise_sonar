using IntelligentSampleEnginePOC.API.Core.Model;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface IProjectService
    {
        Task<Project> CreateProject(Project project);
        Task<Project> LaunchProject(Project project);

        Task<ProjectList> GetProjects(int? status, int pageNumber, string? searchString, int recentCount);

        Task<Project?> GetAsync(long id);
    }
}
