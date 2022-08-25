using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Core.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntelligentSampleEnginePOC.API.Http.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TargetAudienceController : ControllerBase
    {
        readonly ITargetAudienceService _targetAudienceService;

        public TargetAudienceController(ITargetAudienceService taService)
        {
            _targetAudienceService = taService;
        }

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

        [HttpGet]
        public ActionResult Get([FromQuery] int page, [FromQuery] int pageSize = 5, [FromQuery] long? projectId = null)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Invalid paging");
            
            var result = new PagedResult<TargetAudience>();
            try
            {
                if (projectId.HasValue)
                {
                    result = _targetAudienceService.GetAllByProjectId(projectId.Value, page, pageSize);
                }
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
            
            return Ok(result);
        }
    }
}
