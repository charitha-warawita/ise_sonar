using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Core.Results;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface ITargetAudienceContext
    {
        TargetAudience CreateTargetAudience(long projectId, TargetAudience audience);

        Task<List<TargetAudience>> GetTargetAudiencesByProjectIdAsync(long id);
    }
}
