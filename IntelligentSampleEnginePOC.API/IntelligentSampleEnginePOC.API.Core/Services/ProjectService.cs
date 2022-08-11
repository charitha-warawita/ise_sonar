using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;

namespace IntelligentSampleEnginePOC.API.Core.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectContext _projectContext;
        private readonly ITargetAudienceService _taService;
        private readonly ICintSamplingService _samplingService;

        public ProjectService(IProjectContext context, ITargetAudienceService taService, ICintSamplingService samplingService)
        {
            _projectContext = context;
            _taService = taService;
            _samplingService = samplingService;
        }

        public async Task<Project> CreateProject(Project project)
        {
            if (project == null)
                throw new ArgumentNullException("Project model not found", nameof(project));

            if (ProjectValidated(project))
            {
                project = _projectContext.CreateProject(project);
                if(project.TargetAudiences.Any())
                {
                    for(int i = 0; i < project.TargetAudiences.Count; i++)
                    {
                        project.TargetAudiences[i] = _taService.CreateTargetAudience(project.Id, project.TargetAudiences[i]);
                    }
                }

                return project;
            }

            throw new ArgumentException("Project Validation failed", nameof(project));
        }

        public async Task<Project> LaunchProject(Project project)
        {
            if (project == null)
                throw new ArgumentNullException("project model not found", nameof(project));

            // project = await CreateProject(project);
            project = await _samplingService.CreateProject(project);
            project.Status = Model.Status.Created;
            // ModelMapping(project, true);
            // _dataContext.SaveChanges();
            return project;
        }

        /*public async Task<IValidationModel> ValidateProject(Project project)
        {
            if (project == null)
                throw new ArgumentNullException("project model not found", nameof(project));

            var result = _samplingService.ConvertToCintRequests(project);
            return result;
        }*/


        private bool ProjectValidated(Project project)
        {
            return true;
        }

        
    }
}
