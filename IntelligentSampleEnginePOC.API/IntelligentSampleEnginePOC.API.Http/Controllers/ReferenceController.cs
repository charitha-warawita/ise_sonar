using IntelligentSampleEnginePOC.API.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace IntelligentSampleEnginePOC.API.Http.Controllers
{
    [Route("api/[controller]/project")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:scopes")]
    public class ReferenceController : ControllerBase
    {
        ILogger _logger;
        IProjectReferenceService _referenceService;

        public ReferenceController(ILogger<ReferenceController> logger, IProjectReferenceService referenceService)
        {
            _logger = logger;
            _referenceService = referenceService;
        }

        [Authorize]
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

        [Authorize]
        [HttpGet("countries")]
        public ActionResult GetCountries()
        {
            try
            {
                var countriesList = _referenceService.GetCountries();
                return Ok(countriesList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Exception occured - " + ex.Message);
            }
        }

        [Authorize]
        [HttpGet("ProfileCategories")]
        public ActionResult GetProfileCategories()
        {
            try
            {
                var profCategories = _referenceService.GetProfileCategories();
                return Ok(profCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Exception occured - " + ex.Message);
            }
        }

        [Authorize]
        [HttpGet("questions")]
        public ActionResult GetQuestions(string? category)
        {
            try
            {
                var qas = string.IsNullOrEmpty(category) ? _referenceService.GetQuestions(string.Empty) : _referenceService.GetQuestions(category);
                return Ok(qas);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Exception occured - " + ex.Message);
            }
        }
    }
}
