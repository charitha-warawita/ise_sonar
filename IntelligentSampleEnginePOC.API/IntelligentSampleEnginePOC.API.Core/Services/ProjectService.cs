using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;

namespace IntelligentSampleEnginePOC.API.Core.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectContext _projectContext;
        private readonly ITargetAudienceService _taService;
        private readonly ICintSamplingService _samplingService;
        private readonly IProjectValidator _projectValidator;

        public ProjectService(IProjectContext context, ITargetAudienceService taService, ICintSamplingService samplingService, IProjectValidator projectValidator)
        {
            _projectContext = context;
            _taService = taService;
            _samplingService = samplingService;
            _projectValidator = projectValidator;
        }

        public async Task<Project> CreateProject(Project project)
        {
            if (project == null)
                throw new ArgumentNullException("Project model not found", nameof(project));

            if (_projectValidator.IsValidated(project))
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

        public async Task<long> UpdatePrjectStatus(long projectId, Status status)
        {
            if(projectId < 1)
                throw new ArgumentException("Not a valid project", nameof(projectId));

            return _projectContext.UpdateProjectStatus(projectId, status);

        }

        public async Task<Project> LaunchProject(Project project)
        {
            if (project == null)
                throw new ArgumentNullException("project model not found", nameof(project));

            try
            {
                project = await CreateProject(project);
                project = await _samplingService.CreateProject(project);
                
                project.Status = Model.Status.Created;
                await UpdatePrjectStatus(project.Id, project.Status);

            }
            catch(Exception ex)
            {
                throw new Exception("Failed in Launch project service - " + ex.Message, ex);
            }

            return project;
        }

        public async Task<ProjectList> GetProjects(int? status, int pageNumber, string? searchString, int recordCount)
        {
            return _projectContext.GetProjects(status, pageNumber, searchString, recordCount);
        }

        private bool ProjectValidated(Project project)
        {
            if (project != null && project.LastUpdate == null)
                project.LastUpdate = DateTime.Now;

            return true;
        }

        public async Task<Project> GetProjectById(long id)
        {
            if (id > 0)
                return _projectContext.GetProject(id);
            else
                throw new ArgumentNullException("Project Id submitted in invalid");
        }
    }
}
