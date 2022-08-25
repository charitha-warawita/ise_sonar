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
        private Mock<IProjectContext> _projectContext;
        private Mock<ITargetAudienceService> _targetAudienceService;
        private Mock<ProjectValidator> _projectValidator;
        private ProjectService _projectService;
        private Mock<ICintSamplingService> _samplingService;           
          
        public ProjectServiceTest()
        {
            _projectContext = new Mock<IProjectContext>();
            _targetAudienceService = new Mock<ITargetAudienceService>();
            _samplingService = new Mock<ICintSamplingService>();
            _projectValidator = new Mock<ProjectValidator>();
            _projectService = new ProjectService(_projectContext.Object,_targetAudienceService.Object, _samplingService.Object,_projectValidator.Object);
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

    }
}