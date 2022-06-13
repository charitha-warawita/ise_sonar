using IntelligentSampleEnginePOC.UI.Core;
using IntelligentSampleEnginePOC.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace IntelligentSampleEnginePOC.UI.Controllers.v2
{
    public class DashboardController : Controller
    {
        private IProjectVMService _service { get; set; }
        private readonly ILogger<HomeController> _logger;
        public DashboardController(IProjectVMService service, ILogger<HomeController> logger)
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
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "SSL Exception occured - " + ex.Message, ex);
            }
            return View();
        }
    }
}
