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
            return View(project);
        }

        // POST: Project/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.CreateProject(project);
                /*_context.Add(movie);
                await _context.SaveChangesAsync();*/
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }
    }
}
