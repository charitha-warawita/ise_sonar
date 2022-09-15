using IntelligentSampleEnginePOC.API.Core.Exceptions;
using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Core.Model.Cint;
using TaskExtensions = IntelligentSampleEnginePOC.API.Core.Extensions.TaskExtensions;

namespace IntelligentSampleEnginePOC.API.Core.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectContext _projectContext;
        private readonly ITargetAudienceService _taService;
        private readonly ICintSamplingService _samplingService;
        private readonly IProjectValidator _projectValidator;

        public ProjectService(
            IProjectContext context,
            ITargetAudienceService taService,
            ICintSamplingService samplingService,
            IProjectValidator projectValidator)
        {
            _projectContext = context;
            _taService = taService;
            _samplingService = samplingService;
            _projectValidator = projectValidator;
        }

        public async Task<Project> CreateProject(Project project)
        {
            if (project == null)
                throw new BadRequestException("Project model not found");
                // throw new ArgumentNullException("Project model not found", nameof(project));
            project.LastUpdate = DateTime.Now;
            if (_projectValidator.IsValidated(project))
            {
                var updateProjectStatus = project.StartDate.AddDays(project.FieldingPeriod);
                if (project.StartDate < DateTime.Now && updateProjectStatus > DateTime.Now)
                {
                    project.Status = (Status)2;
                }
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
            else
                throw new BadRequestException("ISE project validation failed");
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
                throw new BadRequestException("Project model not found");
            // throw new ArgumentNullException("project model not found", nameof(project));

            project.LastUpdate = DateTime.Now;
            try
            {
                project = await CreateProject(project);
                project = await _samplingService.CreateProject(project);

                var updateProjectStatus = project.StartDate.AddDays(project.FieldingPeriod);
                if (project.StartDate < DateTime.Now && updateProjectStatus > DateTime.Now)
                {
                    project.Status = Model.Status.Live;
                }
                else 
                {
                    project.Status = Model.Status.Created;
                }
                
                await UpdatePrjectStatus(project.Id, project.Status);

            }
            catch(HttpRequestException ex)
            {
                throw new HttpRequestException("Failing in Cint API call - " + ex.Message, ex);
            }
            catch(Exception ex)
            {
                throw;
            }

            return project;
        }

        public async Task<ProjectList> GetProjects(int? status, int pageNumber, string? searchString, int recordCount)
        {
            if (pageNumber <= 0 && recordCount > 0)
                pageNumber=Constants.PAGENUMBER_DEFAULT;
            if (recordCount <= 0 && pageNumber > 0)
                recordCount = Constants.RECORDCOUNT_DEFAULT;
            return _projectContext.GetProjects(status, pageNumber, searchString, recordCount);
        }

        public async Task<Project?> GetAsync(long id)
        {
            var project = await _projectContext.GetAsync(id);
            
            return project;
        }

        public async Task<List<Survey>> GetSurveysAsync(long id)
        {
            var surveys = await _samplingService.GetSurveysAsync(id);

            return surveys;
        }

        public async Task<List<Cost>> GetCurrentCostAsync(long id)
        {
            var surveys = await _samplingService.GetSurveyIdsAsync(id);
            if (!surveys.Any())
                return new List<Cost>();
            
            // Send all requests to Cint.
            var tasks = surveys.Select(s => _samplingService.GetCurrentCostAsync(s)).ToList();
            var result = await TaskExtensions.WhenAll(tasks); // Wait for all requests to complete. 

            var costs = result
                .GroupBy(k => k.Currency, v => v.Amount)
                .Select(g => new Cost(g.Sum(), g.Key))
                .ToList();
            
            return costs;
        }

        private bool ProjectValidated(Project project)
        {
            if (project != null && project.LastUpdate == null)
                project.LastUpdate = DateTime.Now;

            return true;
        }
    }
}
