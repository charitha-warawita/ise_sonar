using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public class Qualification
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LogicalOperator {  get; set; }
        public int NumberOfRequiredConditions { get; set; }
        public bool IsActive {  get; set; }
        public string PreCodes {  get; set; }
        public int Order {  get; set; }
    }
}
