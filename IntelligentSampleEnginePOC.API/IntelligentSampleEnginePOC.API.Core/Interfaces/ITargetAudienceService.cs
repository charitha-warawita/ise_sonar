using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Core.Results;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface ITargetAudienceService
    {
        TargetAudience CreateTargetAudience(long? projectId, TargetAudience audience);

        Task<List<TargetAudience>> GetAllByProjectIdAsync(long id);
    }
}
