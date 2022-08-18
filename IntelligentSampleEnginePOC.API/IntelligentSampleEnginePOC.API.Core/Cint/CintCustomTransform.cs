using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Cint
{
    public class CintCustomTransform : ISpecTransform
    {
        bool isLimitFromCountryQuota = false;
        CintRequest cintRequest = new CintRequest();

        public List<CintRequestModel> TransformIseRequestToCintRequests(Project project)
        {
            List<CintRequestModel> requests = new List<CintRequestModel>();

            if (project.TargetAudiences.Any())
            {
                foreach (var item in project.TargetAudiences)
                {
                    var countryQuals = item.Qualifications.Where(x => (x.Question.CategoryName.ToLower() == "standard"
                                                        && x.Question.Name.ToLower() == "country")).ToList();
                    if (countryQuals.Any())
                    {
                        foreach (var subItem in countryQuals)
                        {
                            foreach (var subSubItem in subItem.Question.Variables)
                            {
                                var cintRequetModel = new CintRequestModel();
                                cintRequetModel.ProjectId = project.Id;
                                cintRequetModel.TargetAudienceId = item.Id;
                                cintRequetModel.CountryId = subSubItem.Id;
                                cintRequetModel.CountryName = subSubItem.Name;
                                cintRequetModel.Request = CreateIndividualSurvey(project, item.AudienceNumber, subSubItem.Id, subSubItem.Name);
                                if (cintRequetModel.Request == null)
                                    cintRequetModel.ValidationResult = false;
                                else
                                    cintRequetModel.ValidationResult = true;

                                requests.Add(cintRequetModel);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Failed in creating Individual Requests. No country found");
                    }
                }
            }
            // var stringRequest = JsonSerializer.Serialize(requests);
            return requests;
        }

        private CintRequest CreateIndividualSurvey(Project project, int audienceNumber, int? countryId, string? countryName)
        {
            isLimitFromCountryQuota = false;
            StringBuilder surveyName = new StringBuilder();
            cintRequest = new CintRequest();
            // cintRequest.name = project.Name;
            surveyName.Append(project.Name);
            cintRequest.referenceNumber = project.Reference;
            cintRequest.purchaseOrderNumber = project.Reference;
            cintRequest.contact = new contact { name = project.User.Name, company = project.User.Name, emailAddress = project.User.Email };

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

                var quotaswithKeys = new List<QuotaGrouper>();
                quotaswithKeys = ConvertISEQuotaToQuotaGroupers(ta.Quotas, quotaswithKeys);
                quotaswithKeys = ConvertISEQualificationToQuotaGroupers(ta.Qualifications, quotaswithKeys, ta.Limit);
                ConvertIseQGroupersToCintQGroups(quotaswithKeys);
            }
            else
                throw new ArgumentNullException("Target Audience is not found");

            return cintRequest;
        }

        private List<QuotaGrouper> ConvertISEQuotaToQuotaGroupers(List<Quota> iseQuotas, List<QuotaGrouper> quotaswithKeys)
        {
            foreach (var item in iseQuotas)
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
                            if (subItem.Variables.FirstOrDefault() != null)
                                qt.targetGroup.gender = subItem.Variables.FirstOrDefault().Id;
                        }
                        else if (subItem.Id == 10002 && siName == "country")
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
                if (profileVariables.Any())
                    qt.targetGroup.variableIds = profileVariables;

                quotaWithKeys.CintQuota = qt;
                quotaswithKeys.Add(quotaWithKeys);
            }
            return quotaswithKeys;
        }

        private List<QuotaGrouper> ConvertISEQualificationToQuotaGroupers(List<Qualification> iseQuals, List<QuotaGrouper> quotaswithKeys, int taLimit)
        {
            var count = 0;
            foreach (var item in iseQuals)
            {
                var subItem = item.Question;
                var currentItemFoundinQuotaList = false;
                foreach (var temp in quotaswithKeys)
                {
                    if (temp.Keywords.Contains(subItem.Name))
                    {
                        currentItemFoundinQuotaList = true;
                        break;
                    }
                }
                if (!currentItemFoundinQuotaList)
                {
                    var quotaWithKeys = new QuotaGrouper();
                    quotaWithKeys.Keywords = new List<string>();

                    var qt = new quota(); count++;
                    qt.name = string.IsNullOrEmpty(item.Name) ? (item.Question.Name + " - " + count) : item.Name;
                    qt.limit = taLimit;
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
                    }
                    else
                    {
                        foreach (var temp in subItem.Variables)
                            profileVariables.Add(temp.Id);
                    }
                    if (profileVariables.Any())
                        qt.targetGroup.variableIds = profileVariables;

                    quotaWithKeys.CintQuota = qt;
                    quotaswithKeys.Add(quotaWithKeys);
                }
            }
            return quotaswithKeys;
        }

        private void ConvertIseQGroupersToCintQGroups(List<QuotaGrouper> quotaswithKeys)
        {
            cintRequest.quotaGroups = new List<quotaGroup>();
            var cintQGroups = new List<quotaGroup>();
            foreach (var quotaKey in quotaswithKeys)
            {
                var tg = quotaKey.CintQuota.targetGroup;
                if (tg.minAge < 1 && tg.maxAge < 1
                    && tg.variableIds == null && tg.panelIds == null
                    && tg.panelVariableIds == null && tg.regionIds == null && tg.gender < 1 && tg.postalCodes == null)
                    continue;

                quotaGroup currentGroup = null;
                foreach (var item in quotaKey.Keywords)
                {
                    currentGroup = cintQGroups.Where(x => x.name.Contains(item)).FirstOrDefault();
                    if (currentGroup != null)
                        break;
                }
                if (currentGroup != null)
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
    }
}
