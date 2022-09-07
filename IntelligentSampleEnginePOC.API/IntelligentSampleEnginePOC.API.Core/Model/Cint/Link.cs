namespace IntelligentSampleEnginePOC.API.Core.Model.Cint;

public class Link
{
    public long SurveyId { get; set; }
    public string Key { get; }
    public string Value { get; }
    public string Type { get; }
    
    public Link(long surveyId, string key, string value, string type)
    {
        SurveyId = surveyId;
        Key = key;
        Value = value;
        Type = type;
    }
}