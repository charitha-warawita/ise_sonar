using IntelligentSampleEnginePOC.API.Core.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IntelligentSampleEnginePOC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody] Project project)
        {
            //Yet to do

            if (project != null)
            {
                project.Id = Guid.NewGuid();
                return Ok(project);
            }
            else
                return StatusCode(500, "Error occured");
            
        }

        [HttpPut]
        public ActionResult Put([FromBody] Project project)
        {
            //Yet to do

            if (project != null)
                return Ok(project);
            else
                return StatusCode(500,"Error occured");

        }

        [HttpGet("id/{id}")]
        public ActionResult GetById(string id)
        {
            //Yet to do 

            if (!string.IsNullOrEmpty(id))
            {
                return Ok(new Project());
            }
            else
                return StatusCode(500, "Id not found");
        }

        [HttpGet("status/{status}")]
        public ActionResult GetByStatus(int status)
        {
            //Yet to do 

            if (status > -1)
            {
                return Ok(new List<Project> { new Project() }) ;
            }
            else
                return StatusCode(500, "No projects found on this status");
        }

        [HttpGet("recent/{maxNumber}")]
        public ActionResult GetRecentProjects(int maxNumber)
        {
            //Yet to do 

            if (maxNumber > -1 && maxNumber < 100)
            {
                return Ok(new List<Project> { new Project() });
            }
            else
                return StatusCode(500, "No projects found on this status");
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            //Yet to do 

            return Ok(new List<Project> { new Project() });
        }

        [HttpPost]
        public ActionResult LaunchProject(Project project)
        {
            //Yet to do 

            if (project != null)
                return Ok(new CintResponse());
            else
                return StatusCode(500, "The current project is not launched");
        }
    }
}
