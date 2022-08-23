using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface IProjectContext
    {
        Project CreateProject(Project project);

        long UpdateProjectStatus(long projectId, Status status);

        ProjectList GetProjects(int? status,int pageNumber, string? searchString, int recentCount);

        Project GetProject(long projectId);
    }
}
