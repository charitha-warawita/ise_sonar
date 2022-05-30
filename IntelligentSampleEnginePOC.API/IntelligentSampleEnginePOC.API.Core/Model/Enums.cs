using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public enum Status 
    { 
        Draft, 
        Created,
        Live,
        Paused,
        Complete,
        Closed,
        Halted
    }
}
