using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Http.Controllers;
using IntelligentSampleEnginePOC.API.Http.Tests.MockModelData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;

namespace IntelligentSampleEnginePOC.API.Http.Tests.Controller
{
    internal class QuotaControllerTest
    {
        private Mock<ILogger<QuotaController>> _logger;
        private Mock<IQuotaService> _quotaService;
        private QuotaController _quotaController;

        public QuotaControllerTest()
        {
            _logger = new Mock<ILogger<QuotaController>>();
            _quotaService = new Mock<IQuotaService>();
            _quotaController = new QuotaController(_logger.Object, _quotaService.Object);
        }

    }
}
