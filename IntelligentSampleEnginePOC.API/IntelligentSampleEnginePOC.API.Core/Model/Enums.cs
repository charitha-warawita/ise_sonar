using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public enum Status 
    { 
        Draft = 0, 
        Created = 1,
        Live = 2,
        Paused = 3,
        Complete = 4,
        Closed = 5,
        Halted = 6
    }
}
