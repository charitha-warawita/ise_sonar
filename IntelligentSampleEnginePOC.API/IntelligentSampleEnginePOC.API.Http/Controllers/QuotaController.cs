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
    public class QuotaController : ControllerBase
    {
        ILogger _logger;
        IQuotaService _quotaService;

        public QuotaController(ILogger<QuotaController> logger, IQuotaService quotaService)
        {
            _logger = logger;
            _quotaService = quotaService;
        }

        [Authorize]
        [HttpGet("quota")]
        public ActionResult GetQuota(long qtid)
        {
            try
            {
                var quotaList = _quotaService.GetQuota(qtid);
                return Ok(quotaList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Exception occured - " + ex.Message);
            }
        }

        [Authorize]
        [HttpPost("{taid}")]
        public ActionResult Post(long projectId, long? taid, [FromBody] Core.Model.Quota qtaData)
        {
            try
            {
                var resultQuota = _quotaService.CreateQuota(projectId, taid, qtaData);
                if (resultQuota != null)
                {
                    return Ok(resultQuota);
                }
                else
                    return StatusCode(500, "Error occured");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Exception occured - " + ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{qtid}")]
        public ActionResult DeleteQuota(long qtid)
        {
            try
            {
                var resultDeleted = _quotaService.DeleteQuota(qtid);
                if (resultDeleted > 0)
                {
                    return Ok("Record deleted");
                }
                else
                    return StatusCode(500, "No Records Found");

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Exception occured - " + ex.Message);
            }
        }
    }
}
