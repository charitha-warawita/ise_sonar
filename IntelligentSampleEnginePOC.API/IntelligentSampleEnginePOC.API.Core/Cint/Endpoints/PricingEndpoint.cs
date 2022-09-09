using System.Net.Http.Json;
using IntelligentSampleEnginePOC.API.Core.Cint.Responses;
using Microsoft.Extensions.Logging;

namespace IntelligentSampleEnginePOC.API.Core.Cint.Endpoints;

public interface IPricingEndpoint
{
    Task<CurrentCostResponse> GetCurrentCostAsync(long id);
}

public class PricingEndpoint: CintEndpoint, IPricingEndpoint
{
    private readonly ILogger<CintEndpoint> _logger;
    
    public PricingEndpoint(
        IHttpClientFactory httpClientFactory,
        ILogger<PricingEndpoint> logger) : base(httpClientFactory)
    {
        _logger = logger;
    }

    public async Task<CurrentCostResponse> GetCurrentCostAsync(long id)
    {
        var uri = $"/surveys/{id}/CurrentCost";
        var response = await HttpClient.GetAsync(uri);

        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException e)
        {
            var url = response.RequestMessage?.RequestUri?.ToString() ?? string.Empty;
            _logger.LogError(
                e,
                "PricingEndpoint - GetCurrentCostAsync: Failed to retrieve current cost for '{Url}'",
                url);

            throw;
        }

        var cost = await response.Content.ReadFromJsonAsync<CurrentCostResponse>();
        if (cost is null)
            throw new Exception("Did not receive a valid CurrentCost from Cint CurrentCost endpoint");

        return cost;
    }
}