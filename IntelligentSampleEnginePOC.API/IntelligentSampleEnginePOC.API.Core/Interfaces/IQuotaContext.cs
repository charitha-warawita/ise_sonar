using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface IQuotaContext
    {
        Quota GetQuota(long qtid);

        Quota CreateQuota(long projectId, long taid, Quota qtaData);

        public long DeleteQuotaFromDB(long qtid);
    }
 
    
}
