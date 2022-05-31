using Microsoft.AspNetCore.Mvc.Rendering;

namespace IntelligentSampleEnginePOC.UI.Models
{
    public class ProjectViewModel
    {
        public List<Project>? Projects { get; set; }
        public string Status { get; set; }
        public SelectList? Statuses { get; set; }
        public string? ProjectStatus { get; set; }
        public string? SearchString { get; set; }
    }
}
