using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Services;
using IntelligentSampleEnginePOC.API.Core.Tests.MockModelData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace IntelligentSampleEnginePOC.API.Core.Tests.Service
{
    public class ProjectReferenceServiceTest
    {
        private Mock<ILogger<ProjectReferenceService>> _logger;
        private Mock<IReferenceContext> _referenceContext;
        private ProjectReferenceService _projectReferenceService;

        public ProjectReferenceServiceTest()
        {
            _logger = new Mock<ILogger<ProjectReferenceService>>();
            _referenceContext = new Mock<IReferenceContext>();
            _projectReferenceService = new ProjectReferenceService(_logger.Object, _referenceContext.Object);
        }

        [Fact]
        public void GetCategories_ReturnsOk()
        {
            _referenceContext.Setup(repo => repo.GetCategoriesFromDB()).Returns(new Categories().GetTestCategories());
            var result = _projectReferenceService.GetCategories();
            Assert.NotNull(result);        
        }
        [Fact]
        public void GetCategories_NotNull()
        {
            _referenceContext.Setup(repo => repo.GetCategoriesFromDB()).Returns(new Categories().GetTestCategories());
            var index = _projectReferenceService.GetCategories();
            Assert.NotNull(index);
        }

        [Fact]
        public void GetCountries_ReturnsOk()
        {
            _referenceContext.Setup(repo => repo.GetCountriesFromDB()).Returns(new Countries().GetTestCountries());
            var result = _projectReferenceService.GetCountries();
            Assert.NotNull(result);        

        }
        [Fact]
        public void GetCountries_NotNull()
        {
            _referenceContext.Setup(repo => repo.GetCountriesFromDB()).Returns(new Countries().GetTestCountries());
            var index = _projectReferenceService.GetCountries();
            Assert.NotNull(index);
        }

        [Fact]
        public void GetProfileCategories_ReturnsOk()
        {
            _referenceContext.Setup(repo => repo.GetProfileCategoriesFromDB()).Returns(new ProfileCategories().GetTestProfileCategories());
            var result = _projectReferenceService.GetProfileCategories();
            Assert.NotNull(result);         
        }
        [Fact]
        public void GetProfileCategories_NotNull()
        {
            _referenceContext.Setup(repo => repo.GetProfileCategoriesFromDB()).Returns(new ProfileCategories().GetTestProfileCategories());
            var index = _projectReferenceService.GetProfileCategories();
            Assert.NotNull(index);
        }

        [Theory]
        [InlineData("Travel")]
        [InlineData("Finance")]      
        public void GetQuestions_ReturnsOk(string categoryName)
        {
            _referenceContext.Setup(repo => repo.GetQuestionsByCategoryFromDB(categoryName)).Returns(new Questions().GetTestQuestions(categoryName));
            var result = _projectReferenceService.GetQuestions(categoryName);
            Assert.NotNull(result);         
        }

        [Theory]
        [InlineData("Travel")]
        [InlineData("Finance")]   
        public void GetQuestions_NotNull(string categoryName)
        {
            _referenceContext.Setup(repo => repo.GetQuestionsByCategoryFromDB(categoryName)).Returns(new Questions().GetTestQuestions(categoryName));
            var index = _projectReferenceService.GetQuestions(categoryName);
            Assert.NotNull(index);
        }
    }
}