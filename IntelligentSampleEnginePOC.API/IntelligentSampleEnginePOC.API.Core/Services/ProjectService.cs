using IntelligentSampleEnginePOC.API.Core.Data;
using Model=IntelligentSampleEnginePOC.API.Core.Model;
using DBModel=IntelligentSampleEnginePOC.API.DB;
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
            if(project == null)
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

            if(string.IsNullOrEmpty(project.Name))
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

            if(status != null && status > -1 && status < 7)
            {
                currProjects = currProjects.Where(x => x.Status == status);
            }
            if(!string.IsNullOrEmpty(searchString))
            {
                currProjects = currProjects.Where(x => x.Name.Contains((string)searchString, StringComparison.CurrentCultureIgnoreCase));
            }
            if(recentCount != null && recentCount > 0)
            {
                if (recentCount > 100)
                    recentCount = 100;

                currProjects = currProjects.OrderByDescending(x => x.LastUpdate).Take((int)recentCount);
            }
            return currProjects.ToList();  
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
            foreach(var item in project.TargetAudiences)
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
    }
}
