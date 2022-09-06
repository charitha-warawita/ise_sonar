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
    public class QualificationController : ControllerBase
    {
        ILogger _logger;
        IQualificationService _qualificationService;

        public QualificationController(ILogger<QualificationController> logger, IQualificationService qualificationService)
        {
            _logger = logger;
            _qualificationService = qualificationService;
        }

        [Authorize]
        [HttpGet("{qid}")]
        public ActionResult GetQualification( long qid)
        {
            try
            {
                var qualificationList = _qualificationService.GetQualification(qid);
                return Ok(qualificationList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Exception occured - " + ex.Message);
            }
        }

        [Authorize]
        [HttpPost("{taid}")]
        public ActionResult Post(long projectId ,long? taid, [FromBody] Core.Model.Qualification qualData)
        {
            try
            {
                var resultQualification = _qualificationService.CreateQualification(projectId,taid, qualData);
                if (resultQualification != null)
                {
                    return Ok(resultQualification);
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
        [HttpDelete("{qid}")]
        public ActionResult DeleteQualification(long qid)
        {
            try
            {
                var resultDeleted = _qualificationService.DeleteQualification( qid);
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
