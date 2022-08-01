using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Http.Tests.MockModelData
{
    public class ProfileCategories
    {
        public List<string> GetTestProfileCategories()
        {
            var profileCategories = new List<string>();
            profileCategories.Add("Automotive");
            profileCategories.Add("Country specific");
            profileCategories.Add("Finance");
            return profileCategories;
        }
    }
   
}
