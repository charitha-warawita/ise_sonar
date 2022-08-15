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
        public CintSamplingService(HttpClient client, IOptions<CintApiSettings> options)
        {
            _httpClient = client;
            _settings = options.Value;
        }

        public CintRequestsModel ConvertToCintRequests(Project project)
        {
            var cintRequests = ConvertProjectToCintRequest(project);
            if(cintRequests != null)
            {
                var result = new CintRequestsModel();
                result.ValidationResult = true;
                result.Requests = cintRequests;
                return result;
            }

            throw new ApplicationException("Validation and conversion failed");
        }

        public async Task<Project> CreateProject(Project project)
        {
            var cintRequest = ConvertProjectToCintRequest(project);
            // var cintResponse = await CallCintApi(cintRequest.FirstOrDefault());
            // return AddCintResponseToProject(cintResponse, project);
            throw new NotImplementedException();
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

        private CintRequest CreateIndividualSurvey(Project project, int audienceNumber, int? countryId, string? countryName)
        {
            var isLimitFromCountryQuota = false;
            StringBuilder surveyName = new StringBuilder();
            CintRequest cintRequest = new CintRequest();
            // cintRequest.name = project.Name;
            surveyName.Append(project.Name);
            cintRequest.referenceNumber = project.Reference;
            cintRequest.purchaseOrderNumber = project.Reference;
            cintRequest.contact = new contact { name = project.User.Name, company = project.User.Name, emailAddress = project.User.Email };
            // cintRequest.linkTemplate = project.LiveUrl;
            // cintRequest.testLinkTemplate = project.TestingUrl;

            var ta = project.TargetAudiences.Where(x => x.AudienceNumber == audienceNumber).FirstOrDefault();
            if (ta != null)
            {
                surveyName.Append("-"); surveyName.Append(ta.Name);
                surveyName.Append("-"); surveyName.Append(countryName);
                cintRequest.name = surveyName.ToString();
                cintRequest.limit = ta.Limit;
                cintRequest.limitType = 0;
                cintRequest.incidenceRate = ta.EstimatedIR;
                cintRequest.lengthOfInterview = ta.EstimatedLOI;
                if (countryId != null)
                    cintRequest.countryId = (int)countryId;
                cintRequest.deviceTypes = new List<int>() { 1, 2, 3 };
                cintRequest.deviceCapabilities = new List<string>();
                cintRequest.categories = project.Categories;
                cintRequest.testLinkTemplate = ta.TestingUrl;
                cintRequest.linkTemplate = ta.LiveUrl;
               
                // var quotaGroup = new quotaGroup();

                var quotaswithKeys = new List<QuotaGrouper>();
                foreach (var item in ta.Quotas)
                {
                    var quotaWithKeys = new QuotaGrouper();
                    quotaWithKeys.Keywords = new List<string>();

                    var qt = new quota();
                    qt.name = item.QuotaName;
                    qt.limit = item.Limit;
                    qt.targetGroup = new targetGroup();
                    var profileVariables = new List<int>();

                    foreach (var subItem in item.Conditions)
                    {
                        quotaWithKeys.Keywords.Add(subItem.Name);

                        if(subItem.CategoryName.ToLower() == "standard")
                        {
                            //Standard variables Age, gender or country
                            var siName = subItem.Name.ToLower();
                            if(subItem.Id == 10001 && siName == "agegroup") // Age group
                            {
                                var firstVar = subItem.Variables.FirstOrDefault()?.Name.Trim();
                                var lastVar = subItem.Variables.LastOrDefault()?.Name.Trim();
                                if (firstVar == "<18")
                                    qt.targetGroup.maxAge = 18;
                                else if (lastVar == "65+")
                                    qt.targetGroup.minAge = 65;
                                else
                                {
                                    qt.targetGroup.minAge = Convert.ToInt32(firstVar?.Substring(0, 2));
                                    qt.targetGroup.maxAge = Convert.ToInt32(lastVar?.Substring(lastVar.IndexOf('-') + 1, 2));
                                }
                            }
                            else if(subItem.Id == 10003 && siName == "gender")
                            {
                                if(subItem.Variables.FirstOrDefault() != null)
                                    qt.targetGroup.gender = subItem.Variables.FirstOrDefault().Id;
                            }
                            else if(subItem.Id == 10002 && siName == "country")
                            {
                                if (isLimitFromCountryQuota)
                                    cintRequest.limit += item.Limit;
                                else
                                {
                                    isLimitFromCountryQuota = true;
                                    cintRequest.limit = item.Limit;
                                }
                            }
                        }
                        else
                        {
                            foreach (var temp in subItem.Variables)
                                profileVariables.Add(temp.Id);
                        }
                    }
                    if(profileVariables.Any())
                        qt.targetGroup.variableIds = profileVariables;

                    quotaWithKeys.CintQuota = qt;
                    quotaswithKeys.Add(quotaWithKeys);
                }

                foreach(var item in ta.Qualifications)
                {
                    

                    var subItem = item.Question;
                    //foreach (var subItem in item.Conditions)
                    //{
                    var currentItemFoundinQuotaList = false;
                    foreach(var temp in quotaswithKeys)
                    {
                        if (temp.Keywords.Contains(subItem.Name))
                        {
                            currentItemFoundinQuotaList = true;
                            break;
                        }
                    }
                    if(!currentItemFoundinQuotaList)
                    {
                        var quotaWithKeys = new QuotaGrouper();
                        quotaWithKeys.Keywords = new List<string>();

                        var qt = new quota();
                        qt.name = string.IsNullOrEmpty(item.Name) ? "" : item.Name;
                        qt.limit = ta.Limit;
                        qt.targetGroup = new targetGroup();
                        var profileVariables = new List<int>();

                        quotaWithKeys.Keywords.Add(subItem.Name);

                        if (subItem.CategoryName.ToLower() == "standard")
                        {
                            //Standard variables Age, gender or country
                            var siName = subItem.Name.ToLower();
                            if (subItem.Id == 10001 && siName == "agegroup") // Age group
                            {
                                var firstVar = subItem.Variables.FirstOrDefault()?.Name.Trim();
                                var lastVar = subItem.Variables.LastOrDefault()?.Name.Trim();
                                if (firstVar == "<18")
                                    qt.targetGroup.maxAge = 18;
                                else if (lastVar == "65+")
                                    qt.targetGroup.minAge = 65;
                                else
                                {
                                    qt.targetGroup.minAge = Convert.ToInt32(firstVar?.Substring(0, 2));
                                    qt.targetGroup.maxAge = Convert.ToInt32(lastVar?.Substring(lastVar.IndexOf('-') + 1, 2));
                                }
                            }
                            else if (subItem.Id == 10003 && siName == "gender")
                            {
                                if (subItem.Variables.Count == 1)
                                {
                                    if (subItem.Variables.FirstOrDefault() != null)
                                        qt.targetGroup.gender = subItem.Variables.FirstOrDefault().Id;
                                }
                            }
                            /*else if (subItem.Id == 10002 && siName == "country")
                            {
                                if (isLimitFromCountryQuota)
                                    cintRequest.limit += item.Limit;
                                else
                                {
                                    isLimitFromCountryQuota = true;
                                    cintRequest.limit = item.Limit;
                                }
                            }*/
                        }
                        else
                        {
                            foreach (var temp in subItem.Variables)
                                profileVariables.Add(temp.Id);
                        }
                        //}
                        if (profileVariables.Any())
                            qt.targetGroup.variableIds = profileVariables;

                        quotaWithKeys.CintQuota = qt;
                        quotaswithKeys.Add(quotaWithKeys);
                    }

                    
                }




                cintRequest.quotaGroups = new List<quotaGroup>();
                // var keystoCompare = new List<string>();
                foreach(var quotaKey in quotaswithKeys)
                {
                    var tg = quotaKey.CintQuota.targetGroup;
                    if (tg.minAge < 1 && tg.maxAge < 1
                        && tg.variableIds == null && tg.panelIds == null
                        && tg.panelVariableIds == null && tg.regionIds == null && tg.gender < 1 && tg.postalCodes == null)
                        continue;

                    quotaGroup currentGroup = null;
                    foreach(var item in quotaKey.Keywords)
                    {
                        currentGroup = cintRequest.quotaGroups.Where(x => x.name.Contains(item)).FirstOrDefault();
                        if (currentGroup != null)
                            break;
                    }
                    if(currentGroup != null)
                    {
                        cintRequest.quotaGroups.Where(x => x.name == currentGroup.name).FirstOrDefault().Quotas.Add(quotaKey.CintQuota);
                    }
                    else
                    {
                        var newGroup = new quotaGroup();
                        newGroup.name = String.Join("-", quotaKey.Keywords);
                        newGroup.limitType = 0;
                        newGroup.Quotas = new List<quota>();
                        newGroup.Quotas.Add(quotaKey.CintQuota);
                        cintRequest.quotaGroups.Add(newGroup);
                    }
                }

            }
            else
                throw new ArgumentNullException("Target Audience is not found");
            
            return cintRequest;
        }

        private List<CintRequest> ConvertProjectToCintRequest(Project project)
        {
            List<CintRequest> requests = new List<CintRequest>();

            // string input = File.ReadAllText("../IntelligentSampleEnginePOC.API.Core/Cint/IseRequest.json");
            // string transformer = File.ReadAllText("../IntelligentSampleEnginePOC.API.Core/Cint/IsetoCintTransformer.json");
            // string transformedString = JsonTransformer.Transform(transformer, input);

            if(project.TargetAudiences.Any())
            {
                foreach(var item in project.TargetAudiences)
                {
                    var countryQuals = item.Qualifications.Where(x => (x.Question.CategoryName.ToLower() == "standard"
                                                        && x.Question.Name.ToLower() == "country")).ToList();
                    if(countryQuals.Any())
                    {
                        foreach(var subItem in countryQuals)
                        {
                            foreach(var subSubItem in subItem.Question.Variables)
                            {
                                requests.Add(CreateIndividualSurvey(project, item.AudienceNumber, subSubItem.Id, subSubItem.Name));
                            }
                        }
                    }
                    else
                    {
                        requests.Add(CreateIndividualSurvey(project, item.AudienceNumber, null, null));
                    }
                }
            }
            var stringRequest = JsonSerializer.Serialize(requests);
            return requests;
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
