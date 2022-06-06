using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public class CintRequest
    {
        public string name { get; set; }
        public string referenceNumber { get; set; }
        public contact contact { get; set; }
        public string purchaseOrderNumber { get; set; }
        public int limit { get; set; }
        public int limitType { get; set; }
        public int incidenceRate { get; set; }
        public int lengthOfInterview { get; set; }
        public string linkTemplate { get; set; }
        public string testLinkTemplate { get; set; }
        public int countryId { get; set; }
        public List<int> deviceTypes { get; set; }
        public List<string> deviceCapabilities { get; set; }
        public List<int> categories { get; set; }
        public List<quotaGroup> quotaGroups { get; set; }

    }

    public class contact
    {
        public string emailAddress { get; set; }
        public string name { get; set; }
        public string company { get; set; }
    }

    public class quotaGroup
    {
        public string name { get; set; }
        public int limitType { get; set; }

        public List<quota> Quotas { get; set; }
    }

    public class quota
    {
        public string name { get; set; }
        public int limit { get; set; }

        public targetGroup targetGroup { get; set; }

    }

    public class targetGroup
    {
        public int minAge { get; set; }
        public int maxAge { get; set; }
    }
}
