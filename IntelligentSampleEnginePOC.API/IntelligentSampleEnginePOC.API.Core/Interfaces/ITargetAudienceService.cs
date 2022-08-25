using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Core.Results;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface ITargetAudienceService
    {
        TargetAudience CreateTargetAudience(long? projectId, TargetAudience audience);

        PagedResult<TargetAudience> GetAllByProjectId(long id, int page, int pageSize);
    }
}
