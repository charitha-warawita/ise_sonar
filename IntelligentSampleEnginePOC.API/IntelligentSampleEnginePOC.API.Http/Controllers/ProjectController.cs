using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IntelligentSampleEnginePOC.API.Http.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        IProjectService _projectService;
        
        public ProjectController(IProjectService projectService, ICintSamplingService samplingService)
        {
            _projectService = projectService;
            
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

        [HttpGet("id/{id}")]
        public ActionResult GetById(string id)
        {
            return Ok();
        }

        [HttpGet]
        public ActionResult GetProjects(int? status, int pageNumber, string? searchString, int recentCount)
        {
            try
            {
                var projectLists = _projectService.GetProjects(status, pageNumber, searchString, recentCount);
                return Ok(projectLists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Exception occured - " + ex.Message);
            }
        }
    }
}
