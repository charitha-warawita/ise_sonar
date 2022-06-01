using System;
using System.Collections.Generic;

namespace IntelligentSampleEnginePOC.API.DB
{
    public partial class QuotaGroupQuotum
    {
        public string Id { get; set; } = null!;
        public string QuotaGroupId { get; set; } = null!;
        public string QuotaId { get; set; } = null!;

        public virtual Quotum Quota { get; set; } = null!;
        public virtual QuotaGroup QuotaGroup { get; set; } = null!;
    }
}
