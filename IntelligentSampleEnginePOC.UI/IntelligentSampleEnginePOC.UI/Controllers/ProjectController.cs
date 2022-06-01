using IntelligentSampleEnginePOC.UI.Core;
using IntelligentSampleEnginePOC.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntelligentSampleEnginePOC.UI.Controllers
{
    public class ProjectController : Controller
    {
        private IProjectVMService _service { get; set; }
        public ProjectController(IProjectVMService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string status, string searchString)
        {
            var result = await _service.GetProjectVM(status, searchString);
            return View(result);
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
            if (ModelState.IsValid)
            {
                if(project != null)
                {
                    project.Id = Guid.NewGuid();
                    project.LastUpdate = DateTime.UtcNow;
                    if (project.User != null)
                        project.User.Id = Guid.NewGuid();
                    if(project.TargetAudiences.Any())
                    {
                        foreach(var item in project.TargetAudiences)
                        {
                            item.Id = Guid.NewGuid();
                        }
                    }
                }
                var result = await _service.CreateProject(project);

                if(submitButton == "Launch")
                {
                    //Write something to launch project
                }


                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }
    }
}
