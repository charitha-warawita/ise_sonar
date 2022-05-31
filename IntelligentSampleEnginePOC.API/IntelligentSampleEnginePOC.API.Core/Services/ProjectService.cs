using IntelligentSampleEnginePOC.API.Core.Data;
using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Services
{
    public class ProjectService : IProjectService
    {
        private ProjectContext _dataContext;

        public ProjectService()
        {
            _dataContext = new ProjectContext();
        }
        public Project CreateProject(Project project)
        {
            if(project == null)
                throw new ArgumentNullException("Project model not found", nameof(project));

            return _dataContext.CreateProject(project);


        }
    }
}
