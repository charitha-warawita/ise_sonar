using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Http.Controllers;
using IntelligentSampleEnginePOC.API.Http.Tests.MockModelData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;

namespace IntelligentSampleEnginePOC.API.Http.Tests.Controller
{
    public class QualificationControllerTest
    {
        private Mock<ILogger<QualificationController>> _logger;
        private Mock<IQualificationService> _qualificationService;
        private QualificationController _qualificationController;

        public QualificationControllerTest()
        {
            _logger = new Mock<ILogger<QualificationController>>();
            _qualificationService = new Mock<IQualificationService>();
            _qualificationController = new QualificationController(_logger.Object, _qualificationService.Object);
        }


        [Theory]
        [InlineData(331)]
        [InlineData(0)]
        public void GetQualification_ReturnsOk( long qid)
        {
            string jsonString = QualificationTest.GetQualificationJson();
            Qualification qualification  = JsonConvert.DeserializeObject<Qualification>(jsonString);
            _qualificationService.Setup(repo => repo.GetQualification(qid)).Returns(new QualificationTest().GetQualification(qid, qualification));
            var result = _qualificationController.GetQualification(qid);
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }



        [Theory]
        [InlineData(331)]
        [InlineData(0)]
        public void GetQualification_NotNull(long qid)
        {
            string jsonString = QualificationTest.GetQualificationJson();
            Qualification qualification = JsonConvert.DeserializeObject<Qualification>(jsonString);
            _qualificationService.Setup(repo => repo.GetQualification(qid)).Returns(new QualificationTest().GetQualification(qid, qualification));
            ActionResult index = (ActionResult)_qualificationController.GetQualification(qid);
            Assert.NotNull(index);
        }

        [Fact]
        public void CreateQualification_ReturnsOk()
        {
            string jsonString = QualificationTest.GetQualificationJson();
            Qualification qualification = JsonConvert.DeserializeObject<Qualification>(jsonString);
            long taid = 104;
            long projectId = 143;
            _qualificationService.Setup(repo => repo.CreateQualification(projectId, taid, qualification)).Returns(new QualificationTest().CreateQualificationTest(projectId,taid, qualification));
            var result = _qualificationController.Post(projectId, taid, qualification);
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            var qual = ((Qualification?)okResult.Value)?.Id;
            Assert.NotNull(qual);
        }
    }
}
