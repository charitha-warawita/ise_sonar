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
    public  class CintService : ICintService
    {
        private readonly HttpClient _httpClient;
        private CintApiSettings _settings;
        public CintService(HttpClient client, IOptions<CintApiSettings> options)
        {
            _httpClient = client;
            _settings = options.Value;
        }

        public async Task<Project> CallCint(Project project)
        {
            var cintRequest = ConvertProjectToCintRequest(project);
            var cintResponse = await CallCintApi(cintRequest);
            return AddCintResponseToProject(cintResponse, project);

        }

        private Project AddCintResponseToProject(CintResponse response, Project project)
        {
            if(response != null)
            {
                /*project.CintResponseId = response.id;
                if(response.links.Any())
                {
                    foreach(var item in response.links)
                    {
                        if (item.rel == "self")
                            project.CintSelfLink = item.href;
                        else if (item.rel == "currentCost")
                            project.CintCurrentCostLink = item.href;
                        else if (item.rel == "testing")
                            project.CintTestingLink = item.href;
                    }
                }*/
            }
            return project;
        }



        private CintRequest ConvertProjectToCintRequest(Project project)
        {
            var cintRequest = new CintRequest();

            cintRequest.name = project.Name;
            cintRequest.referenceNumber = project.Reference;
            cintRequest.purchaseOrderNumber = project.Reference;
            cintRequest.contact = new contact { name = project.User.Name, company = project.User.Name, emailAddress = project.User.Email };
            cintRequest.limit = 200;
            cintRequest.limitType = 0;
            cintRequest.incidenceRate = 80;
            cintRequest.lengthOfInterview = 5;
            /*cintRequest.linkTemplate = project.LinkToSurvey;
            cintRequest.testLinkTemplate = project.LinkToSurvey;*/
            cintRequest.countryId = 22;
            cintRequest.deviceTypes = new List<int>() { 1, 2, 3 };
            cintRequest.deviceCapabilities = new List<string>();
            cintRequest.categories = new List<int>() { 8 };
            cintRequest.quotaGroups = new List<quotaGroup>();
            /*foreach(var item in project.TargetAudiences[0].QuotaGroups)
            {
                var quotaGroup = new quotaGroup()
                {
                    name = item.Name,
                    limitType = 0
                };
                quotaGroup.Quotas = new List<quota>();
                foreach(var subItem in item.Quotas)
                {
                    var quota = new quota();
                    quota.name = subItem.QuotaName;
                    quota.limit = 100;
                    quota.targetGroup = new targetGroup
                    {
                        minAge = subItem.MinAge,
                        maxAge = subItem.MaxAge
                    };
                    quotaGroup.Quotas.Add(quota);
                    
                }
                cintRequest.quotaGroups.Add(quotaGroup);
                break;
            }*/

            return cintRequest;
        }

        private async Task<CintResponse> CallCintApi(CintRequest request)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Path.Combine(_settings.Url, _settings.Path));
            var jsonString = JsonSerializer.Serialize(request);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(builder.ToString(), request);
            response.EnsureSuccessStatusCode();

            var cintResponse = await response.Content.ReadFromJsonAsync<CintResponse>();
            return cintResponse;
        }
    }
}
