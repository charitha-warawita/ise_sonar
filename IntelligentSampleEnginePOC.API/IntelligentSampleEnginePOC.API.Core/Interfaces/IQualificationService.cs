using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface IQualificationService
    {
        List<Qualification> GetQualification(long  qid);

        Qualification CreateQualification (long? taid,  Qualification qualData);
    
    }
}
