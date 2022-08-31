using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using Microsoft.AspNetCore.Mvc;
using IntelligentSampleEnginePOC.API.Core.Results;

namespace IntelligentSampleEnginePOC.API.Http.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly ITargetAudienceService _targetAudienceService;

        public ProjectController(IProjectService projectService, ITargetAudienceService targetAudienceService, ICintSamplingService samplingService)
        {
            _projectService = projectService;
            _targetAudienceService = targetAudienceService;
        }

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

        [HttpPost("update")]
        public ActionResult UpdateProject([FromBody] Project project)
        {
            return Ok();
        }

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
        
        [HttpGet("{id}/TargetAudiences")]
        public async Task<ActionResult> GetTargetAudiencesForProjectPaged(long id, [FromQuery] int page = 1, [FromQuery] int pageSize = 5)
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
    }
}
