using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Core.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IntelligentSampleEnginePOC.API.Http.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:scopes")]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IProjectService _projectService;
        private readonly ITargetAudienceService _targetAudienceService;

        public ProjectController(
            ILogger<ProjectController> logger,
            IProjectService projectService,
            ITargetAudienceService targetAudienceService)
        {
            _logger = logger;
            _projectService = projectService;
            _targetAudienceService = targetAudienceService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Project project)
        {
            try
            {
                var resultProject = await _projectService.CreateProject(project);
                if (resultProject != null)
                {
                    return Ok(resultProject);
                }
                else
                    return StatusCode(500, "Error occured");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Exception occured - " + ex.Message);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("launch")]
        public async Task<ActionResult> Launch([FromBody] Project project)
        {
            try
            {
                var result = await _projectService.LaunchProject(project);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                    return StatusCode(500, "Error occured");
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Exception occured - " + ex.Message);
            }

            return Ok();
        }

        [Authorize]
        [HttpPost("update")]
        public ActionResult UpdateProject([FromBody] Project project)
        {
            return Ok();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(long id)
        {
            try
            {
                var result = await _projectService.GetAsync(id);

                return Ok(result);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetProjects(int? status, int pageNumber, string? searchString, int recordCount)
        {
            try
            {
                var projectLists = await _projectService.GetProjects(status, pageNumber, searchString, recordCount);
                return Ok(projectLists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Exception occured - " + ex.Message);
            }
        }
        
        [Authorize]
        [HttpGet("{id}/TargetAudiences")]
        public async Task<ActionResult> GetTargetAudiencesForProjectPaged(
            long id,
            [FromQuery] int page = Constants.PAGENUMBER_DEFAULT,
            [FromQuery] int pageSize = Constants.RECORDCOUNT_DEFAULT)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Invalid paging data");
            
            PagedResult<TargetAudience> result;
            try
            {
                result = await _targetAudienceService.GetAllByProjectIdAsync(id, page, pageSize);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
            
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}/Surveys")]
        public async Task<ActionResult> GetSurveys(long id)
        {
            try
            {
                var surveys = await _projectService.GetSurveysAsync(id);

                return Ok(surveys);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ProjectController - GetSurveys - Error: {Message}", e.Message);

                return Problem(e.Message);
            }
        }
    }
}
