using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Tests.MockModelData
{
    public class CreateProjects
    {
        public Project CreateProjectTest(Project project)
        {
            project.Id = 1;
            return project;
        }
        public Project CreateProjectTargetAudience(Project project)
        {
            return project;
        }

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


        public static string GetProjectJson()
        {
            return @"{
                      ""tempId"": 1,
                      ""name"": ""AutoSurvey01"",
                      ""reference"": ""MAC001"",
                      ""lastUpdate"": ""2022-07-28T00:51:50.752Z"",
                      ""startDate"": ""2023-01-01"",
                      ""fieldingPeriod"": 60,
                      ""status"": 0,
                      ""testingUrl"": ""https://sim.cintworks.net/[ID]"",
                      ""liveUrl"": ""https://sim.cintworks.net/[ID]"",
                      ""categories"": [5],
                      ""user"": {
                        ""id"": 1,
                        ""name"": ""Test"",
                        ""email"": ""test.kantar@kk.com""
                      },
                      ""errors"": [],
                      ""targetAudiences"": [
                        {
                          ""tempId"": 1,
                          ""name"": ""TA001"",
                          ""audienceNumber"": 1,
                          ""estimatedIR"": 100,
                          ""estimatedLOI"": 5,
                          ""costPerInterview"": 2,
                          ""cptg"": 2000,
                          ""wantedCompletes"": 1000,
                          ""qualifications"": [
                            {
                              ""id"": 1,
                              ""order"": 1,
                              ""logicalDecision"": ""AND"",
                              ""NumberOfRequiredConditions"": 0,
                              ""IsActive"": true,
                              ""question"": {
                                ""id"": 10001,
                                ""name"": ""AgeGroup"",
                                ""text"": ""AgeGroup"",
                                ""categoryName"": ""Standard"",
                                ""variables"": [
                                  {
                                    ""id"": 2,
                                    ""name"": ""18-24"",
                                    ""selected"": true
                                  },
                                  {
                                    ""id"": 3,
                                    ""name"": ""25-34"",
                                    ""selected"": true
                                  },
                                  {
                                    ""id"": 4,
                                    ""name"": ""35-44"",
                                    ""selected"": true
                                  },
                                  {
                                    ""id"": 5,
                                    ""name"": ""45-54"",
                                    ""selected"": true
                                  },
                                  {
                                    ""id"": 6,
                                    ""name"": ""55-64"",
                                    ""selected"": true
                                  }
                                ]
                              }
                            },
                            {
                              ""id"": 2,
                              ""order"": 2,
                              ""logicalDecision"": ""AND"",
                              ""NumberOfRequiredConditions"": 0,
                              ""IsActive"": true,
                              ""question"": {
                                ""id"": 10002,
                                ""name"": ""Country"",
                                ""text"": ""Country"",
                                ""categoryName"": ""Standard"",
                                ""variables"": [
                                  {
                                    ""id"": 1,
                                    ""name"": ""UK"",
                                    ""selected"": true
                                  }
                                ]
                              }
                            },
                            {
                              ""id"": 3,
                              ""order"": 3,
                              ""logicalDecision"": ""AND"",
                              ""NumberOfRequiredConditions"": 0,
                              ""IsActive"": true,
                              ""question"": {
                                ""id"": 10003,
                                ""name"": ""Gender"",
                                ""text"": ""Gender"",
                                ""categoryName"": ""Standard"",
                                ""variables"": [
                                  {
                                    ""id"": 1,
                                    ""name"": ""M"",
                                    ""selected"": true
                                  },
                                  {
                                    ""id"": 2,
                                    ""name"": ""F"",
                                    ""selected"": true
                                  }
                                ]
                              }
                            },
                            {
                              ""id"": 4,
                              ""order"": 4,
                              ""logicalDecision"": ""AND"",
                              ""NumberOfRequiredConditions"": 0,
                              ""IsActive"": true,
                              ""question"": {
                                ""id"": 16,
                                ""name"": ""Access to car"",
                                ""text"": ""Do you have access to a car?"",
                                ""categoryName"": ""Automotive"",
                                ""variables"": [
                                  {
                                    ""id"": 65,
                                    ""name"": ""I own a car/cars"",
                                    ""selected"": true
                                  }
                                ]
                              }
                            },
                            {
                              ""id"": 5,
                              ""order"": 5,
                              ""logicalDecision"": ""AND"",
                              ""NumberOfRequiredConditions"": 0,
                              ""IsActive"": true,
                              ""question"": {
                                ""id"": 17,
                                ""name"": ""Make of car/cars"",
                                ""text"": ""If you own/lease a car(s), which brand are they?"",
                                ""categoryName"": ""Automotive"",
                                ""variables"": [
                                  {
                                    ""id"": 69,
                                    ""name"": ""Audi"",
                                    ""selected"": true
                                  },
                                  {
                                    ""id"": 71,
                                    ""name"": ""BMW"",
                                    ""selected"": true
                                  }
                                ]
                              }
                            }
                          ],
                          ""quota"": [],
                          ""quotas"": [],
                          ""subtotal"": 2000,
                          ""errors"": []
                        }
                      ]
                    }";
        }
    }
   
}
