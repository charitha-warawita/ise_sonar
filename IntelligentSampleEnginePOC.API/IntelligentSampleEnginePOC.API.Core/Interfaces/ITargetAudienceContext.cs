using IntelligentSampleEnginePOC.API.Core.Model;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface ITargetAudienceContext
    {
        TargetAudience CreateTargetAudience(long projectId, TargetAudience audience);

        IEnumerable<TargetAudience> GetTargetAudiencesByProjectId(long id);
    }
}
