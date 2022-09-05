using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Services
{
    public class QuotaService : IQuotaService
    {
        private readonly ILogger _logger;
        private readonly IQuotaContext _quotaContext;
        public QuotaService(ILogger<QuotaService> logger, IQuotaContext quotaContext)
        {
            _logger = logger;
            _quotaContext = quotaContext;
        }
        public Quota GetQuota(long qtid)
        {
            return _quotaContext.GetQuota(qtid);
        }

        public Quota CreateQuota(long projectId, long? taid, Quota qtaData)
        {
            if (taid == null)
                throw new ArgumentNullException("TA Id  is not found");
            if (qtaData == null)
                throw new ArgumentNullException("Quota model  is not found", nameof(qtaData));

            if (QuotaValidated(qtaData))
                return _quotaContext.CreateQuota(projectId, (long)taid, qtaData);

            throw new ArgumentException("Target Audience Validation failed", nameof(qtaData));
        }

        private bool QuotaValidated(Quota qtaData)
        {
            return true;
        }



        public long DeleteQuota(long qtid)
        {
            return _quotaContext.DeleteQuotaFromDB(qtid);
        }

    }
}
