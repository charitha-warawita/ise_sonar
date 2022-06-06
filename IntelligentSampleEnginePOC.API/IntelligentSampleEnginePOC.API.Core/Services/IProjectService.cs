using Model = IntelligentSampleEnginePOC.API.Core.Model;
using DBModel = IntelligentSampleEnginePOC.API.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Services
{
    public interface IProjectService
    {
        Model.Project CreateProject(Model.Project project);

        List<DBModel.Project> GetProjects(int? status, string? searchString, int? recentCount);

        Task<Model.Project> LaunchProject(Model.Project project);
    }
}
