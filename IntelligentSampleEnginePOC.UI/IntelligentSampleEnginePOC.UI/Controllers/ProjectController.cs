using IntelligentSampleEnginePOC.UI.Core;
using IntelligentSampleEnginePOC.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace IntelligentSampleEnginePOC.UI.Controllers
{
    public class ProjectController : Controller
    {
        private IProjectVMService _service { get; set; }
        private readonly ILogger<HomeController> _logger;
        public ProjectController(IProjectVMService service, ILogger<HomeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string status, string searchString)
        {
            try
            {
                var result = await _service.GetProjectVM(status, searchString);
                _logger.Log(LogLevel.Information, "Prejects returned: " + JsonSerializer.Serialize(result));
                return View(result);
            }
            catch(Exception ex)
            {
                _logger.Log(LogLevel.Error, "SSL Exception occured - " + ex.Message, ex);
            }
            return View();
        }

        public IActionResult Create()
        {
            Project project = new Project();
            // var projectTemplate = await _service.GetEmptyProjectTemplate();
            return View(project);
        }

        /*public async IActionResult Edit(string Id)
        {
            // var result = await _service.GetProject(Id);
            //Do the magic
            return View();
        }*/

        // POST: Project/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project, string submitButton)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (project != null)
                    {
                        project.Id = Guid.NewGuid();
                        project.LastUpdate = DateTime.UtcNow;
                        if (project.User != null)
                            project.User.Id = Guid.NewGuid();
                        if (project.TargetAudiences.Any())
                        {
                            foreach (var item in project.TargetAudiences)
                            {
                                item.Id = Guid.NewGuid();
                            }
                        }
                    }
                    var result = await _service.CreateProject(project);

                    if (submitButton == "Launch")
                    {
                        _service.LaunchProject(project);
                        //Write something to launch project
                    }


                    return RedirectToAction(nameof(Index));
                }
                return View(project);
            }
            catch(Exception ex)
            {
                _logger.Log(LogLevel.Error, "Exception occured - " + ex.Message, ex);

            }
            return View();
        }
    }
}
