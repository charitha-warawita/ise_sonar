using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.UI.Models
{
    public class Project
    {
        public Project()
        {
            TargetAudiences = new List<TargetAudience>() 
            { new TargetAudience() };

            CintResponseId = 0;
            CintSelfLink = "NA";
            CintCurrentCostLink = "NA";
            CintTestingLink = "NA";
        }

        public Guid Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }
        [StringLength(60, MinimumLength = 3)]
        public string Reference { get; set; }
        public User User { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime StartDate { get; set; }
        public int FieldingPeriod { get; set; }
        public Status Status { get; set; }
        public string LinkToSurvey { get; set; }
        public List<TargetAudience> TargetAudiences { get; set; }
        public int CintResponseId { get; set; }
        public string CintSelfLink { get; set; }
        public string CintCurrentCostLink { get; set; }
        public string CintTestingLink { get; set; }
    }
}
