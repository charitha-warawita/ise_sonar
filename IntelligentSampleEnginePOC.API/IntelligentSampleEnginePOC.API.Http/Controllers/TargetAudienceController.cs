﻿using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntelligentSampleEnginePOC.API.Http.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TargetAudienceController : ControllerBase
    {
        ITargetAudienceService _targetAudienceService;

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
    }
}