using System.Net.Http.Json;
using IntelligentSampleEnginePOC.API.Core.Model;
using Microsoft.Extensions.Logging;

namespace IntelligentSampleEnginePOC.API.Core.Cint.Endpoints;

public interface ISurveysEndpoint
{
    /// <summary>
    /// Retrieves a Survey with the given 'id' from Cint.
    /// </summary>
    /// <remarks>
    /// The id is usually the CintResponseId in the db.
    /// </remarks>
    /// <param name="id">The Cint id of the Survey to retrieve.</param>
    /// <returns>A valid <see cref="CintResponse"/>.</returns>
    public Task<CintResponse> GetAsync(long id);

    /// <summary>
    /// Creates a Cint Survey from the given <see cref="CintRequest"/>. 
    /// </summary>
    /// <param name="request">The Cint Survey to create.</param>
    /// <returns>The raw, unchecked, <see cref="HttpResponseMessage"/> of the HTTP call.</returns>
    /// <remarks>
    /// The response has not been validated. It is advised you check that the response was successful manually.
    /// </remarks>
    public Task<HttpResponseMessage?> PostAsync(CintRequest request);
}

public class SurveysEndpoint : CintEndpoint, ISurveysEndpoint
{
    private readonly ILogger<SurveysEndpoint> _logger;

    public SurveysEndpoint(IHttpClientFactory httpClientFactory, ILogger<SurveysEndpoint> logger) : base(httpClientFactory)
    {
        _logger = logger;
    }

    public async Task<CintResponse> GetAsync(long id)
    {
        var uri = $"/ordering/Surveys/{id}";
        var response = await HttpClient.GetAsync(uri);
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException e)
        {
            var url = response.RequestMessage?.RequestUri?.ToString() ?? string.Empty;
            _logger.LogError(e, "SurveysEndpoint - GetAsync - Error: Failed to retrieve surveys for '{Url}'", url);
            
            throw;
        }

        var survey = await response.Content.ReadFromJsonAsync<CintResponse>();
        if (survey is null)
            throw new Exception("Did not receive a valid Survey from Cint Surveys endpoint");

        return survey;
    }

    public async Task<HttpResponseMessage?> PostAsync(CintRequest request)
    {
        var response = await HttpClient.PostAsJsonAsync("/ordering/Surveys", request);

        return response;
    }
}