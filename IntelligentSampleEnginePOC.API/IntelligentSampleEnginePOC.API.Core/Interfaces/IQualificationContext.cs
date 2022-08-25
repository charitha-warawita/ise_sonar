using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface IQualificationContext
    {
        List<Qualification> GetQualificationsFromDB( long qid);

        Qualification CreateQualification(long projectId,long taid, Qualification qualData);

        public long DeleteQualificationFromDB(long qid);
    }
}
