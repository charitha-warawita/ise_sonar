using System.Text.Json;
using IntelligentSampleEnginePOC.API.Core.Cint.Responses;

namespace IntelligentSampleEnginePOC.API.Core.Cint.Endpoints;

public interface ITestingEndpoint
{
    Task<GetTestResponse> TestAsync(long id);
}

public class TestingEndpoint: CintEndpoint, ITestingEndpoint
{
    public TestingEndpoint(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    public async Task<GetTestResponse> TestAsync(long id)
    {
        var uri = $"/ordering/surveys/{id}/Test";
        var response = await HttpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(content))
            throw new Exception("Did not receive a valid response from the Cint Test endpoint.");

        var data = JsonSerializer.Deserialize<GetTestResponse>(content);
        if (data is null)
            throw new Exception("Received null from Cint Test endpoint");

        return data;
    }
}