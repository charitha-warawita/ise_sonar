using IntelligentSampleEnginePOC.API.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntelligentSampleEnginePOC.API.Http.Controllers
{
    [Route("api/[controller]/project")]
    [ApiController]

   

    public class QualificationController : ControllerBase
    {
        ILogger _logger;
        IQualificationService _qualificationService;

        public QualificationController(ILogger<QualificationController> logger, IQualificationService qualificationService)
        {
            _logger = logger;
            _qualificationService = qualificationService;
        }

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
