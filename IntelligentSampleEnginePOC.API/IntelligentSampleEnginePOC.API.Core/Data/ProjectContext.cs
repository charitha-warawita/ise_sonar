using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Data
{
    public class ProjectContext
    {
        public Project CreateProject(Project project)
        {
            if (project != null)
                project.Id = Guid.NewGuid();
            else
                new Project { Id = Guid.NewGuid() };

            return project;
        }
    }
}
