using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public class CintResponse
    {
        public int id { get; set; }
        public int status { get; set; }
        public string name { get; set; }
        public int limit { get; set; }
        public contact contact { get; set; }
        public DateTime created { get; set; }
        public int limitType { get; set; }
        public int incidenceRate { get; set; }
        public int lengthOfInterview { get; set; }
        public string linkTemplate { get; set; }
        public string testLinkTemplate { get; set; }
        public string referenceNumber { get; set; }
        public string purchaseOrderNumber { get; set; }
        public List<int> deviceTypes { get; set; }
        public List<string> deviceCapabilities { get; set; }
        public Cpi cpi { get; set; }
        public int countryId { get; set; }
        public bool respondentIdentifiableInfo { get; set; }
        public List<quotaGroup> quotaGroups { get; set; }
        public List<int> categories { get; set; }
        public List<string> dedupeTags { get; set; }
        public List<string> excludedTags { get; set; }
        public List<string> excludedByTags { get; set; }
        public statistics statistics { get; set; }
        public List<link> links { get; set; }
        public List<string> errors { get; set; }
        public int priceSource { get; set; }
        public List<int> targetedPanelIds { get; set; }
        public int pricingStrategy { get; set; }
        public int fieldPeriod { get; set; }
        public bool useDeduplication { get; set; }
        public bool checkGeoIp { get; set; }
    }

    public class Cpi
    {
        public decimal amount { get; set; }
        public string currency { get; set; }
    }

    public class statistics
    {
        public int responded { get; set; }
        public int started { get; set; }
        public int inSurvey { get; set; }
        public int completes { get; set; }
        public int loiCompletes { get; set; }
        public int screenouts { get; set; }
        public int quotaFulls { get; set; }
        public int timeouts { get; set; }
        public int qualityTerminates { get; set; }
    }

    public class link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }
}
