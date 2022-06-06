using IntelligentSampleEnginePOC.UI.Models;

namespace IntelligentSampleEnginePOC.UI.Core
{
    public interface IProjectVMService
    {
        Task<ProjectViewModel> GetProjectVM(string status, string searchString);

        // Task<Project> GetEmptyProjectTemplate();

        Task<Project> CreateProject(Project project);

        Task<bool> LaunchProject(Project project);
    }
}
