using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelligentSampleEnginePOC.API.Core.Results;

namespace IntelligentSampleEnginePOC.API.Core.Services
{
    public class TargetAudienceService : ITargetAudienceService
    {
        private readonly ITargetAudienceContext _taContext;
        public TargetAudienceService(ITargetAudienceContext taContext)
        {
            _taContext = taContext;
        }

        public TargetAudience CreateTargetAudience(long? projectId, TargetAudience audience)
        {
            if (projectId == null)
                throw new ArgumentNullException("Project Id  is not found");
            if (audience == null)
                throw new ArgumentNullException("Target Audience model  is not found", nameof(audience));

            if (TargetAudienceValidated(audience))
                return _taContext.CreateTargetAudience((long)projectId, audience);

            throw new ArgumentException("Target Audience Validation failed", nameof(audience));
        }

        public PagedResult<TargetAudience> GetAllByProjectId(long id, int page, int pageSize = 5)
        {
            var result = _taContext.GetTargetAudiencesByProjectId(id, page, pageSize);
            
            return result;
        }

        private bool TargetAudienceValidated(TargetAudience audience)
        {
            return true;
        }
    }
}
