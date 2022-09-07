using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Http.Controllers;
using IntelligentSampleEnginePOC.API.Http.Tests.MockModelData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;

namespace IntelligentSampleEnginePOC.API.Http.Tests.Controller
{
    public class ProjectControllerTest
    {
        private  Mock<IProjectService> _projectService;
        private ProjectController _projectController;
        private Mock<ICintSamplingService> _samplingService;
        private Mock<ITargetAudienceService> _targetAudienceService;
        private Mock<ILogger<ProjectController>> _logger;
        public ProjectControllerTest()
        {
            _projectService = new Mock<IProjectService>();
            _samplingService = new Mock<ICintSamplingService>();
            _targetAudienceService = new Mock<ITargetAudienceService>();
            _logger = new Mock<ILogger<ProjectController>>();
            _projectController = new ProjectController(_logger.Object, _projectService.Object, _targetAudienceService.Object);
        }

        [Fact]
        public async void PostProject_ReturnsOk()
        {
            string jsonString = CreateProjects.GetProjectJson();
            Project project = JsonConvert.DeserializeObject<Project>(jsonString);
            _projectService.Setup(repo => repo.CreateProject(project)).ReturnsAsync(new CreateProjects().CreateProjectTest(project));
            var result = await _projectController.Post(project);
            Assert.NotNull(result);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            var projectId = ((Project?)okResult.Value)?.Id;
            Assert.NotNull(projectId);
          
        }

        [Fact]
        public async void LaunchProject_ReturnsOk()
        {
            string jsonString = CreateProjects.GetProjectJson();
            Project project = JsonConvert.DeserializeObject<Project>(jsonString);
            _projectService.Setup(repo => repo.LaunchProject(project)).ReturnsAsync(new CreateProjects().CreateProjectTest(project));
            var result = await _projectController.Launch(project);
            Assert.NotNull(result);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void UpdateProject_ReturnsOk()
        {
            string jsonString = CreateProjects.GetProjectJson();
            Project project = JsonConvert.DeserializeObject<Project>(jsonString);
            var okResult = _projectController.UpdateProject(project);
            Assert.NotNull(okResult);          
        }

        /*[Fact]
        public void GetProjectById_ReturnsOk()
        {
            var okResult =  _projectController.GetById(1);
            Assert.NotNull(okResult);
            Assert.Equal(200, ((StatusCodeResult)okResult).StatusCode);
        }*/

        [Theory]
        [InlineData(null,1,null,1)]
        [InlineData(null, 2, null, 1)]
        [InlineData(null, 2, null, 5)]
        [InlineData(null, 1, null,2)]
        [InlineData(0, 1, null, 3)]
        [InlineData(null, 1, "Test1", 3)]
        [InlineData(2, 1, "Test2", 3)]
        public async void GetAllProjects_ReturnsOk(int? status,int pageNumber, string? searchString, int recordCount)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            recordCount= recordCount <= 0 ? 10 : recordCount;
            _projectService.Setup(repo => repo.GetProjects(status, pageNumber, searchString, recordCount)).Returns(Task.FromResult(new MockProjectList().GetTestProjectList(status, pageNumber, searchString, recordCount)));
            var result = await _projectController.GetProjects(status, pageNumber, searchString, recordCount);
            long pageRecordCount = ((ProjectList)((ObjectResult)result).Value).TotalItems;
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((pageRecordCount < recordCount) || (recordCount == pageRecordCount), "paging check");
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);            
        }


    }
}
