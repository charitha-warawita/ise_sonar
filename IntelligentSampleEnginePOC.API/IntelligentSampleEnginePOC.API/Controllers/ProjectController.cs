using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Core.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IntelligentSampleEnginePOC.API.Controllers
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
            //Yet to do
            var resultProject = _projectService.CreateProject(project);

            if (resultProject != null)
            {
                return Ok(resultProject);
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

        [HttpGet]
        public ActionResult GetAll(int? status, string? searchString, int? recentCount)
        {
            //Yet to do

            IEnumerable<Project> projects = new List<Project>
                {
                    new Project() { Id = Guid.NewGuid(), Name = "Your ongoing diary study", Status = Status.Draft, LastUpdate = DateTime.Today.AddDays(-10) },
                    new Project() { Id = Guid.NewGuid(), Name = "Rewarding you every day", Status = Status.Created, LastUpdate = DateTime.Today.AddDays(-20) },
                    new Project() { Id = Guid.NewGuid(), Name = "Drinks survey", Status = Status.Live, LastUpdate = DateTime.Today.AddDays(-30) },
                    new Project() { Id = Guid.NewGuid(), Name = "All about cars", Status = Status.Paused, LastUpdate = DateTime.Today.AddDays(-40) },
                    new Project() { Id = Guid.NewGuid(), Name = "Stocks Study", Status = Status.Complete, LastUpdate = DateTime.Today.AddDays(-50) },
                    new Project() { Id = Guid.NewGuid(), Name = "Fast track you rewards", Status = Status.Closed, LastUpdate = DateTime.Today.AddDays(-60) },
                    new Project() { Id = Guid.NewGuid(), Name = "Rewards and surveys", Status = Status.Halted, LastUpdate = DateTime.Today.AddDays(-70) },
                    new Project() { Id = Guid.NewGuid(), Name = "Machine study", Status = Status.Paused, LastUpdate = DateTime.Today.AddDays(-80) },
                };

            if (status != null && status > -1 && status < 7)
                projects = projects.Where(x => (int)x.Status == status);
            if (!string.IsNullOrEmpty(searchString))
                projects = projects.Where(x => x.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase));

            return Ok(projects);
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
