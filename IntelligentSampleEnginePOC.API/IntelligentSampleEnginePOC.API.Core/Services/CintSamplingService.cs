using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Services
{
    public  class CintSamplingService : ICintSamplingService
    {
        private readonly HttpClient _httpClient;
        private CintApiSettings _settings;
        private IProjectCintContext _projectCintContext;
        private ISpecTransform _cintCustomTransform;
        private IProjectValidator _projectValidator;

        public CintSamplingService(HttpClient client, IOptions<CintApiSettings> options, 
            IProjectCintContext projectCintContext, ISpecTransform cintCustomTransform, IProjectValidator projectValidator)
        {
            _httpClient = client;
            _settings = options.Value;
            _projectCintContext = projectCintContext;
            _cintCustomTransform =  cintCustomTransform;
            _projectValidator = projectValidator;
        }

        public List<CintRequestModel> ConvertToCintRequests(Project project)
        {
            // var cintRequests = ConvertProjectToCintRequest(project);
            if (_projectValidator.IsValidated(project))
            {
                var cintRequests = _cintCustomTransform.TransformIseRequestToCintRequests(project);
                if (cintRequests != null)
                {
                    var result = cintRequests;
                    return result;
                }
            }

            throw new ArgumentException("Project Validation failed", nameof(project));
        }

        public async Task<Project> CreateProject(Project project)
        {
            try
            {
                // var cintRequests = ConvertProjectToCintRequest(project);
                var cintRequests = _cintCustomTransform.TransformIseRequestToCintRequests(project);
                await CallandStoreCintData(project, cintRequests);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return project;
        }

        private async Task CallandStoreCintData(Project project, List<CintRequestModel> cintRequests)
        {
 
            foreach (var item in cintRequests)
            {
                var response = await CallCintApi(item.Request);
                if (response != null)
                {
                    var cintResponse = await response.Content.ReadFromJsonAsync<CintResponse>();
                    if (cintResponse != null)
                    {
                        var projectCintResponseId =
                            _projectCintContext.StoreProjectCintResponse(response.IsSuccessStatusCode,
                            response.StatusCode.ToString(), item, cintResponse);
                    }
                }
            }
        }

        private async Task<HttpResponseMessage> CallCintApi(CintRequest request)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Path.Combine(_settings.Url, _settings.Path));
            var jsonString = JsonSerializer.Serialize(request);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(builder.ToString(), request);
            // response.EnsureSuccessStatusCode();

            
            return response;
        }       
    }
}
