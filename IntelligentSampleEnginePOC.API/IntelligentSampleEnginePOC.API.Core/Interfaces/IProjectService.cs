using IntelligentSampleEnginePOC.API.Core.Model;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface IProjectService
    {
        Task<Project> CreateProject(Project project);
        Task<Project> LaunchProject(Project project);
        // Task<IValidationModel> ValidateProject(Project project);
        /*List<DBModel.Project> GetProjects(int? status, string? searchString, int? recentCount);
        List<Project> GetProjects(int? status,int pageNumber, string? searchString, int recentCount);

        /*Task<Model.Project> LaunchProject(Model.Project project);
       Model.Project GetProjects(string Id);
       Model.Project UpdateProject(Model.Project project);*/
    }
}
