using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Http.Controllers;
using IntelligentSampleEnginePOC.API.Http.Tests.MockModelData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace IntelligentSampleEnginePOC.API.Http.Tests.Controller
{
    public class ReferenceControllerTest
    {

        private  Mock<ILogger<ReferenceController>> _logger;
        private  Mock<IProjectReferenceService> _projectReferenceService;
        private ReferenceController _referenceController;
        public ReferenceControllerTest()
        {
            _logger = new Mock<ILogger<ReferenceController>>();
            _projectReferenceService = new Mock<IProjectReferenceService>();
            _referenceController = new ReferenceController(_logger.Object, _projectReferenceService.Object);
        }

        [Fact]
        public void GetCategories_ReturnsOk()
        {
            _projectReferenceService.Setup(repo => repo.GetCategories()).Returns(new Categories().GetTestCategories());
            var result = _referenceController.GetCategories();
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            //exceptions
            // function call check
            // check function call when breaks
            
        }
        [Fact]
        public void GetCategories_NotNull()
        {
            _projectReferenceService.Setup(repo => repo.GetCategories()).Returns(new Categories().GetTestCategories());
            ActionResult index = (ActionResult)_referenceController.GetCategories();
            Assert.NotNull(index);
        }

        [Fact]
        public void GetCountries_ReturnsOk()
        {
            _projectReferenceService.Setup(repo => repo.GetCountries()).Returns(new Countries().GetTestCountries());
            var result = _referenceController.GetCountries();
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

        }
        [Fact]
        public void GetCountries_NotNull()
        {
            _projectReferenceService.Setup(repo => repo.GetCountries()).Returns(new Countries().GetTestCountries());
            ActionResult index = (ActionResult)_referenceController.GetCountries();
            Assert.NotNull(index);
        }

        [Fact]
        public void GetProfileCategories_ReturnsOk()
        {
            _projectReferenceService.Setup(repo => repo.GetProfileCategories()).Returns(new ProfileCategories().GetTestProfileCategories());
            var result = _referenceController.GetProfileCategories();
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

        }
        [Fact]
        public void GetProfileCategories_NotNull()
        {
            _projectReferenceService.Setup(repo => repo.GetProfileCategories()).Returns(new ProfileCategories().GetTestProfileCategories());
            ActionResult index = (ActionResult)_referenceController.GetProfileCategories();
            Assert.NotNull(index);
        }

        [Theory]
        [InlineData("Travel")]
        [InlineData("Finance")]
        [InlineData("Invalid Test")]
        [InlineData("")]
        public void GetQuestions_ReturnsOk(string categoryName)
        {
            _projectReferenceService.Setup(repo => repo.GetQuestions(categoryName)).Returns(new Questions().GetTestQuestions(categoryName));
            var result = _referenceController.GetQuestions(categoryName);
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

        }

        [Theory]
        [InlineData("Travel")]
        public void GetQuestions_ReturnsStatusCode500(string categoryName)
        {
            _projectReferenceService.Setup(repo => repo.GetQuestions(categoryName)).Throws(new Exception("Test Exception"));
            var result = _referenceController.GetQuestions(categoryName);
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Equal("Exception occured - Test Exception", objectResult.Value);
        }

        [Theory]
        [InlineData("Travel")]
        [InlineData("Finance")]
        [InlineData("Invalid Test")]
        [InlineData("")]
        public void GetQuestions_NotNull(string categoryName)
        {
            _projectReferenceService.Setup(repo => repo.GetQuestions(categoryName)).Returns(new Questions().GetTestQuestions(categoryName));
            ActionResult index = (ActionResult)_referenceController.GetQuestions(categoryName);
            Assert.NotNull(index);
        }
       
    }
}
