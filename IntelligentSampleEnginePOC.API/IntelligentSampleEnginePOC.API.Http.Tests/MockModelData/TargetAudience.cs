using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Http.Tests.MockModelData
{
    public class CreateTargetAudiences
    {
        public TargetAudience CreateTargetAudienceTest(long projectId, TargetAudience audience)
        {
            audience.Id = 1;
            return audience;
        }
        public static string GetTargetAudienceJson()
        {
            return @"{
                        ""name"": ""TA01"",
                        ""audienceNumber"": 1,
                        ""estimatedIR"": 100,
                        ""estimatedLOI"": 5,
                        ""costPerInterview"": 0,
                        ""cptg"": 0,
                        ""limit"": 1000,
                        ""limitType"": 1000,
                        ""qualifications"": [
                            {
                                ""order"": 1,
                                ""logicalDecision"": ""OR"",
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
                                            ""name"": ""18-24""
                                        },
                                        {
                                            ""id"": 3,
                                            ""name"": ""25-34""
                                        },
                                        {
                                            ""id"": 4,
                                            ""name"": ""35-44""
                                        },
                                        {
                                            ""id"": 5,
                                            ""name"": ""45-54""
                                        },
                                        {
                                            ""id"": 6,
                                            ""name"": ""55-64""
                                        }
                                    ]
                                },
                                ""tempId"": 1
                            },
                            {
                                ""order"": 2,
                                ""logicalDecision"": ""OR"",
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
                                            ""name"": ""UK""
                                        },
                                        {
                                            ""id"": 34,
                                            ""name"": ""India""
                                        }
                                    ]
                                },
                                ""tempId"": 2
                            },
                            {
                                ""order"": 3,
                                ""logicalDecision"": ""OR"",
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
                                            ""name"": ""M""
                                        },
                                        {
                                            ""id"": 2,
                                            ""name"": ""F""
                                        }
                                    ]
                                },
                                ""tempId"": 3
                            },
                            {
                                ""order"": 4,
                                ""logicalDecision"": ""OR"",
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
                                            ""name"": ""I own a car/cars""
                                        },
                                        {
                                            ""id"": 66,
                                            ""name"": ""I have access to a car/cars""
                                        }
                                    ]
                                },
                                ""tempId"": 4
                            },
                            {
                                ""order"": 5,
                                ""logicalDecision"": ""OR"",
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
                                            ""name"": ""Audi""
                                        },
                                        {
                                            ""id"": 71,
                                            ""name"": ""BMW""
                                        }
                                    ]
                                },
                                ""tempId"": 5
                            },
                            {
                                ""order"": 6,
                                ""logicalDecision"": ""OR"",
                                ""NumberOfRequiredConditions"": 0,
                                ""IsActive"": true,
                                ""question"": {
                                    ""id"": 18,
                                    ""name"": ""Type of car/cars"",
                                    ""text"": ""How would you describe the car(s) you own/lease?"",
                                    ""categoryName"": ""Automotive"",
                                    ""variables"": [
                                        {
                                            ""id"": 107,
                                            ""name"": ""Sub Compact (e.g. Toyota Yaris)""
                                        }
                                    ]
                                },
                                ""tempId"": 6
                            }
                        ],
                        ""quota"": [],
                        ""quotas"": [],
                        ""subtotal"": 0,
                        ""tempId"": 1
                    }";
        }
    }   
}
