using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Services
{
    public class ProjectReferenceService : IProjectReferenceService
    {
        private readonly ILogger _logger;
        private readonly IReferenceContext _referenceContext;
        public ProjectReferenceService(ILogger<ProjectReferenceService> logger, IReferenceContext referenceContext)
        {
            _logger = logger;
            _referenceContext = referenceContext;
        }

        public List<Category> GetCategories()
        {
            return _referenceContext.GetCategoriesFromDB();
        }

        public List<Country> GetCountries()
        {
            return _referenceContext.GetCountriesFromDB();
        }

        public List<string> GetProfileCategories()
        {
            return _referenceContext.GetProfileCategoriesFromDB();
        }

        public List<Question> GetQuestions(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
                return _referenceContext.GetAllQuestionsFromDB();
            else
                return _referenceContext.GetQuestionsByCategoryFromDB(categoryName);
        }
    }
}
