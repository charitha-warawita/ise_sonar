using IntelligentSampleEnginePOC.API.Core.Model;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface ITargetAudienceService
    {
        TargetAudience CreateTargetAudience(long? projectId, TargetAudience audience);

        IEnumerable<TargetAudience> GetAllByProjectId(long id);
    }
}
