using IntelligentSampleEnginePOC.UI.Models;

namespace IntelligentSampleEnginePOC.UI.Core
{
    public interface IProjectVMService
    {
        Task<ProjectViewModel> GetProjectVM(string status, string searchString);

        Task<Project> CreateProject(Project project);
    }
}
