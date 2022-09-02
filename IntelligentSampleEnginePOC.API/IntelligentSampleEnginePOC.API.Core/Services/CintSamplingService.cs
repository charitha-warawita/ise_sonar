using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using System.Net.Http.Json;
using IntelligentSampleEnginePOC.API.Core.Cint.Endpoints;
using IntelligentSampleEnginePOC.API.Core.Model.Cint;

namespace IntelligentSampleEnginePOC.API.Core.Services
{
    public  class CintSamplingService : ICintSamplingService
    {
        private readonly ISurveysEndpoint _surveysEndpoint;
        private readonly ITestingEndpoint _testingEndpoint;
        private readonly IProjectCintContext _projectCintContext;
        private readonly ISpecTransform _cintCustomTransform;

        public CintSamplingService(
            IProjectCintContext projectCintContext,
            ISpecTransform cintCustomTransform,
            ISurveysEndpoint surveysEndpoint,
            ITestingEndpoint testingEndpoint)
        {
            _projectCintContext = projectCintContext;
            _cintCustomTransform =  cintCustomTransform;
            _surveysEndpoint = surveysEndpoint;
            _testingEndpoint = testingEndpoint;
        }

        public List<CintRequestModel> ConvertToCintRequests(Project project)
        {
            var cintRequests = _cintCustomTransform.TransformIseRequestToCintRequests(project);
            if (cintRequests != null)
            {
                var result = cintRequests;
                return result;
            }

            throw new ApplicationException("Validation and conversion failed");
        }

        public async Task<Project> CreateProject(Project project)
        {
            var cintRequests = _cintCustomTransform.TransformIseRequestToCintRequests(project);
            await CallandStoreCintData(project, cintRequests);

            return project;
        }

        public async Task<List<Survey>> GetSurveysAsync(long projectId)
        {
            var surveys = new List<Survey>();
            var ids = await _projectCintContext.GetCintIdsAsync(projectId);
            if (!ids.Any())
                return surveys;
            
            var links = (await _projectCintContext.GetLinksAsync(projectId)).ToLookup(l => l.SurveyId);
            foreach (var id in ids)
            {
                var survey = await _surveysEndpoint.GetAsync(id);   // Do we really need to make a while HTTP call for the survey name?

                var surveyLinks = links[id].ToList();
                if (surveyLinks.All(l => l.Type == Constants.LinkTypes.Create))   // Do we have all links? i.e Test links.
                {
                    var test = await _testingEndpoint.TestAsync(id);
                    var testLinks = test.Links
                        .Select(l => new Link(id, l.rel, l.href, Constants.LinkTypes.Test))
                        .ToList();
                  
                    await _projectCintContext.InsertLinksAsync(id, projectId, testLinks);

                    surveyLinks = surveyLinks.Union(testLinks).ToList();
                }

                var dto = new Survey(survey.name)
                {
                    Links = surveyLinks
                };
                surveys.Add(dto);
            }
            
            return surveys;
        }

        private async Task CallandStoreCintData(Project project, List<CintRequestModel> cintRequests)
        {
            foreach (var item in cintRequests)
            {
                var response = await _surveysEndpoint.PostAsync(item.Request);
                if (response is null)
                    continue;
                
                var cintResponse = await response.Content.ReadFromJsonAsync<CintResponse>();
                if (cintResponse is null)
                    continue;

                _projectCintContext.StoreProjectCintResponse(
                    response.IsSuccessStatusCode,
                    response.StatusCode.ToString(),
                    item,
                    cintResponse);
            }
        }
    }
}
