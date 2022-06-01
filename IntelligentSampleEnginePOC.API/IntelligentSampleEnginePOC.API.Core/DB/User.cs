using System;
using System.Collections.Generic;

namespace IntelligentSampleEnginePOC.API.DB
{
    public partial class User
    {
        public User()
        {
            Projects = new HashSet<Project>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Project> Projects { get; set; }
    }
}
