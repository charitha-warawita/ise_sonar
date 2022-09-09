﻿using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using System.ComponentModel.DataAnnotations;

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
            if (project == null)
            {
                errors.Add("Project return is null");
                isValidationSuccess = true;
            }

            if (project != null)
            {
                ValidateProjectCategories(project);
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
        private void ValidateProjectName(Project project)
        {
            if (string.IsNullOrEmpty(project.Name))
            {
                errors.Add(Constants.NoProjNameFound);
            }
        }
        private void ValidateProjectReference(Project project)
        {
            if (string.IsNullOrEmpty(project.Reference))
            {
                errors.Add(Constants.NoProjReferenceFound);
            }
        }
        private void ValidateProjectStartDate(Project project)
        {
            if (project.StartDate == DateTime.MinValue)
            {
                errors.Add(Constants.NoProjStartDateFound);
            }
        }
        private void ValidateProjectFieldingPeriod(Project project)
        {
            if (project.FieldingPeriod <= 0)
            {
                errors.Add(Constants.NoProjFieldingPeriodFound);
            }
        }
        private void ValidateProjectCategories(Project project)
        {
            if (project.Categories == null || !project.Categories.Any())
            {
                errors.Add(string.Format(Constants.NoCategoryFound, project.Id));
            }
        }
        private void ValidateProjectUser(Project project)
        {
            if (project.User != null)
            {
                if (string.IsNullOrEmpty(project.User.Name))
                    errors.Add(Constants.ProjUserNameErr);
                if (!ValidateEmail(project.User.Email))
                    errors.Add(Constants.ProjUserEmailErr);               
            }
            else
                errors.Add(string.Format(Constants.NoUserFound, project.Id));
        }
        private bool ValidateUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return false;

            Uri validatedUri;

            if (Uri.TryCreate(url, UriKind.Absolute, out validatedUri))
            {
                return (validatedUri.Scheme == Uri.UriSchemeHttp || validatedUri.Scheme == Uri.UriSchemeHttps);
            }
            return false;
        }

        private bool ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            if (new EmailAddressAttribute().IsValid(email))
                return true;
            else
                return false;
        }
        private void ValidateProjectAsTA(Project project)
        {
            if (project.TargetAudiences != null && project.TargetAudiences.Any())
            {
                var targetAudiencesUniqueName = project.TargetAudiences.GroupBy(i => i.Name).Any(g => g.Count() > 1);
                if (targetAudiencesUniqueName)
                    errors.Add(Constants.DuplicateTAName);
                var targetAudiencesUniqueOrder = project.TargetAudiences.GroupBy(i => i.AudienceNumber).Any(g => g.Count() > 1);
                if (targetAudiencesUniqueName)
                    errors.Add(Constants.DuplicateTAOrder);

                foreach (var ta in project.TargetAudiences)
                {
                    if (string.IsNullOrEmpty(ta.Name))
                        errors.Add(Constants.TANameErr);
                    if (ta.AudienceNumber < 1)
                        errors.Add(string.Format(Constants.MinValCheckErr, "Target Audience number (order)", 1));
                    if (ta.EstimatedIR < 1)
                        errors.Add(string.Format(Constants.MinValCheckErr, "Estimated IR", 1));
                    if (ta.EstimatedLOI < 1)
                        errors.Add(string.Format(Constants.MinValCheckErr, "Estimated LOI", 1));
                    if (ta.Limit < 1)
                        errors.Add(string.Format(Constants.MinValCheckErr, "Wanted Completes (or Limit)", 1));
                    if (!ValidateUrl(ta.TestingUrl))
                        errors.Add(string.Format(Constants.InvalidUrlErr, "Testing Url"));
                    if (!ValidateUrl(ta.LiveUrl))
                        errors.Add(string.Format(Constants.InvalidUrlErr, "Live Url"));
                    if (ta.Qualifications != null && ta.Qualifications.Count > 1)
                    {
                        if (!ta.Qualifications.Any(x => (x.Question != null && x.Question.Name == "Country")))
                            errors.Add(Constants.QualValidationMessage);
                    }
                    else
                    {
                        errors.Add(Constants.QualValidationMessage);
                    }
                    if (ta.Qualifications != null && ta.Qualifications.Any())
                    {
                        foreach (Qualification qual in ta.Qualifications)
                        {                            
                            if (qual.Order <1)
                                errors.Add(string.Format(Constants.MinValCheckErr, "Qualification Id", 1));
                           
                            if (qual.Question != null)
                            {                               
                                if (qual.Question.Id <1)
                                    errors.Add(string.Format(Constants.MinValCheckErr, "Qualification Question Id", 1));
                                if (string.IsNullOrEmpty(qual.Question.Name))
                                    errors.Add(string.Format(Constants.NoQualQuestionNameFound, qual.Id));
                                if (string.IsNullOrEmpty(qual.Question.Text))
                                    errors.Add(string.Format(Constants.NoQualQuestionTextFound, qual.Id));
                                if (string.IsNullOrEmpty(qual.Question.CategoryName))
                                    errors.Add(string.Format(Constants.NoQualQuestionCategoryFound, qual.Id));

                                if (qual.Question.Variables != null && qual.Question.Variables.Count > 0)
                                {
                                    foreach (var variable in qual.Question.Variables)
                                    {
                                        if (variable.Id < 1)
                                            errors.Add(string.Format(Constants.MinValCheckErr, "VarId", 1));
                                        if (string.IsNullOrEmpty(variable.Name))
                                            errors.Add(string.Format(Constants.NoQualVariableNameFound, qual.Id));
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        errors.Add(string.Format(Constants.NoQualificationFound, ta.Id));
                    }
                    if (ta.Quotas != null && ta.Quotas.Count > 1)
                    {
                        foreach (Quota qa in ta.Quotas)
                        {
                            var quotaName = ta.Quotas.GroupBy(x => x.QuotaName).Any(g => g.Count() > 1);
                            if (quotaName)
                                errors.Add(Constants.DuplicateQuotaName);                            
                            if (string.IsNullOrEmpty(qa.QuotaName))
                                errors.Add(string.Format(Constants.NoQuotaNameFound, qa.Id));                            
                             if (qa.FieldTarget < 1)
                                 errors.Add(string.Format(Constants.MinValCheckErr, "Quota FieldTarget", 1));
                             if (qa.Limit < 1)
                                 errors.Add(string.Format(Constants.MinValCheckErr, "Quota Limit", 1));                            

                            if (qa.Conditions!=null && qa.Conditions.Count > 0)
                            {
                                foreach (Question qs in qa.Conditions)
                                {
                                    if (qs.Id < 1)
                                        errors.Add(string.Format(Constants.MinValCheckErr, "Quota Question Id", 1));
                                    if (string.IsNullOrEmpty(qs.Name))
                                        errors.Add(string.Format(Constants.NoQuestionNameFound, qs.Id));
                                    if (string.IsNullOrEmpty(qs.Text))
                                        errors.Add(string.Format(Constants.NoQuestionTextFound, qs.Id));
                                    if (string.IsNullOrEmpty(qs.CategoryName))
                                        errors.Add(string.Format(Constants.NoQuestionCategoryFound, qs.Id));
                                    if (qs.Variables != null && qs.Variables.Count > 0)
                                    {
                                        foreach (Variable v in qs.Variables)
                                        {
                                            if (v.Id < 1)
                                                errors.Add(string.Format(Constants.MinValCheckErr, "VarId", 1));
                                            if (string.IsNullOrEmpty(v.Name))
                                                errors.Add(string.Format(Constants.NoQuestionVariableNameFound, qs.Id));
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        errors.Add(string.Format(Constants.NoQuotaFound, ta.Id));
                    }
                }
            }
            else
                errors.Add(string.Format(Constants.NoTAFound, project.Id));
        }

    }
}

