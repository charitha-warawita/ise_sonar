using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Core.Results;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface ITargetAudienceContext
    {
        TargetAudience CreateTargetAudience(long projectId, TargetAudience audience);

        Task<PagedResult<TargetAudience>> GetTargetAudiencesByProjectIdAsync(long id, int page, int pageSize);
    }
}
