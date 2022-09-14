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
        public const string ProjValFailed = "Project validation failed.";
        public const string NoTAFound = "Target audience not available in project id: {0}";
        public const string TANameErr = "Target Audience name is invalid";
        public const string MinValCheckErr = "{0} is invalid. Minimum value allowed is {1}";
        public const string InvalidUrlErr = "Invalid Url entered in the field - {0}";
        public const string QualValidationMessage = "Each TA must contain atlast one country selected in the qualification " +
            "list under STANDARD category and should have atleast one more additional qualification like gender, age or " +
            "profiling questions to support quota groups";




        public const string QUALIFICATIONCOUNTERCHECKSTART = "Atleast two Qualification should be present to create a project under Target Audience : ";
        public const string QUALIFICATIONCOUNTERCHECKEND = " provided one of which should be country details qualification";
        public const string TANAME =  " and Target Audience Name : ";
        public const string QORDER = "No Qualification Order Identified for Target Audience : ";
        public const string QLOGICALDICISION= "No Qualification Logical Condition Identified for Target Audience : ";
        public const string QNOOFREQUIREDCONDITIONS = "No Qualification Number of Required Conditions Identified for Target Audience : ";
        public const string TAISACTIVE = "No Qualification Data Identified IsActive value for Target Audience : ";
        public const string QID = " and Qualification ID : ";      
        public const string QUALIFICATIONCOUNTRYCHECK = "Atleast one Country should be present to create Project under TA ID : ";
        public const string QQUESTIONID = "No Question ID Identified for Qualification ID : ";
        public const string QQUESTIONNAME = "No Question Name Identified for Qualification ID : ";
        public const string QQUESTIONTEXT = "No Question Text Identified for Qualification ID : ";
        public const string QCATEGORYNAME = "No Question Category Name Identified for Qualification ID : ";
        public const string VARIABLEID = "No Variable ID  Identified for Qualification ID : ";
        public const string VARIABLENAME = "No Variable Name  Identified for Qualification ID : ";


    }
}
