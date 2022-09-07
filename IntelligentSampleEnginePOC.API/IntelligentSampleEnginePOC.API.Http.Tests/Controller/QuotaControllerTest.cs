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
    public class QuotaControllerTest
    {
        private Mock<ILogger<QuotaController>> _logger;
        private Mock<IQuotaService> _quotaService;
        private QuotaController _quotaController;

        public QuotaControllerTest()
        {
            _logger = new Mock<ILogger<QuotaController>>();
            _quotaService = new Mock<IQuotaService>();
            _quotaController = new QuotaController(_logger.Object, _quotaService.Object);
        }

        [Theory]
        [InlineData(501)]
        public void GetQuota_ReturnsOk(long qtid)
        {
            string jsonString = QuotaTest.GetQuotaJson();
            Quota quota = JsonConvert.DeserializeObject<Quota>(jsonString);
            _quotaService.Setup(repo => repo.GetQuota(qtid)).Returns(new QuotaTest().GetQuota(qtid, quota));
            var result = _quotaController.GetQuota(qtid);
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

        }

        [Theory]
        [InlineData(331)]
        [InlineData(0)]
        public void GetQuota_NotNull(long qtid)
        {
            string jsonString = QuotaTest.GetQuotaJson();
            Quota quota = JsonConvert.DeserializeObject<Quota>(jsonString);
            _quotaService.Setup(repo => repo.GetQuota(qtid)).Returns(new QuotaTest().GetQuota(qtid, quota));
            ActionResult index = (ActionResult)_quotaController.GetQuota(qtid);
            Assert.NotNull(index);
        }

        [Fact]
        public void CreateQuota_ReturnsOk()
        {
            string jsonString = QuotaTest.GetQuotaJson();
            Quota quota = JsonConvert.DeserializeObject<Quota>(jsonString);
            long taid = 104;
            long projectId = 143;
            _quotaService.Setup(repo => repo.CreateQuota(projectId, taid, quota)).Returns(new QuotaTest().CreateQuotaTest(projectId, taid, quota));
            var result = _quotaController.Post(projectId, taid, quota);
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            var qta = ((Quota?)okResult.Value)?.Id;
            Assert.NotNull(qta);
        }



        [Theory]
        [InlineData(501)]
        [InlineData(3333)]
        public void DeleteQuota_ReturnsOk(long qtid)
        {
            string jsonString = QuotaTest.GetQuotaJson();
            Quota quota = JsonConvert.DeserializeObject<Quota>(jsonString);

            _quotaService.Setup(repo => repo.DeleteQuota(qtid)).Returns(new QuotaTest().DeleteQuotaTest(qtid, quota));
            var result = _quotaController.DeleteQuota(qtid);
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);


        }

    }
}
