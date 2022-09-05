using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Http.Tests.MockModelData
{
    public class QuotaTest
    {
        public Quota GetQuota(long qtid, Quota quotadata)
        {
            if(quotadata != null)
            {
                if(quotadata.Id == qtid)
                {
                    return quotadata;
                }
            }

            return null; 

       }
        public static string GetQuotaJson()
        {
            return @" {
                        ""id"": 501,
                        ""quotaName"": ""Female - 35 to 55"",
                        ""quotaType"": ""completed"",
                        ""fieldTarget"": 200,
                        ""limit"": 200,
                        ""prescreens"": 0,
                        ""completes"": 0,
                        ""isActive"": true,
                        ""conditions"": [
                         {
                            ""id"": 10001,
                            ""name"": ""AgeGroup"",
                            ""text"": ""AgeGroup"",
                            ""categoryName"": ""Standard"",
                            ""variables"": [
                                    {
                                ""id"": 4,
                                ""name"": ""35-44""
                                    },
                                    {
                                    ""id"": 5,
                                    ""name"": ""45-54""
                                    }
                                    ]
                                    }
  
                                    ]
                            }";
           
        }


        public Quota CreateQuotaTest(long projectId, long taid, Quota quota)
        {
            quota.Id = 333;
            return quota;
        }


        public long DeleteQuotaTest(long qtid, Quota quota)
        {
            quota.Id = qtid;
            return qtid;
        }

    }
}
