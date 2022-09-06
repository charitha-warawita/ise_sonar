using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Core.Services;
using IntelligentSampleEnginePOC.API.Http.Controllers;
using IntelligentSampleEnginePOC.API.Http.Tests.MockModelData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;

namespace IntelligentSampleEnginePOC.API.Http.Tests.Controller
{
    public class QuotaServiceControllerTest
    {
  
        private Mock<ILogger<QuotaService>> _logger;
        private Mock<IQuotaContext> _quotaContext;
        private QuotaService _quotaService;
        public QuotaServiceControllerTest()
        {
            _logger = new Mock<ILogger<QuotaService>>();
            _quotaContext = new Mock<IQuotaContext>();
            _quotaService = new QuotaService(_logger.Object, _quotaContext.Object);

        }

            [Theory]
        [InlineData(501)]
        
        public void GetQuestions_ReturnsOk(long qtid)
        {
            string jsonString = QuotaTest.GetQuotaJson();
            Quota quota = JsonConvert.DeserializeObject<Quota>(jsonString);
            _quotaContext.Setup(repo => repo.GetQuota(qtid)).Returns(new QuotaTest().GetQuota(qtid, quota));
            var result = _quotaService.GetQuota(qtid);
            Assert.NotNull(result);
        }

    }
}
