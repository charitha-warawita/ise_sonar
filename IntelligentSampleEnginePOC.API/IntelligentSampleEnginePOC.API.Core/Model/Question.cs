using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public class Question
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string CategoryName { get; set; }

        public List<Variable> Variables { get; set; }
    }

    public class Variable
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
