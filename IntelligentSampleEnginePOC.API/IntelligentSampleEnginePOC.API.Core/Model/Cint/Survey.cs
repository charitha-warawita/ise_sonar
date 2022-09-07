namespace IntelligentSampleEnginePOC.API.Core.Model.Cint;

public class Survey
{
    public string Name { get; }
    public ICollection<Link> Links { get; set; }

    public Survey(string name)
    {
        Name = name;
        Links = new List<Link>();
    }
}