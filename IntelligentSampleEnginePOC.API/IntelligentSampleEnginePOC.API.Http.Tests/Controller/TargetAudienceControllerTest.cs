using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Http.Controllers;
using IntelligentSampleEnginePOC.API.Http.Tests.MockModelData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;

namespace IntelligentSampleEnginePOC.API.Http.Tests.Controller
{
    public class TargetAudienceServiceTest
    {
        private  Mock<ITargetAudienceService> _targetAudienceService;
        private TargetAudienceController _targetAudienceController;
        public TargetAudienceServiceTest()
        {
            _targetAudienceService = new Mock<ITargetAudienceService>();
            _targetAudienceController = new TargetAudienceController(_targetAudienceService.Object);
        }

        [Fact]
        public void Post_ReturnsOk()
        {
            string jsonString = CreateTargetAudiences.GetTargetAudienceJson();
            TargetAudience targetAudience = JsonConvert.DeserializeObject<TargetAudience>(jsonString);
            long projectId = 1;
            _targetAudienceService.Setup(repo => repo.CreateTargetAudience(projectId, targetAudience)).Returns(new CreateTargetAudiences().CreateTargetAudienceTest(projectId, targetAudience));
            var result = _targetAudienceController.Post(projectId, targetAudience);
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            var targetAudienceId = ((TargetAudience?)okResult.Value)?.Id;
            Assert.NotNull(targetAudienceId);          
        }
    }
}
