﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public User User { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime FieldPeriod { get; set; }
        public Status Status { get; set; }
        public string LinkToSurvey { get; set; }
        public List<TargetAudience> TargetAudiences { get; set; }
    }
}
