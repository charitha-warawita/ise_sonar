using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface IReferenceContext
    {
        List<Category> GetCategoriesFromDB();
        List<Country> GetCountriesFromDB();
        List<string> GetProfileCategoriesFromDB();
        List<Question> GetAllQuestionsFromDB();
        List<Question> GetQuestionsByCategoryFromDB(string categoryName);
    }
}