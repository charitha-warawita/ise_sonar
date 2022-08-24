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

        /*public async Task<IValidationModel> ValidateProject(Project project)
        {
            if (project == null)
                throw new ArgumentNullException("project model not found", nameof(project));

            var result = _samplingService.ConvertToCintRequests(project);
            return result;
        }*/
        public ProjectList GetProjects(int? status, int pageNumber, string? searchString, int recentCount)
        {
            return _projectContext.GetProjects(status, pageNumber, searchString, recentCount);
        }

        public Project? Get(long id)
        {
            var project = _projectContext.Get(id);
            if (project is null) return project;

            project.TargetAudiences = _taService.GetAllByProjectId(id).ToList();

            return project;
        }
 
        /*private Model.Project SetupGuids(Model.Project project)
        {
            //Setting up IDs
            /*if (project?.Id == Guid.Empty)
                project.Id = Guid.NewGuid();

            if (project?.User != null && project?.User?.Id == Guid.Empty)
                project.User.Id = Guid.NewGuid();

            if (project.TargetAudiences.Any())
            {
                foreach (var item in project.TargetAudiences)
                {
                    if (item?.Id == Guid.Empty)
                        item.Id = Guid.NewGuid();

                    if (item.Qualifications.Any())
                    {
                        foreach (var subItem in item.Qualifications)
                        {
                            if (subItem?.Id == Guid.Empty)
                                subItem.Id = Guid.NewGuid();
                        }
                    }
                }
            }
            return project;
        }*/


        private bool ProjectValidated(Project project)
        {
            if (project != null && project.LastUpdate == null)
                project.LastUpdate = DateTime.Now;

            return true;
        }

        
    }
}
