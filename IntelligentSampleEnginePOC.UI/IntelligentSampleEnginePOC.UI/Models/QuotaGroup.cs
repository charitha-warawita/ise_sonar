namespace IntelligentSampleEnginePOC.UI.Models
{
    public class QuotaGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<Quota> Quotas { get; set; }
    }
}
