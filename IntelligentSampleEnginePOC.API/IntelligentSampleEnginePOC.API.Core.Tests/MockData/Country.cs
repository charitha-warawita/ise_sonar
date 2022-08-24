using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Tests.MockModelData
{
    public class Countries
    {
        public List<Country> GetTestCountries()
        {
            var Countries = new List<Country>();
            Countries.Add(new Country()
            {
                Id = 8,
                Name = "Automotive"
            });
            Countries.Add(new Country()
            {
                Id = 32,
                Name = "Beverages - alcoholic"
            });
            return Countries;
        }
    }
   
}
