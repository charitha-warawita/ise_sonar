namespace IntelligentSampleEnginePOC.API.Core.Extensions;

public static class TaskExtensions
{
    public static async Task<T[]> WhenAll<T>(IEnumerable<Task<T>> tasks)
    {
        var aggregate = Task.WhenAll(tasks);
        try
        {
            return await aggregate;
        }
        catch (Exception)
        {
            // Ignore the exception, if multiple tasks failed this error will only detail the FIRST error thrown.
        }

        // Throw the aggregated exception, this contains ALL exceptions that may have been thrown by tasks.
        throw aggregate.Exception ?? throw new Exception("Impossible, Exception can't actually be null here.");
    }
}