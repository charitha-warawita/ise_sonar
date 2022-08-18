using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Services
{
    public class QualificationService : IQualificationService
    {

         private readonly ILogger _logger;
         private readonly IQualificationContext _qualificationContext;
        public QualificationService(ILogger<QualificationService> logger, IQualificationContext qualificationContext)
       {
            _logger = logger;
            _qualificationContext = qualificationContext;
       }

        public List<Qualification> GetQualification(long qid)
        {
            return _qualificationContext.GetQualificationsFromDB( qid);
        }


        public Qualification CreateQualification(long projectId, long? taid, Qualification qualData)
        {
            if (taid == null)
                throw new ArgumentNullException("TA Id  is not found");
            if (qualData == null)
                throw new ArgumentNullException("Qualification model  is not found", nameof(qualData));

            if (QualificationValidated(qualData))
                return _qualificationContext.CreateQualification(projectId,(long)taid, qualData);

            throw new ArgumentException("Target Audience Validation failed", nameof(qualData));
        }

        private bool QualificationValidated(Qualification qualData)
        {
            return true;
        }

    }
}
