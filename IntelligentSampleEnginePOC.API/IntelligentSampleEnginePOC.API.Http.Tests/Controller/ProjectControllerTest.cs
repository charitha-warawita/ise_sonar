using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Http.Controllers;
using IntelligentSampleEnginePOC.API.Http.Tests.MockModelData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;

namespace IntelligentSampleEnginePOC.API.Http.Tests.Controller
{
    public class ProjectControllerTest
    {
        private  Mock<IProjectService> _projectService;
        private ProjectController _projectController;
        public ProjectControllerTest()
        {
            _projectService = new Mock<IProjectService>();
            _projectController = new ProjectController(_projectService.Object);
        }

        [Fact]
        public void Post_ReturnsOk()
        {
            string jsonString = CreateProjects.GetProjectJson();
            Project project = JsonConvert.DeserializeObject<Project>(jsonString);
            _projectService.Setup(repo => repo.CreateProject(project)).Returns(new CreateProjects().CreateProjectTest(project));
            var result = _projectController.Post(project);
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            var projectId = ((Project?)okResult.Value)?.Id;
            Assert.NotNull(projectId);
          
        }

        [Fact]
        public void Launch_ReturnsOk()
        {
            string jsonString = CreateProjects.GetProjectJson();
            Project project = JsonConvert.DeserializeObject<Project>(jsonString);
            var okResult = _projectController.Launch(project);
            Assert.NotNull(okResult);
            Assert.Equal(200, ((StatusCodeResult)okResult.Result).StatusCode);
        }

        [Fact]
        public void Update_ReturnsOk()
        {
            string jsonString = CreateProjects.GetProjectJson();
            Project project = JsonConvert.DeserializeObject<Project>(jsonString);
            var okResult = _projectController.Launch(project);
            Assert.NotNull(okResult);
            Assert.Equal(200, ((StatusCodeResult)okResult.Result).StatusCode);
        }

        [Fact]
        public void GetById_ReturnsOk()
        {
            var okResult =  _projectController.GetById("1");
            Assert.NotNull(okResult);
            Assert.Equal(200, ((StatusCodeResult)okResult).StatusCode);
        }

        [Fact]
        public void GetAll_ReturnsOk()
        {
            var okResult = _projectController.GetProjects(1,"Test",2);
            Assert.NotNull(okResult);
            Assert.Equal(200, ((StatusCodeResult)okResult).StatusCode);
        }


    }
}
