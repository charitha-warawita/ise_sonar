using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface IProjectReferenceService
    {
        List<Category> GetCategories();
        List<Country> GetCountries();
        List<string> GetProfileCategories();
        List<Question> GetQuestions(string categoryName);
    }
}
