using IntelligentSampleEnginePOC.API.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntelligentSampleEnginePOC.API.Http.Controllers
{
    [Route("api/[controller]/project")]
    [ApiController]
    public class ReferenceController : ControllerBase
    {
        ILogger _logger;
        IProjectReferenceService _referenceService;

        public ReferenceController(ILogger<ReferenceController> logger, IProjectReferenceService referenceService)
        {
            _logger = logger;
            _referenceService = referenceService;
        }

        [HttpGet("categories")]
        public ActionResult GetCategories()
        {
            try
            {
                var categoriesList = _referenceService.GetCategories();
                return Ok(categoriesList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Exception occured - " + ex.Message);
            }
        }
    }
}
