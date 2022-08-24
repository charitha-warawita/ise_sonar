using IntelligentSampleEnginePOC.API.Core.Model;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface IProjectService
    {
        Task<Project> CreateProject(Project project);
        Task<Project> LaunchProject(Project project);

        Task<ProjectList> GetProjects(int? status, int pageNumber, string? searchString, int recentCount);

        Project? Get(long id);
        // Task<IValidationModel> ValidateProject(Project project);           

        /*Task<Model.Project> LaunchProject(Model.Project project);
       Model.Project GetProjects(string Id);
       Model.Project UpdateProject(Model.Project project);*/
    }
}
