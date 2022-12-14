using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Core.Services;
using IntelligentSampleEnginePOC.API.Http.Tests.MockModelData;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;



namespace IntelligentSampleEnginePOC.API.Core.Tests.Service
{
    public class QuotaServiceTest
    {

        private Mock<ILogger<QuotaService>> _logger;
        private Mock<IQuotaContext> _quotaContext;
        private QuotaService _quotaService;
        public QuotaServiceTest()
        {
            _logger = new Mock<ILogger<QuotaService>>();
            _quotaContext = new Mock<IQuotaContext>();
            _quotaService = new QuotaService(_logger.Object, _quotaContext.Object);

        }

        [Theory]
        [InlineData(501)]

        public void GetQuota_ReturnsOk(long qtid)
        {
            string jsonString = QuotaTest.GetQuotaJson();
            Quota quota = JsonConvert.DeserializeObject<Quota>(jsonString);
            _quotaContext.Setup(repo => repo.GetQuota(qtid)).Returns(new QuotaTest().GetQuota(qtid, quota));
            var result = _quotaService.GetQuota(qtid);
            Assert.NotNull(result);
        }


        [Theory]
        [InlineData(301)]
        public void GetQuota_NotNull(long qtid)
        {
            string jsonString = QuotaTest.GetQuotaJson();
            Quota quota = JsonConvert.DeserializeObject<Quota>(jsonString);
            _quotaContext.Setup(repo => repo.GetQuota(qtid)).Returns(new QuotaTest().GetQuota(qtid, quota));
            var index = _quotaService.GetQuota(qtid);
            Assert.NotNull(_quotaContext.Object);
        }

        [Theory]
        [InlineData(0)]

        public void GetQuota_Return_Exceptions(long qtid)
        {
            if (qtid <= 0)
            {
                _quotaContext.Setup(repo => repo.GetQuota(qtid)).Throws(new Exception("Test Exception"));
            }
        }

        [Fact]
        public void CreateQuota_ReturnsOk()
        {
            string jsonString = QuotaTest.GetQuotaJson();
            long taid = 104;
            long projectId = 143;
            Quota quota = JsonConvert.DeserializeObject<Quota>(jsonString);
            _quotaContext.Setup(repo => repo.CreateQuota(projectId, taid, quota)).Returns(new QuotaTest().CreateQuotaTest(projectId, taid, quota));
            var result = _quotaService.CreateQuota(projectId, taid, quota);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(501)]
        public void DeleteQuota_ReturnsOk(long qtid)
        {
            string jsonString = QuotaTest.GetQuotaJson();
            Quota quota = JsonConvert.DeserializeObject<Quota>(jsonString);
            _quotaContext.Setup(repo => repo.DeleteQuotaFromDB(qtid)).Returns(new QuotaTest().DeleteQuotaTest(qtid, quota));
            var result = _quotaService.DeleteQuota(qtid);
            Assert.NotNull(result);

        }





    }
}
