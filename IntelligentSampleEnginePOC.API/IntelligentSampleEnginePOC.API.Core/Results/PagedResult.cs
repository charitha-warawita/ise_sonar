namespace IntelligentSampleEnginePOC.API.Core.Results;

public class PagedResult<T>
{
    public ICollection<T> Result { get; set; } = new List<T>();
    
    // Note: Making TotalResults nullable allows us to easily identify when the result has been set.
    public int? TotalResults { get; set; }

    /// <summary>
    /// Creates a <see cref="PagedResult{T}"/> of type <see cref="T"/>, with TotalResults set to 0.
    /// </summary>
    /// <remarks>
    /// Use this method when you know the result is going to be empty. You will not be able to manipulate the Data
    /// property after using this method as it is a reference to Array.Empty.
    /// </remarks>
    /// <returns><see cref="PagedResult{T}"/> of type <see cref="T"/>.</returns>
    public static PagedResult<T> Empty()
    {
        return new PagedResult<T>
        {
            TotalResults = 0,
        };
    }
}