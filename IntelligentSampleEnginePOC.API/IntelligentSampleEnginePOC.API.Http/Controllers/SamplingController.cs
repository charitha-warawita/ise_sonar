using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace IntelligentSampleEnginePOC.API.Http.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:scopes")]
    public class SamplingController : ControllerBase
    {
        ICintSamplingService _cintSamplingService;

        public SamplingController(ICintSamplingService samplingService)
        {
            _cintSamplingService = samplingService;
        }

        [Authorize]
        [HttpPost("CintRequests")]
        public async Task<ActionResult> CintRequests([FromBody] Project project)
        {
            try
            {
                var result = _cintSamplingService.ConvertToCintRequests(project);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                    return StatusCode(500, "Exception occured");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Exception occured - " + ex.Message);
            }
        }
    }
}
