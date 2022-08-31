using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Core.Results;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface ITargetAudienceService
    {
        TargetAudience CreateTargetAudience(long? projectId, TargetAudience audience);

        Task<PagedResult<TargetAudience>> GetAllByProjectIdAsync(long id, int page, int pageSize);
    }
}
