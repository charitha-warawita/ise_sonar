using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace IntelligentSampleEnginePOC.API.Http.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:scopes")]
    public class TargetAudienceController : ControllerBase
    {
        ITargetAudienceService _targetAudienceService;

        public TargetAudienceController(ITargetAudienceService taService)
        {
            _targetAudienceService = taService;
        }

        [Authorize]
        [HttpPost("{projectId}")]
        public ActionResult Post(long? projectId, [FromBody] TargetAudience taAudience)
        {
            try
            {
                var resultAudience = _targetAudienceService.CreateTargetAudience(projectId, taAudience);
                if (resultAudience != null)
                {
                    return Ok(resultAudience);
                }
                else
                    return StatusCode(500, "Error occured");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Exception occured - " + ex.Message);
            }
        }
    }
}
