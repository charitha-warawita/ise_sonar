using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Core.Services;
using IntelligentSampleEnginePOC.API.Core.Tests.MockModelData;
using Moq;
using Newtonsoft.Json;

namespace IntelligentSampleEnginePOC.API.Core.Tests.Service
{
    public class ProjectServiceTest
    {
        private readonly Mock<IProjectContext> _projectContext;
        private readonly Mock<ITargetAudienceService> _targetAudienceService;
        private readonly Mock<ProjectValidator> _projectValidator;
        private readonly ProjectService _projectService;
        private readonly Mock<ICintSamplingService> _samplingService;           
          
        public ProjectServiceTest()
        {
            _projectContext = new Mock<IProjectContext>();
            _targetAudienceService = new Mock<ITargetAudienceService>();
            _samplingService = new Mock<ICintSamplingService>();
            _projectValidator = new Mock<ProjectValidator>();
            
            _projectService = new ProjectService(
                _projectContext.Object,
                _targetAudienceService.Object,
                _samplingService.Object,
                _projectValidator.Object);
        }

        [Fact]
        public void CreateProject_ReturnsOk()
        {
            string jsonString = CreateProjects.GetProjectNoTargetAudienceJson();
            Project project = JsonConvert.DeserializeObject<Project>(jsonString);
            _projectContext.Setup(repo => repo.CreateProject(project)).Returns(new CreateProjects().CreateProjectTargetAudience(project));
            var result = _projectService.CreateProject(project);
            Assert.NotNull(result);
        }

        [Fact]
        public void LaunchProject_ReturnsOk()
        {
            string jsonString = CreateProjects.GetProjectJson();
            Project project = JsonConvert.DeserializeObject<Project>(jsonString);
            _projectContext.Setup(repo => repo.CreateProject(project)).Returns(new CreateProjects().CreateProjectTest(project));
            var result = _projectService.LaunchProject(project);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(0, (Status)1)]
        [InlineData(1,(Status)1)]
        public void UpdateProject_ReturnsOk(long projectId, Status status)
        {
            if(projectId < 1)
             Assert.True(projectId < 1);
            var okResult = _projectService.UpdatePrjectStatus(projectId, status);
            Assert.NotNull(okResult);
        }

        [Theory]
        [InlineData(null, 1, null, 1)]
        [InlineData(null, 2, null, 1)]
        [InlineData(null, 2, null, 5)]
        [InlineData(null, 1, null, 2)]
        [InlineData(0, 1, null, 3)]
        [InlineData(null, 1, "Test1", 3)]
        [InlineData(2, 1, "Test2", 3)]
        public void GetAllProjects_ReturnsOk(int? status, int pageNumber, string? searchString, int recordCount)
        {
            _projectContext.Setup(repo => repo.GetProjects(status, pageNumber, searchString, recordCount)).Returns(new MockProjectList().GetTestProjectList(status, pageNumber, searchString, recordCount));
            var result = _projectService.GetProjects(status, pageNumber, searchString, recordCount);           
            Assert.NotNull(result);           
        }

        [Fact]
        public async Task GetCurrentCostAsync_ReturnsOk()
        {
            const int id = 1;

            _samplingService
                .Setup(m => m.GetSurveyIdsAsync(It.IsAny<long>()))
                .ReturnsAsync(new HashSet<long>
                {
                    100,
                    101,
                    102
                });

            _samplingService.SetupSequence(m => m.GetCurrentCostAsync(It.IsAny<long>()))
                .ReturnsAsync(new Cost(100, "USD"))
                .ReturnsAsync(new Cost(150, "USD"))
                .ReturnsAsync(new Cost(50, "EUR"));

            var result = (await _projectService.GetCurrentCostAsync(id))
                .OrderBy(c => c.Currency)
                .ToList();
            
            Assert.True(result.Count == 2);
            Assert.Collection(
                result,
                c => Assert.True(c.Currency == "EUR" && c.Amount == 50),
                c => Assert.True(c.Currency == "USD" && c.Amount == 250));
        }

        [Fact]
        public async Task GetCurrentCostAsync_ReturnsAllExceptions()
        {
            const int id = 1;

            _samplingService
                .Setup(m => m.GetSurveyIdsAsync(It.IsAny<long>()))
                .ReturnsAsync(new HashSet<long>
                {
                    100,
                    101,
                    102
                });

            _samplingService.SetupSequence(m => m.GetCurrentCostAsync(It.IsAny<long>()))
                .ThrowsAsync(new Exception("first"))
                .ThrowsAsync(new Exception("second"))
                .ReturnsAsync(new Cost(1, "EUR"));

            var ex = await Assert.ThrowsAsync<AggregateException>(() => _projectService.GetCurrentCostAsync(id));
            Assert.True(ex.InnerExceptions.Count == 2);
            Assert.Collection(ex.InnerExceptions,
                e => Assert.Equal("first", e.Message),
                e => Assert.Equal("second", e.Message));
        }
    }
}