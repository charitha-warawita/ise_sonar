using System.ComponentModel.DataAnnotations;

namespace IntelligentSampleEnginePOC.UI.Models
{
    public class ProjectRecordViewModel
    {
        public Guid Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }
        [StringLength(60, MinimumLength = 3)]
        public string Reference { get; set; }
        public string UserName {  get; set; }
        public string UserEmail { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime StartDate { get; set; }
        public int FieldingPeriod { get; set; }
        public Status Status { get; set; }
        public string LinkToSurvey { get; set; }
    }
}
