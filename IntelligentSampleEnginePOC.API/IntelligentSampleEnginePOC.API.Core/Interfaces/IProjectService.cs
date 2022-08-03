using IntelligentSampleEnginePOC.API.Core.Model;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface IProjectService
    {
        Project CreateProject(Project project);

        /*List<DBModel.Project> GetProjects(int? status, string? searchString, int? recentCount);

        Task<Model.Project> LaunchProject(Model.Project project);
        Model.Project GetProjects(string Id);
        Model.Project UpdateProject(Model.Project project);*/
    }
}
