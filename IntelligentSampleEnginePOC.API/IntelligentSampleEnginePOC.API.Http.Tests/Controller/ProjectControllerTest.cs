using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Http.Controllers;
using Moq;

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
        public void GetById_ReturnsOk()
        {
            var okResult =  _projectController.GetById("1");
            Assert.NotNull(okResult);
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)okResult).StatusCode);
        }

        [Fact]
        public void GetAll_ReturnsOk()
        {
            var okResult = _projectController.GetAll(1,"Test",2);
            Assert.NotNull(okResult);
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)okResult).StatusCode);
        }


    }
}
