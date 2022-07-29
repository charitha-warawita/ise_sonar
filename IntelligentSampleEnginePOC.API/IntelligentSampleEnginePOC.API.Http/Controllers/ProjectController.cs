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
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Project project)
        {
            try
            {
                var resultProject = _projectService.CreateProject(project);
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
            return Ok();
        }

        [HttpPost("launch")]
        public async Task<ActionResult> Launch([FromBody] Project project)
        {
            /*try
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
            }*/

            return Ok();
        }

        [HttpPost("update")]
        public ActionResult UpdateProject([FromBody] Project project)
        {
            /*try
            {
                var resultProject = _projectService.UpdateProject(project);
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
            }*/
            return Ok();

        }

        /*[HttpGet("template")]
        public ActionResult GetEmptyProjectTemplate()
        {
            var projectTemplate = _projectService.GetProjectTemplate();
            if (projectTemplate != null)
            {
                return Ok(projectTemplate);
            }
            else
                return StatusCode(500, "Error response");
        }*/

        [HttpGet("id/{id}")]
        public ActionResult GetById(string id)
        {
            /*var project = _projectService.GetProjects(id);
            return Ok(project);*/

            //if (!string.IsNullOrEmpty(id))
            //{
            //    return Ok(new Project());
            //}
            //else
            //    return StatusCode(500, "Id not found");

            return Ok();
        }

        [HttpGet]
        public ActionResult GetAll(int? status, string? searchString, int? recentCount)
        {
            /*var projectLists = _projectService.GetProjects(status, searchString, recentCount);
            return Ok(projectLists);*/

            return Ok();
        }

        /*[HttpPost]
        public ActionResult LaunchProject(Project project)
        {
            //Yet to do 

            if (project != null)
                return Ok(new CintResponse());
            else
                return StatusCode(500, "The current project is not launched");
        }*/
    }
}
