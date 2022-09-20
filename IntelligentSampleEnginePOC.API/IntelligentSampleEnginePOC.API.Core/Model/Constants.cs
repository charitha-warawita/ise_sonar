namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public static class Constants
    {
        //List constants here
        public static class LinkTypes
        {
            public const string Create = "Create";
            public const string Test = "Test";
        }

        public const int PAGENUMBER_DEFAULT = 1;
        public const int RECORDCOUNT_DEFAULT = 10;

        // Error descriptions
        public const string NoProjNameFound = "Project Name is invalid";
        public const string NoProjReferenceFound = "Project Reference is invalid";
        public const string NoProjStartDateFound = "Project StartDate is invalid";
        public const string NoProjFieldingPeriodFound = "Project FieldingPeriod is invalid";
        public const string ProjUserNameErr = "User name is invalid";
        public const string ProjUserEmailErr = "User Email is invalid";
        public const string NoCategoryFound = "Project Categories not available in project id: {0}";
        public const string NoUserFound = "User not available in project id: {0}";
        public const string ProjValFailed = "Project validation failed big time.. You must be ashamed!!!";
        public const string NoTAFound = "Target Audience not available in project id: {0}";
        public const string TANameErr = "Target Audience name is invalid";
        public const string MinValCheckErr = "{0} is invalid. Minimum value allowed is {1}";
        public const string InvalidUrlErr = "Invalid Url entered in the field - {0}";
        public const string QualValidationMessage = "Each TA must contain atleast one country selected in the qualification " +
                                                    "list under STANDARD category and should have atleast one more additional qualification like gender, age or " +
                                                    "profiling questions to support quota groups";
        public const string DuplicateTAName = "Duplicate Target Audience Name Identified ";
        public const string DuplicateTAOrder = "Duplicate Target Audience Order Identified ";       
        public const string NoQualVariableNameFound = "Variable Name not available for Qualification: {0}";
        public const string NoQualQuestionNameFound = "Question Name not available for Qualification: {0}";
        public const string NoQualQuestionTextFound = "Question Text not available for Qualification: {0}";
        public const string NoQualQuestionCategoryFound = "Question Category not available for Qualification: {0}";
        public const string DuplicateQuotaName = "Duplicate Quota Name Identified ";
        public const string NoQuotaNameFound = "Quota Name not available for Target Audience: {0}";
        public const string NoQuotaTypeFound = "Quota Type not available for Target Audience: {0}";
        public const string NoQuestionNameFound = "Question Name not available for Quota: {0}";
        public const string NoQuestionTextFound = "Question Text not available for Quota: {0}";
        public const string NoQuestionCategoryFound = "Question Category not available for Quota: {0}";
        public const string NoQuestionVariableNameFound = "Variable Name not available for Question: {0}";
        public const string NoQualificationFound = "Qualification not available in Target Audience id: {0}";
        public const string NoQuotaFound = "Quota not available in Target Audience id: {0}";
        public const string StartDateAndFieldingPeriodErr = "Field period days count {0} from start date {1} is set to be in the past. Set start date in the future or set start date and fielding period such that it is in the current active period.";
    }
}
