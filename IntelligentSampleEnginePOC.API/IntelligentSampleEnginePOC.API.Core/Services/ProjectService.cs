using IntelligentSampleEnginePOC.API.Core.Data;
using Model = IntelligentSampleEnginePOC.API.Core.Model;
using DBModel = IntelligentSampleEnginePOC.API.Core.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using IntelligentSampleEnginePOC.API.Core.DB;

namespace IntelligentSampleEnginePOC.API.Core.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ISEdbContext _dataContext;
        

        private ICintService cintService { get; set; }

        public ProjectService(ISEdbContext context, ICintService cintService)
        {
            _dataContext = context;
            this.cintService = cintService;
        }
        public Model.Project CreateProject(Model.Project project)
        {
            if (project == null)
                throw new ArgumentNullException("Project model not found", nameof(project));


            project = SetupGuids(project);
            project.LastUpdate = DateTime.UtcNow;
            project.Status = Model.Status.Draft;
            project.CintResponseId = 0;
            ModelMapping(project, true);
            _dataContext.SaveChanges();

            return project;
        }

        private Model.Project SetupGuids(Model.Project project)
        {
            //Setting up IDs
            if (project?.Id == Guid.Empty)
                project.Id = Guid.NewGuid();

            if (project?.User != null && project?.User?.Id == Guid.Empty)
                project.User.Id = Guid.NewGuid();

            if (project.TargetAudiences.Any())
            {
                foreach (var item in project.TargetAudiences)
                {
                    if (item?.Id == Guid.Empty)
                        item.Id = Guid.NewGuid();

                    if (item.Qualifications.Any())
                    {
                        foreach (var subItem in item.Qualifications)
                        {
                            if (subItem?.Id == Guid.Empty)
                                subItem.Id = Guid.NewGuid();
                        }
                    }
                }
            }
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
            project = SetupGuids(project);
            project.LastUpdate = DateTime.UtcNow;
            project = await cintService.CallCint(project);
            project.Status = Model.Status.Created;
            ModelMapping(project, true);
            _dataContext.SaveChanges();
            return project;
        }

        public List<Project> GetProjects(int? status, string? searchString, int? recentCount)
        {
            IEnumerable<Project> currProjects = _dataContext.Projects;

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
            IEnumerable<Project> currProjects = _dataContext.Projects;

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

            if(!currProject.TargetAudiences.Any())
            {
                var targetAudience = _dataContext.TargetAudiences.Where(x => x.ProjectId == currProject.Id).FirstOrDefault();
                if(targetAudience != null)
                {
                    projectModel.TargetAudiences = new List<Model.TargetAudience>();
                    var taModel = new Model.TargetAudience
                    {
                        Id = Guid.Parse(targetAudience.Id), TANumber = targetAudience.Tanumber ?? 0, Name = targetAudience.Name, Limit = targetAudience.Limit ?? 0, LimitType = targetAudience.LimitType ?? String.Empty,
                    };
                    taModel.Qualifications = new List<Model.Qualification>();
                    taModel.QuotaGroups = new List<Model.QuotaGroup>();

                    var qualifications = _dataContext.Qualifications.Where(x => x.TargetAudienceId == targetAudience.Id).ToList();
                    if(qualifications.Any())
                    {
                        foreach(var item in qualifications)
                        {
                            taModel.Qualifications.Add(new Model.Qualification
                            {
                                Id = Guid.Parse(item.Id), IsActive = item.IsActive?? false, LogicalOperator = item.LogicalOperator ?? String.Empty, Name = item.Name, 
                                NumberOfRequiredConditions = item.NumberOfRequiredConditions ?? 0, Order = item.QualificationOrder?? 0, PreCodes = item.PreCodes ?? String.Empty
                            });
                        }
                    }
                    projectModel.TargetAudiences.Add(taModel);
                }
            }

            return projectModel;
        }

        public Model.Project UpdateProject(Model.Project project)
        {
            if (project == null)
                throw new ArgumentNullException("Project model not found", nameof(project));
            project.LastUpdate = DateTime.UtcNow;
            project.Status = Model.Status.Draft;
            project.CintResponseId = 0;
            ModelMapping(project, false);
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

        private void ModelMapping(Model.Project project, bool isCreateCall)
        {
            var tempProject = JsonSerializer.Deserialize<Project>(JsonSerializer.Serialize(project));

            //Manually map Quotas
            /*if(project.TargetAudiences.Any())
            {
                for(int i= 0; i<project.TargetAudiences.Count; i++)
                {
                    if (project.TargetAudiences[i].QuotaGroups.Any())
                    {
                        for (int j =0; j < project.TargetAudiences[i].QuotaGroups.Count; j++)
                        { 
                        }
                    }
                }
            }*/

            if (tempProject != null)
            {
                if (isCreateCall)
                    _dataContext.Add(tempProject);
                else
                    _dataContext.Update(tempProject);
            }
        }

    }
}
