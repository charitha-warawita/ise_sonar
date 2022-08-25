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
        private ILogger _logger;
        public ProjectValidator(ILogger logger)
        {
            _logger = logger;
        }

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

            ValidateProjectAsTA(project);
            ValidateProjectTACountainsCountry(project);


            if (errors.Any())
                throw new ArgumentException(String.Join(";", errors));

            return true;

            // throw new NotImplementedException();
        }

        private void ValidateProjectTACountainsCountry(Project project)
        {
            
        }

        private void ValidateProjectAsTA(Project project)
        {
            if(project.TargetAudiences == null || !project.TargetAudiences.Any())
            {
                // _logger.Log(LogLevel.Error, "ISE-ERR05", Constants.PROJVALFAILED);
                errors.Add("No target Audience Identified");
            }
        }



    }
}
