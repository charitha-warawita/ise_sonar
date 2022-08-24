using IntelligentSampleEnginePOC.API.Core.Cint;
using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Tests.Cint
{
    public class CintCustomTransformTests
    {
        private readonly ISpecTransform specTransform;

        public CintCustomTransformTests()
        {
            specTransform = new CintCustomTransform();
        }

        [Fact]
        public void ProjectToCintTransform1TA2CountriesVaryingQualsQuotas_ReturnSuccess()
        {
            var project = LoadFullProject();
            var cintRequests = specTransform.TransformIseRequestToCintRequests(project);
            Assert.NotNull(cintRequests);
            Assert.Equal(2, cintRequests.Count);
            Assert.Collection(cintRequests,
                item => Assert.Equal("UK", item.CountryName),
                item => Assert.Equal("India", item.CountryName));

            var cintRequest = cintRequests.FirstOrDefault().Request;
            Assert.NotNull(cintRequest);
            Assert.Equal(3, cintRequest.quotaGroups.Count);
            Assert.Equal(4, cintRequest.quotaGroups[0].Quotas.Count);
            Assert.Equal(1, cintRequest.quotaGroups[1].Quotas.Count);
            Assert.Equal(1, cintRequest.quotaGroups[2].Quotas.Count);
        }

        [Fact]
        public void ProjectToCintTransform2TA3countriesEach_ReturnSuccess()
        {
            var project = LoadProjectWith2TA3countriesEach();
            var cintRequests = specTransform.TransformIseRequestToCintRequests(project);
            Assert.NotNull(cintRequests);
            Assert.Equal(6, cintRequests.Count);
            Assert.Collection(cintRequests,
                item => Assert.Equal("UK", item.CountryName),
                item => Assert.Equal("Sweden", item.CountryName),
                item => Assert.Equal("Germany", item.CountryName),
                item => Assert.Equal("Denmark", item.CountryName),
                item => Assert.Equal("Finland", item.CountryName),
                item => Assert.Equal("Norway", item.CountryName));

            var count = 0;
            foreach(var item in cintRequests)
            {
                count++;
                switch (count)
                {
                    case 1:
                    case 2:
                    case 3:
                        Assert.Equal(0, item.Request.quotaGroups.Count);
                        break;
                    case 4:
                    case 5:
                    case 6:
                        Assert.Equal(2, item.Request.quotaGroups.Count);
                        Assert.Equal(1, item.Request.quotaGroups[0].Quotas.Count);
                        Assert.Equal(1, item.Request.quotaGroups[1].Quotas.Count);
                        break;
                    default:
                        break;
                }
            }
        }

        [Fact]
        public void ProjectToCintWithNullProject_ThrowException()
        {
            var exception = Assert.Throws<ArgumentNullException>(
                () => specTransform.TransformIseRequestToCintRequests(null)
            );
            Assert.Equal("Value cannot be null. (Parameter 'No project found to convert to CintRequests')", exception.Message);
        }

        [Fact]
        public void ProjectToCintWithNoTA_ReturnNoCintRequests()
        {
            var project = new Project
            {
                Id = 1,
                Name = "Test",
                FieldingPeriod = 60,
                StartDate = new DateTime(2023, 1, 1),
                User = new User { Name = "gopal", Email = "gopal.ven@kk.com" },
                Reference = "Max01",
                TargetAudiences = new List<TargetAudience>()
            };

            var cintRequests = specTransform.TransformIseRequestToCintRequests(project);
            Assert.NotNull(cintRequests);
            Assert.False(cintRequests.Any());
        }

        [Fact]
        public void ProjectToCintWithNullTA_ReturnNoCintRequests()
        {
            var project = new Project
            {
                Id = 1,
                Name = "Test",
                FieldingPeriod = 60,
                StartDate = new DateTime(2023, 1, 1),
                User = new User { Name = "gopal", Email = "gopal.ven@kk.com" },
                Reference = "Max01"
            };

            var cintRequests = specTransform.TransformIseRequestToCintRequests(project);
            Assert.NotNull(cintRequests);
            Assert.False(cintRequests.Any());
        }

        [Fact]
        public void ProjecttoCintWithTAButNoCountryQual_ThrowsException()
        {
            // 2 Target Audiences- Second one do not contain country qualification
            var iseJson = "{\"tempId\":0,\"name\":\"Test survey 01\",\"reference\":\"MAX001\",\"startDate\":\"2023-01-01\",\"fieldingPeriod\":60,\"status\":0,\"categories\":[5],\"user\":{\"id\":0,\"name\":\"Gopal\",\"email\":\"gopal.ven@kk.com\"},\"errors\":[],\"targetAudiences\":[{\"tempId\":1,\"name\":\"TA001\",\"audienceNumber\":1,\"estimatedIR\":100,\"estimatedLOI\":5,\"costPerInterview\":2,\"cptg\":2000,\"limit\":1000,\"testingUrl\":\"http://testurl.co\",\"liveUrl\":\"http://liveurl.co\",\"qualifications\":[{\"tempId\":1,\"order\":1,\"logicalDecision\":\"AND\",\"NumberOfRequiredConditions\":0,\"IsActive\":true,\"question\":{\"id\":10002,\"name\":\"Country\",\"text\":\"Country\",\"categoryName\":\"Standard\",\"variables\":[{\"id\":1,\"name\":\"UK\",\"selected\":true},{\"id\":2,\"name\":\"Sweden\",\"selected\":true},{\"id\":3,\"name\":\"Germany\",\"selected\":true}]}}],\"quotas\":[],\"subtotal\":2000,\"errors\":[]},{\"tempId\":2,\"name\":\"TA002\",\"audienceNumber\":2,\"estimatedIR\":80,\"estimatedLOI\":8,\"costPerInterview\":2.5,\"cptg\":2000,\"limit\":800,\"testingUrl\":\"http://testurl2.co\",\"liveUrl\":\"http://liveurl2.co\",\"qualifications\":[{\"tempId\":2,\"order\":2,\"logicalDecision\":\"AND\",\"NumberOfRequiredConditions\":0,\"IsActive\":true,\"question\":{\"id\":10003,\"name\":\"Gender\",\"text\":\"Gender\",\"categoryName\":\"Standard\",\"variables\":[{\"id\":1,\"name\":\"M\",\"selected\":true}]}},{\"tempId\":3,\"order\":3,\"logicalDecision\":\"AND\",\"NumberOfRequiredConditions\":0,\"IsActive\":true,\"question\":{\"id\":10001,\"name\":\"AgeGroup\",\"text\":\"AgeGroup\",\"categoryName\":\"Standard\",\"variables\":[{\"id\":2,\"name\":\"18-24\",\"selected\":true},{\"id\":3,\"name\":\"25-34\",\"selected\":true},{\"id\":4,\"name\":\"35-44\",\"selected\":true}]}}],\"quotas\":[],\"subtotal\":2000,\"errors\":[]}]}";
            var project = JsonConvert.DeserializeObject<Project>(iseJson);

            var exception = Assert.Throws<ArgumentNullException>(
                () => specTransform.TransformIseRequestToCintRequests(project)
            );
            Assert.Equal("Value cannot be null. (Parameter 'Failed in creating Individual Requests. No country found')", exception.Message);
        }

        private Project LoadFullProject()
        {

            var iseJson = "{\"tempId\":0,\"name\":\"Test survey 01\",\"reference\":\"MAX001\",\"lastUpdate\":\"2022-08-16T09:43:48.354Z\",\"startDate\":\"2023-01-01\",\"fieldingPeriod\":60,\"status\":0,\"testingUrl\":\"\",\"liveUrl\":\"\",\"categories\":[5],\"user\":{\"id\":0,\"name\":\"Gopal\",\"email\":\"gopal.ven@kk.com\"},\"errors\":[],\"targetAudiences\":[{\"tempId\":1,\"name\":\"TA001\",\"audienceNumber\":1,\"estimatedIR\":100,\"estimatedLOI\":5,\"costPerInterview\":2,\"cptg\":2000,\"limit\":1000,\"testingUrl\":\"Test URL\",\"liveUrl\":\"Live URL\",\"qualifications\":[{\"tempId\":1,\"order\":1,\"logicalDecision\":\"AND\",\"NumberOfRequiredConditions\":0,\"IsActive\":true,\"question\":{\"id\":10001,\"name\":\"AgeGroup\",\"text\":\"AgeGroup\",\"categoryName\":\"Standard\",\"variables\":[{\"id\":2,\"name\":\"18-24\",\"selected\":true},{\"id\":3,\"name\":\"25-34\",\"selected\":true},{\"id\":4,\"name\":\"35-44\",\"selected\":true},{\"id\":5,\"name\":\"45-54\",\"selected\":true},{\"id\":6,\"name\":\"55-64\",\"selected\":true}]}},{\"tempId\":2,\"order\":2,\"logicalDecision\":\"AND\",\"NumberOfRequiredConditions\":0,\"IsActive\":true,\"question\":{\"id\":10002,\"name\":\"Country\",\"text\":\"Country\",\"categoryName\":\"Standard\",\"variables\":[{\"id\":1,\"name\":\"UK\",\"selected\":true},{\"id\":34,\"name\":\"India\",\"selected\":true}]}},{\"tempId\":3,\"order\":3,\"logicalDecision\":\"AND\",\"NumberOfRequiredConditions\":0,\"IsActive\":true,\"question\":{\"id\":10003,\"name\":\"Gender\",\"text\":\"Gender\",\"categoryName\":\"Standard\",\"variables\":[{\"id\":1,\"name\":\"M\",\"selected\":true},{\"id\":2,\"name\":\"F\",\"selected\":true}]}},{\"tempId\":4,\"order\":4,\"logicalDecision\":\"AND\",\"NumberOfRequiredConditions\":0,\"IsActive\":true,\"question\":{\"id\":16,\"name\":\"Access to car\",\"text\":\"Do you have access to a car?\",\"categoryName\":\"Automotive\",\"variables\":[{\"id\":65,\"name\":\"I own a car/cars\",\"selected\":true},{\"id\":66,\"name\":\"I have access to a car/cars\",\"selected\":true}]}},{\"tempId\":5,\"order\":5,\"logicalDecision\":\"AND\",\"NumberOfRequiredConditions\":0,\"IsActive\":true,\"question\":{\"id\":17,\"name\":\"Make of car/cars\",\"text\":\"If you own/lease a car(s), which brand are they?\",\"categoryName\":\"Automotive\",\"variables\":[{\"id\":69,\"name\":\"Audi\",\"selected\":true},{\"id\":71,\"name\":\"BMW\",\"selected\":true},{\"id\":78,\"name\":\"Ford\",\"selected\":true}]}}],\"quotas\":[{\"tempId\":1,\"quotaName\":\"Under 34\",\"quotaType\":\"completed\",\"fieldTarget\":200,\"limit\":200,\"prescreens\":0,\"completes\":0,\"isActive\":true,\"conditions\":[{\"id\":10001,\"name\":\"AgeGroup\",\"text\":\"AgeGroup\",\"categoryName\":\"Standard\",\"variables\":[{\"id\":2,\"name\":\"18-24\",\"selected\":true},{\"id\":3,\"name\":\"25-34\",\"selected\":true}]}]},{\"tempId\":2,\"quotaName\":\"Under 54 - Male\",\"quotaType\":\"completed\",\"fieldTarget\":200,\"limit\":200,\"prescreens\":0,\"completes\":0,\"isActive\":true,\"conditions\":[{\"id\":10001,\"name\":\"AgeGroup\",\"text\":\"AgeGroup\",\"categoryName\":\"Standard\",\"variables\":[{\"id\":4,\"name\":\"35-44\",\"selected\":true},{\"id\":5,\"name\":\"45-54\",\"selected\":true}]},{\"id\":10003,\"name\":\"Gender\",\"text\":\"Gender\",\"categoryName\":\"Standard\",\"variables\":[{\"id\":1,\"name\":\"M\",\"selected\":true}]}]},{\"tempId\":3,\"quotaName\":\"Under 54 - Female\",\"quotaType\":\"completed\",\"fieldTarget\":200,\"limit\":200,\"prescreens\":0,\"completes\":0,\"isActive\":true,\"conditions\":[{\"id\":10001,\"name\":\"AgeGroup\",\"text\":\"AgeGroup\",\"categoryName\":\"Standard\",\"variables\":[{\"id\":4,\"name\":\"35-44\",\"selected\":true},{\"id\":5,\"name\":\"45-54\",\"selected\":true}]},{\"id\":10003,\"name\":\"Gender\",\"text\":\"Gender\",\"categoryName\":\"Standard\",\"variables\":[{\"id\":2,\"name\":\"F\",\"selected\":true}]}]},{\"tempId\":4,\"quotaName\":\"Above 54\",\"quotaType\":\"completed\",\"fieldTarget\":400,\"limit\":400,\"prescreens\":0,\"completes\":0,\"isActive\":true,\"conditions\":[{\"id\":10001,\"name\":\"AgeGroup\",\"text\":\"AgeGroup\",\"categoryName\":\"Standard\",\"variables\":[{\"id\":6,\"name\":\"55-64\",\"selected\":true}]}]}],\"subtotal\":2000,\"errors\":[]}]}";
            return JsonConvert.DeserializeObject<Project>(iseJson);
        }

        private Project LoadProjectWith2TA3countriesEach()
        {
            var iseJson = "{\"tempId\":0,\"name\":\"Test survey 01\",\"reference\":\"MAX001\",\"startDate\":\"2023-01-01\",\"fieldingPeriod\":60,\"status\":0,\"categories\":[5],\"user\":{\"id\":0,\"name\":\"Gopal\",\"email\":\"gopal.ven@kk.com\"},\"errors\":[],\"targetAudiences\":[{\"tempId\":1,\"name\":\"TA001\",\"audienceNumber\":1,\"estimatedIR\":100,\"estimatedLOI\":5,\"costPerInterview\":2,\"cptg\":2000,\"limit\":1000,\"testingUrl\":\"http://testurl.co\",\"liveUrl\":\"http://liveurl.co\",\"qualifications\":[{\"tempId\":1,\"order\":1,\"logicalDecision\":\"AND\",\"NumberOfRequiredConditions\":0,\"IsActive\":true,\"question\":{\"id\":10002,\"name\":\"Country\",\"text\":\"Country\",\"categoryName\":\"Standard\",\"variables\":[{\"id\":1,\"name\":\"UK\",\"selected\":true},{\"id\":2,\"name\":\"Sweden\",\"selected\":true},{\"id\":3,\"name\":\"Germany\",\"selected\":true}]}}],\"quotas\":[],\"subtotal\":2000,\"errors\":[]},{\"tempId\":2,\"name\":\"TA002\",\"audienceNumber\":2,\"estimatedIR\":80,\"estimatedLOI\":8,\"costPerInterview\":2.5,\"cptg\":2000,\"limit\":800,\"testingUrl\":\"http://testurl2.co\",\"liveUrl\":\"http://liveurl2.co\",\"qualifications\":[{\"tempId\":1,\"order\":1,\"logicalDecision\":\"AND\",\"NumberOfRequiredConditions\":0,\"IsActive\":true,\"question\":{\"id\":10002,\"name\":\"Country\",\"text\":\"Country\",\"categoryName\":\"Standard\",\"variables\":[{\"id\":4,\"name\":\"Denmark\",\"selected\":true},{\"id\":5,\"name\":\"Finland\",\"selected\":true},{\"id\":6,\"name\":\"Norway\",\"selected\":true}]}},{\"tempId\":2,\"order\":2,\"logicalDecision\":\"AND\",\"NumberOfRequiredConditions\":0,\"IsActive\":true,\"question\":{\"id\":10003,\"name\":\"Gender\",\"text\":\"Gender\",\"categoryName\":\"Standard\",\"variables\":[{\"id\":1,\"name\":\"M\",\"selected\":true}]}},{\"tempId\":3,\"order\":3,\"logicalDecision\":\"AND\",\"NumberOfRequiredConditions\":0,\"IsActive\":true,\"question\":{\"id\":10001,\"name\":\"AgeGroup\",\"text\":\"AgeGroup\",\"categoryName\":\"Standard\",\"variables\":[{\"id\":2,\"name\":\"18-24\",\"selected\":true},{\"id\":3,\"name\":\"25-34\",\"selected\":true},{\"id\":4,\"name\":\"35-44\",\"selected\":true}]}}],\"quotas\":[],\"subtotal\":2000,\"errors\":[]}]}";
            return JsonConvert.DeserializeObject<Project>(iseJson);
        }

    }
}
