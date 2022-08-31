using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Http.Tests.MockModelData
{
    public class QualificationTest
    {
        public List <Qualification> GetQualification(long qid , Qualification qualification)
        {
          List <Qualification> qualification1 = new List<Qualification>();
            qualification1.Equals(qualification);
            if (!qualification1.Any())
                return null;
            else
                return qualification1;


        }

        public static string GetQualificationJson()
        {
            return @"{
                                ""id"": 331,
                                ""name"": """",
                                ""order"": 3,
                                ""logicalDecision"": ""AND"",
                                ""NumberOfRequiredConditions"": 0,
                                ""IsActive"": false,
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
                                }
                            
                       
                      
                    }";
        }


        

        public Qualification CreateQualificationTest(long projectId,long taid, Qualification qualification)
        {
            qualification.Id = 333;
            return qualification;
        }


        public long DeleteQualificationTest(long qid, Qualification qualification)
        {
            qualification.Id = qid;
            return qid;
        }
    }
}
