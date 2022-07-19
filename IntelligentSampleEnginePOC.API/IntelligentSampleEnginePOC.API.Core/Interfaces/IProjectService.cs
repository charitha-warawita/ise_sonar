using Model = IntelligentSampleEnginePOC.API.Core.Model;
using DBModel = IntelligentSampleEnginePOC.API.Core.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface IProjectService
    {
        Model.Project CreateProject(Model.Project project);

        List<DBModel.Project> GetProjects(int? status, string? searchString, int? recentCount);

        Task<Model.Project> LaunchProject(Model.Project project);
        Model.Project GetProjects(string Id);
        Model.Project UpdateProject(Model.Project project);
    }
}
