using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Core.Services;
using IntelligentSampleEnginePOC.API.Core.Tests.MockModelData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;

namespace IntelligentSampleEnginePOC.API.Core.Tests.Service
{
    public class TargetAudienceServiceTest
    {
        private Mock<ITargetAudienceContext> _targetAudienceContext;
        private TargetAudienceService _targetAudienceService;
        public TargetAudienceServiceTest()
        {
           _targetAudienceContext = new Mock<ITargetAudienceContext>();
           _targetAudienceService = new TargetAudienceService(_targetAudienceContext.Object);
        }

        [Fact]
        public void CreateTargetAudience_Success()
        {
            string jsonString = CreateTargetAudiences.GetTargetAudienceJson();
            TargetAudience targetAudience = JsonConvert.DeserializeObject<TargetAudience>(jsonString);
            long projectId = 1;
            _targetAudienceContext.Setup(repo => repo.CreateTargetAudience(projectId, targetAudience)).Returns(new CreateTargetAudiences().CreateTargetAudienceTest(projectId, targetAudience));
            var result = _targetAudienceService.CreateTargetAudience(projectId, targetAudience);
            Assert.NotNull(result);         
        }
    }
}