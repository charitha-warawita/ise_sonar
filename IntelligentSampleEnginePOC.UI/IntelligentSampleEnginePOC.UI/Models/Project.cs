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
            { new TargetAudience()
                {
                    Qualifications = new List<Qualification>
                    {
                        new Qualification { Id = Guid.NewGuid(), Name = "Adult Age Set 1", IsActive = true, LogicalOperator = "or", NumberOfRequiredConditions = 1, Order = 1, PreCodes = "18-25" },
                        new Qualification { Id = Guid.NewGuid(), Name = "Adult Age Set 2", IsActive = true, LogicalOperator = "or", NumberOfRequiredConditions = 1, Order = 2, PreCodes = "35-45" },
                        new Qualification { Id = Guid.NewGuid(), Name = "Adult Age Set 3", IsActive = true, LogicalOperator = "or", NumberOfRequiredConditions = 1, Order = 3, PreCodes = "55-60" },
                        new Qualification { Id = Guid.NewGuid(), Name = "Adult Age Set 4", IsActive = true, LogicalOperator = "or", NumberOfRequiredConditions = 1, Order = 4, PreCodes = "65-70" },
                    }, 
                    QuotaGroups = new List<QuotaGroup>
                    {
                        new QuotaGroup 
                        { 
                            Id = Guid.NewGuid(), 
                            Name = "Age", 
                            IsActive = true, 
                            Quotas = new List<Quota>
                            {
                                new Quota { QuotaName = "Young", Limit = 70, Precode="18-35", MinAge = 18, MaxAge = 35 },
                                new Quota { QuotaName = "Old", Limit = 30, Precode="36-70", MinAge = 36, MaxAge = 70 }
                            } 
                        },
                        new QuotaGroup
                        {
                            Id = Guid.NewGuid(),
                            Name = "Gender",
                            IsActive = true,
                            Quotas = new List<Quota>
                            {
                                new Quota { QuotaName = "Male", Limit = 50, Precode = "1", Gender = 1 },
                                new Quota { QuotaName = "Female", Limit = 50, Precode = "2", Gender = 2 }
                            }
                        }
                    }
                } 
            };

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
    }
}
