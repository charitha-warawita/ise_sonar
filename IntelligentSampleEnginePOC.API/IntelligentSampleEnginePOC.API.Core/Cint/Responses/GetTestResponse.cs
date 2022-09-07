using System.Text.Json.Serialization;
using IntelligentSampleEnginePOC.API.Core.Model;

namespace IntelligentSampleEnginePOC.API.Core.Cint.Responses;

public class GetTestResponse
{
    [JsonPropertyName("links")] 
    public List<link> Links { get; set; } = new();
}