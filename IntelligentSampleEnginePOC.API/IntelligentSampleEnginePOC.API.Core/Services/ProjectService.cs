using IntelligentSampleEnginePOC.API.Core.Data;
using Model = IntelligentSampleEnginePOC.API.Core.Model;
using DBModel = IntelligentSampleEnginePOC.API.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace IntelligentSampleEnginePOC.API.Core.Services
{
    public class ProjectService : IProjectService
    {
        private readonly DBModel.ISEdbContext _dataContext;
        private DBModel.ProjectTargetAudience projectTargetAudience { get; set; }
        private DBModel.QuotaGroupQuotum quotaGroupQuotum { get; set; }
        private DBModel.TargetAudienceQualification targetAudienceQualification { get; set; }
        private DBModel.TargetAudienceQuotaGroup TargetAudienceQuotaGroup { get; set; }

        private ICintService cintService { get; set; }

        public ProjectService(DBModel.ISEdbContext context, ICintService cintService)
        {
            _dataContext = context;
            this.cintService = cintService;
        }
        public Model.Project CreateProject(Model.Project project)
        {
            if (project == null)
                throw new ArgumentNullException("Project model not found", nameof(project));
            project.Status = Model.Status.Draft;
            project.CintResponseId = 0;
            ModelMapping(project);
            _dataContext.SaveChanges();

            return project;
        }

        public async Task<Model.Project> LaunchProject(Model.Project project)
        {
            if (project == null)
                throw new ArgumentNullException("project model not found", nameof(project));

            if (string.IsNullOrEmpty(project.Name))
            {
                // This means the project ID and first we need to retrieve all project details and then form json request to CINT API
            }

            project = await cintService.CallCint(project);
            project.Status = Model.Status.Created;
            ModelMapping(project);
            _dataContext.SaveChanges();
            return project;
        }

        public List<DBModel.Project> GetProjects(int? status, string? searchString, int? recentCount)
        {
            IEnumerable<DBModel.Project> currProjects = _dataContext.Projects;

            if (status != null && status > -1 && status < 7)
            {
                currProjects = currProjects.Where(x => x.Status == status);
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                currProjects = currProjects.Where(x => x.Name.Contains((string)searchString, StringComparison.CurrentCultureIgnoreCase));
            }
            if (recentCount != null && recentCount > 0)
            {
                if (recentCount > 100)
                    recentCount = 100;

                currProjects = currProjects.OrderByDescending(x => x.LastUpdate).Take((int)recentCount);
            }
            return currProjects.ToList();
        }
        public Model.Project GetProjects(string? id)

        {
            IEnumerable<DBModel.Project> currProjects = _dataContext.Projects;

            var currProject = currProjects.Where(x => x.Id == id).FirstOrDefault();
            var projectModel = new Model.Project();
            projectModel.Id = Guid.Parse(currProject.Id);
            projectModel.Name = currProject.Name;
            projectModel.Reference = currProject.Reference != null ? currProject.Reference : String.Empty;
            projectModel.StartDate = currProject.StartDate != null ? currProject.StartDate.Value : DateTime.MinValue;
            projectModel.FieldingPeriod = currProject.FieldingPeriod != null ? currProject.FieldingPeriod.Value : 0;
            projectModel.LinkToSurvey = currProject.LinkToSurvey != null ? currProject.LinkToSurvey : String.Empty;
            projectModel.User = new Model.User();
            if (!string.IsNullOrEmpty(currProject.UserId) && currProject.User == null)
            {
                currProject.User = _dataContext.Users.Where(x => x.Id == currProject.UserId).FirstOrDefault();
                if (currProject.User != null)
                {

                    projectModel.User.Id = Guid.Parse(currProject.User.Id);// new Guid(currProject.User.Id);
                    projectModel.User.Name = currProject.User.Name;
                    projectModel.User.Email = currProject.User.Email;

                }
            }
            var taList = currProject.ProjectTargetAudiences.ToList();
            var taProject = _dataContext.ProjectTargetAudiences.Where(x => x.ProjectId == currProject.Id).FirstOrDefault();
            if (taProject != null)
            {
                var taId = taProject.TargetAudienceId;
                var ta = _dataContext.TargetAudiences.Where(x => x.Id == taId).FirstOrDefault();
                if (ta != null)
                {
                    projectModel.TargetAudiences = new List<Model.TargetAudience>()
                    {
                        new Model.TargetAudience()

                        {
                            Id = Guid.Parse(ta.Id),
                            Name =ta.Name,
                            TANumber =ta.Tanumber != null ? ta.Tanumber.Value : 0,
                            Limit = ta.Limit != null ? ta.Limit.Value : 0,
                            LimitType = String.IsNullOrEmpty(ta.LimitType) ? String.Empty : ta.LimitType,

                        }

                    };
                    var taQualList = _dataContext.TargetAudienceQualifications.Where(x => x.TargetAudienceId == ta.Id).ToList();
                    var idsList = new List<string>();
                    if (taQualList.Any()) 
                    {
                        foreach (var taQual in taQualList)
                        {
                            idsList.Add(taQual.QualificationId);
                        }
                        var qaList = _dataContext.Qualifications;//.Where(x => idsList.Contains(x.Id)).ToList();
                        if (qaList.Any())
                        {
                            projectModel.TargetAudiences[0].Qualifications = new List<Model.Qualification>();
                            foreach (var qa in qaList)
                            {
                                var qaQual = new Model.Qualification();
                                qaQual.Id = Guid.Parse(qa.Id);
                                qaQual.Name = qa.Name;
                                qaQual.IsActive = qa.IsActive != null ? qa.IsActive.Value : false;
                                qaQual.PreCodes = String.IsNullOrEmpty(qa.PreCodes) ? String.Empty : qa.PreCodes;
                                qaQual.LogicalOperator = String.IsNullOrEmpty(qa.LogicalOperator) ? String.Empty : qa.LogicalOperator;
                                qaQual.NumberOfRequiredConditions = qa.NumberOfRequiredConditions != null ? qa.NumberOfRequiredConditions.Value : 0;
                                qaQual.Order = qa.QualificationOrder != null ? qa.QualificationOrder.Value : 0;
                                projectModel.TargetAudiences[0].Qualifications.Add(qaQual);
                            }
                        }
                    }
                    var taQuotaList = _dataContext.TargetAudienceQuotaGroups.Where(x => x.TargetAudienceId == ta.Id).ToList();
                    var idQuotaList = new List<string>();
                    if (taQuotaList.Any())
                    {
                        foreach (var taQuota in taQuotaList)
                        {
                            idQuotaList.Add(taQuota.QuotaGroupId);
                        }
                        //idQuotaList.Clear();
                        //idQuotaList.Add("49ea854b-fa04-4718-a2df-c52dd1d76dad");
                        //idQuotaList.Add("eb7ef5d4-c4ba-41dd-9e25-d49e2eec0258");

                        var quotaList = _dataContext.QuotaGroups.Where(x => idQuotaList.Contains(x.Id)).ToList();
                        var idList = new List<string>();

                        if(quotaList.Any())
                        {
                            foreach(var item in quotaList)
                            {
                                idList.Add(item.Id);
                            }
                        }


                        var quotaItemsList = _dataContext.Quota.Where(x => idList.Contains(x.Id)).ToList();
                        if (quotaList.Any())
                        {
                            projectModel.TargetAudiences[0].QuotaGroups = new List<Model.QuotaGroup>();
                            foreach (var qu in quotaList)
                            {
                                var quotaData = new Model.QuotaGroup();
                                quotaData.Name = qu.Name;
                                quotaData.IsActive = qu.IsActive != null ? qu.IsActive.Value : false;
                                projectModel.TargetAudiences[0].QuotaGroups.Add(quotaData);
                                //quotaData.Name = qu.Name;
                                //quotaData.limi = qu.Limit != null && qu.Limit.Value > 0;
                            }
                        }
                    }

                }
            }
            
            return projectModel;
        }

        public Model.Project UpdateProject(Model.Project project)
        {
            if (project == null)
                throw new ArgumentNullException("Project model not found", nameof(project));
            project.Status = Model.Status.Draft;
            project.CintResponseId = 0;
            ModelMapping(project);
            _dataContext.SaveChanges();

            return project;
        }
        /// <summary>
        /// Gopal to Gopal-
        /// This is dirty way of mapping model object to DB object using JSONSerializer (very smart but not wise). 
        /// Find better way of mapping or for ideal use - come up with stored procedure setup.
        /// This solution is only for POC purposes
        /// </summary>
        /// <param name="project"></param>

        private void ModelMapping(Model.Project project)
        {
            var tempProject = JsonSerializer.Deserialize<DBModel.Project>(JsonSerializer.Serialize(project));
            var tempTAList = new List<DBModel.TargetAudience>();
            if (project.Id == null)
            {
                foreach (var item in project.TargetAudiences)
                {
                    var tempTA = JsonSerializer.Deserialize<DBModel.TargetAudience>(JsonSerializer.Serialize(item));
                    _dataContext.Add(new DBModel.ProjectTargetAudience { Id = Guid.NewGuid().ToString(), Project = tempProject, TargetAudience = tempTA });

                    if (_dataContext.Qualifications.Any())
                    {
                        foreach (var existingQ in _dataContext.Qualifications)
                        {
                            _dataContext.Add(new DBModel.TargetAudienceQualification { Id = Guid.NewGuid().ToString(), TargetAudience = tempTA, Qualification = existingQ });
                        }
                    }
                    else
                    {
                        foreach (var subItem in item.Qualifications)
                        {
                            var tempQ = JsonSerializer.Deserialize<DBModel.Qualification>(JsonSerializer.Serialize(subItem));
                            _dataContext.Add(new DBModel.TargetAudienceQualification { Id = Guid.NewGuid().ToString(), TargetAudience = tempTA, Qualification = tempQ });
                        }
                    }

                    if (_dataContext.QuotaGroups.Any())
                    {
                        foreach (var existingQG in _dataContext.QuotaGroups)
                        {
                            _dataContext.Add(new DBModel.TargetAudienceQuotaGroup { Id = Guid.NewGuid().ToString(), TargetAudience = tempTA, QuotaGroup = existingQG });
                        }
                    }
                    else
                    {
                        foreach (var subItem2 in item.QuotaGroups)
                        {
                            var tempQuotaGroup = JsonSerializer.Deserialize<DBModel.QuotaGroup>(JsonSerializer.Serialize(subItem2));
                            _dataContext.Add(new DBModel.TargetAudienceQuotaGroup { Id = Guid.NewGuid().ToString(), TargetAudience = tempTA, QuotaGroup = tempQuotaGroup });

                            foreach (var lowerItem in subItem2.Quotas)
                            {
                                var tempQuota = JsonSerializer.Deserialize<DBModel.Quotum>(JsonSerializer.Serialize(lowerItem));
                                _dataContext.Add(new DBModel.QuotaGroupQuotum { Id = Guid.NewGuid().ToString(), QuotaGroup = tempQuotaGroup, Quota = tempQuota });
                            }
                        }
                    }
                }
            }

            else
            {
                
                    foreach (var item in project.TargetAudiences)
                    {
                        var tempTA = JsonSerializer.Deserialize<DBModel.TargetAudience>(JsonSerializer.Serialize(item));
                        _dataContext.Add(new DBModel.ProjectTargetAudience { Id = project.Id.ToString(), Project = tempProject, TargetAudience = tempTA });

                        if (_dataContext.Qualifications.Any())
                        {
                            foreach (var existingQ in _dataContext.Qualifications)
                            {
                                _dataContext.Add(new DBModel.TargetAudienceQualification { Id = project.Id.ToString(), TargetAudience = tempTA, Qualification = existingQ });
                            }
                        }
                        else
                        {
                            foreach (var subItem in item.Qualifications)
                            {
                                var tempQ = JsonSerializer.Deserialize<DBModel.Qualification>(JsonSerializer.Serialize(subItem));
                                _dataContext.Add(new DBModel.TargetAudienceQualification { Id = project.Id.ToString(), TargetAudience = tempTA, Qualification = tempQ });
                            }
                        }

                        if (_dataContext.QuotaGroups.Any())
                        {
                            foreach (var existingQG in _dataContext.QuotaGroups)
                            {
                                _dataContext.Add(new DBModel.TargetAudienceQuotaGroup { Id = project.Id.ToString(), TargetAudience = tempTA, QuotaGroup = existingQG });
                            }
                        }
                        else
                        {
                            foreach (var subItem2 in item.QuotaGroups)
                            {
                                var tempQuotaGroup = JsonSerializer.Deserialize<DBModel.QuotaGroup>(JsonSerializer.Serialize(subItem2));
                                _dataContext.Add(new DBModel.TargetAudienceQuotaGroup { Id = project.Id.ToString(), TargetAudience = tempTA, QuotaGroup = tempQuotaGroup });

                                foreach (var lowerItem in subItem2.Quotas)
                                {
                                    var tempQuota = JsonSerializer.Deserialize<DBModel.Quotum>(JsonSerializer.Serialize(lowerItem));
                                    _dataContext.Add(new DBModel.QuotaGroupQuotum { Id = project.Id.ToString(), QuotaGroup = tempQuotaGroup, Quota = tempQuota });
                                }
                            }
                        }
                    }
       
            }
            foreach (var item in project.TargetAudiences)
            {
                var tempTA = JsonSerializer.Deserialize<DBModel.TargetAudience>(JsonSerializer.Serialize(item));
                _dataContext.Update(new DBModel.ProjectTargetAudience { Id = project.Id.ToString(), Project = tempProject, TargetAudience = tempTA });

                if (_dataContext.Qualifications.Any())
                {
                    foreach (var existingQ in _dataContext.Qualifications)
                    {
                        _dataContext.Update(new DBModel.TargetAudienceQualification { Id = project.Id.ToString(), TargetAudience = tempTA, Qualification = existingQ });
                    }
                }
                else
                {
                    foreach (var subItem in item.Qualifications)
                    {
                        var tempQ = JsonSerializer.Deserialize<DBModel.Qualification>(JsonSerializer.Serialize(subItem));
                        _dataContext.Update(new DBModel.TargetAudienceQualification { Id = project.Id.ToString(), TargetAudience = tempTA, Qualification = tempQ });
                    }
                }

                if (_dataContext.QuotaGroups.Any())
                {
                    foreach (var existingQG in _dataContext.QuotaGroups)
                    {
                        _dataContext.Update(new DBModel.TargetAudienceQuotaGroup { Id = project.Id.ToString(), TargetAudience = tempTA, QuotaGroup = existingQG });
                    }
                }
                else
                {
                    foreach (var subItem2 in item.QuotaGroups)
                    {
                        var tempQuotaGroup = JsonSerializer.Deserialize<DBModel.QuotaGroup>(JsonSerializer.Serialize(subItem2));
                        _dataContext.Update(new DBModel.TargetAudienceQuotaGroup { Id = project.Id.ToString(), TargetAudience = tempTA, QuotaGroup = tempQuotaGroup });

                        foreach (var lowerItem in subItem2.Quotas)
                        {
                            var tempQuota = JsonSerializer.Deserialize<DBModel.Quotum>(JsonSerializer.Serialize(lowerItem));
                            _dataContext.Update(new DBModel.QuotaGroupQuotum { Id = project.Id.ToString(), QuotaGroup = tempQuotaGroup, Quota = tempQuota });
                        }
                    }
                }
            }
        }

    }
}
