using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Http.Tests.MockModelData
{
    public class Categories
    {
        public List<Category> GetTestCategories()
        {
            var categories = new List<Category>();
            categories.Add(new Category()
            {
                Id = 8,
                Name = "Automotive"
            });
            categories.Add(new Category()
            {
                Id = 32,
                Name = "Beverages - alcoholic"
            });
            return categories;
        }

        public Project CreateProjectTest(Project project)
        {
            project.Id = 1;
            return project;
        }
    }
   
}
