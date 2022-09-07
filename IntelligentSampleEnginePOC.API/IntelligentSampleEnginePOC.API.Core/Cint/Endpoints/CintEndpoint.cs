namespace IntelligentSampleEnginePOC.API.Core.Cint.Endpoints;

public abstract class CintEndpoint
{
    protected readonly HttpClient HttpClient;
    protected CintEndpoint(IHttpClientFactory httpClientFactory)
    {
        HttpClient = httpClientFactory.CreateClient("CintClient");
    }
}