using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.UI.Models
{
    public class TargetAudience
    {
        public TargetAudience()
        {
            Name = String.Empty;
            Qualifications = new List<Qualification>
            {
                new Qualification { Id = Guid.NewGuid(), Name = "Adult Age Set 1", IsActive = true, LogicalOperator = "or", NumberOfRequiredConditions = 1, Order = 1, PreCodes = "18-25" },
                new Qualification { Id = Guid.NewGuid(), Name = "Adult Age Set 2", IsActive = true, LogicalOperator = "or", NumberOfRequiredConditions = 1, Order = 2, PreCodes = "35-45" },
                new Qualification { Id = Guid.NewGuid(), Name = "Adult Age Set 3", IsActive = true, LogicalOperator = "or", NumberOfRequiredConditions = 1, Order = 3, PreCodes = "55-60" },
                new Qualification { Id = Guid.NewGuid(), Name = "Adult Age Set 4", IsActive = true, LogicalOperator = "or", NumberOfRequiredConditions = 1, Order = 4, PreCodes = "65-70" },
            };
            QuotaGroups = new List<QuotaGroup>
            {
                new QuotaGroup
                {
                    Id = Guid.NewGuid(),
                    Name = "Age",
                    IsActive = true,
                    Quotas = new List<Quota>
                    {
                        new Quota { Id = Guid.NewGuid(), QuotaName = "Young", Limit = 70, Precode="18-35", MinAge = 18, MaxAge = 35 },
                        new Quota { Id = Guid.NewGuid(), QuotaName = "Old", Limit = 30, Precode="36-70", MinAge = 36, MaxAge = 70 }
                    }
                },
                new QuotaGroup
                {
                    Id = Guid.NewGuid(),
                    Name = "Gender",
                    IsActive = true,
                    Quotas = new List<Quota>
                    {
                        new Quota { Id = Guid.NewGuid(), QuotaName = "Male", Limit = 50, Precode = "1", Gender = 1 },
                        new Quota { Id = Guid.NewGuid(), QuotaName = "Female", Limit = 50, Precode = "2", Gender = 2 }
                    }
                }
            };
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TANumber { get; set; }
        public int Limit { get; set; }
        public string LimitType { get; set; }
        public List<Qualification> Qualifications { get; set; }
        public List<QuotaGroup> QuotaGroups { get; set; }
    }
}
