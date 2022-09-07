using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Core.Model.Cint;

namespace IntelligentSampleEnginePOC.API.Core.Interfaces
{
    public interface IProjectCintContext
    {
        public long StoreProjectCintResponse(bool isSuccess, string statusCode, CintRequestModel request, CintResponse response);

        public Task<HashSet<long>> GetCintIdsAsync(long id);
        
        Task<List<Link>> GetLinksAsync(long id);

        Task InsertLinksAsync(long surveyId, long projectId, List<Link> links);
    }
}
