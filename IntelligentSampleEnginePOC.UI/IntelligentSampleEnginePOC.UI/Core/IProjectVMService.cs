using IntelligentSampleEnginePOC.UI.Models;

namespace IntelligentSampleEnginePOC.UI.Core
{
    public interface IProjectVMService
    {
        Task<ProjectViewModel> GetProjectVM(string status, string searchString);

        // Task<Project> GetEmptyProjectTemplate();

        Task<Project> CreateProject(Project project);

        Task<Project> LaunchProject(Project project);
       Task<Project> EditProject(string id);
        Task<Project> UpdateProject(Project project);
    }
}
