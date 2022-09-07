using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Services
{
    public class ProjectValidator : IProjectValidator
    {
       // private ILogger _logger;
       // public ProjectValidator(ILogger logger)
       // {
            //_logger = logger;
       // }

        private List<string> errors;
        public bool IsValidated(Project project)
        {
            errors = new List<string>();
            var isValidationSuccess = true;
            if(project == null)
            {
                errors.Add("Project return is null");
                isValidationSuccess = true;
            }

            if (project != null)
            {
                ValidateProjectCategories(project);
                ValidateProjectTACountainsCountry(project);
                ValidateProjectAsTA(project);
                ValidateProjectName(project);
                ValidateProjectReference(project);
                ValidateProjectStartDate(project);
                ValidateProjectFieldingPeriod(project);
                ValidateProjectUser(project);
            }

            if (errors.Any())
                throw new ArgumentException(String.Join(";", errors));

            return true;
        }

        private void ValidateProjectTACountainsCountry(Project project)
        {
            
        }
                
        private void ValidateProjectName(Project project)
        {
            if (project.Name == null || !project.Name.Any())
            {
                errors.Add("No Project Name Identified");
            }
        }
        private void ValidateProjectReference(Project project)
        {
            if (project.Reference == null || !project.Reference.Any())
            {
                errors.Add("No Project Reference Identified");
            }
        }
        private void ValidateProjectStartDate(Project project)
        {
            if (project.StartDate == DateTime.MinValue)
            {
                 errors.Add("No Project StartDate Identified");
            }
        }
        private void ValidateProjectFieldingPeriod(Project project)
        {
            if (project.FieldingPeriod <= 0)
            {
                errors.Add("No Project FieldingPeriod Identified");
            }
        }
        private void ValidateProjectCategories(Project project)
        {
            if (project.Categories == null || !project.Categories.Any())
            {               
                errors.Add("No Categories Identified");
            }
        }
        private void ValidateProjectUser(Project project)
        {
            if (project.User == null)
            {           
            if (project.User.Name == null)               
                errors.Add("No Project User Name Identified");
            if (project.User.Email == null)
                errors.Add("No Project User Email Identified");            
            }
        }
        private void ValidateProjectAsTA(Project project)
        {
            if (project.TargetAudiences == null || !project.TargetAudiences.Any())
            {
               errors.Add("No TargetAudiences Identified");               
            }
            if (project.TargetAudiences.Count > 0 && project.TargetAudiences != null)
            {
                foreach (TargetAudience ta in project.TargetAudiences)
                {
                    if (ta.Name == null)
                        errors.Add("No TargetAudiences Name Identified");
                    if (ta.AudienceNumber <= 0)
                        errors.Add("No TargetAudiences Number Identified");
                    if (ta.EstimatedIR <= 0)
                        errors.Add("No TargetAudiences EstimatedIR Identified");
                    if (ta.EstimatedLOI <=0)
                        errors.Add("No TargetAudiences EstimatedLOI Identified");
                    /*if (ta.LimitType == null)
                        errors.Add("No TargetAudiences LimitType Identified");*/
                    if (ta.TestingUrl == null)
                        errors.Add("No TargetAudiences TestingUrl Identified");
                    if (ta.LiveUrl == null)
                        errors.Add("No TargetAudiences LiveUrl Identified");
                  foreach (Qualification qual in ta.Qualifications)
                    {
                        if (qual.Order <= 0)
                            errors.Add("No Qualification ID Identified for Target Audience : " + ta.Id + " and Target Audience Name : " + ta.Name);
                        if (qual.LogicalDecision == null)
                            errors.Add("No Qualification Logical Condition Identified for Target Audience : " + ta.Id + " and Target Audience Name : " + ta.Name);
                        if (qual.NumberOfRequiredConditions <= 0)
                            errors.Add("No Qualification Number of Required Conditions Identified for Target Audience : " + ta.Id + " and Target Audience Name : " + ta.Name);
                        if (!qual.IsActive == false || !qual.IsActive == true)
                            errors.Add("No Qualification Name Identified for Target Audience : " + ta.Id + " and Target Audience Name : " + ta.Name);
                        if (qual.Question != null)
                        {
                            var qualificationCountryCheck = ta.Qualifications.Any(i => i.Question.Name == "Country");
                            if(!qualificationCountryCheck)
                                errors.Add("Atleast one Country should be present to create Project under TA ID : " + ta.Id + " and Qualification ID : " + qual.Id);
                            if (qual.Question.Id <= 0)
                                errors.Add("No Question ID Identified for Qualification ID" + qual.Id);
                            if (qual.Question.Name == null)
                                errors.Add("No Question Name Identified for Qualification ID" + qual.Id);
                            if (qual.Question.Text == null)
                                errors.Add("No Question Text Identified for Qualification ID" + qual.Id);
                            if (qual.Question.CategoryName == null)
                                errors.Add("No Question Category Name Identified for Qualification ID" + qual.Id);
                            if (qual.Question.Variables == null && qual.Question.Variables.Count > 0)
                            {
                                foreach (var variable in qual.Question.Variables)
                                {
                                    if (variable.Id <= 0)
                                       errors.Add("No Variable ID  Identified for Qualification ID" + qual.Id);
                                    if (variable.Name == null)
                                        errors.Add("No Variable Name  Identified for Qualification ID" + qual.Id);
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
