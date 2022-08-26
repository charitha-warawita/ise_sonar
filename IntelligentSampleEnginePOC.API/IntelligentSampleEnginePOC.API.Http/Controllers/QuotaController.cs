using IntelligentSampleEnginePOC.API.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace IntelligentSampleEnginePOC.API.Http.Controllers

{
    [Route("api/[controller]/project")]
    [ApiController]
    public class QuotaController : ControllerBase
    {
        ILogger _logger;
        IQuotaService _quotaService;

        public QuotaController(ILogger<QuotaController> logger, IQuotaService quotaService)
        {
            _logger = logger;
            _quotaService = quotaService;
        }
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
    }
}
