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
        public List<Quota> GetQuota(long qtid)
        {
            return _quotaContext.GetQuotaFromDB(qtid);
        }
    }
}
