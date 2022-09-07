using System.Data;
using Microsoft.Data.SqlClient;

namespace IntelligentSampleEnginePOC.API.Core.Extensions;

public static class SqlDataReaderExtensions
{
    public static int? GetNullableInt32(this SqlDataReader reader, string name)
    {
        AssertNotNull(reader);
        return !reader.IsDBNull(name) ? reader.GetInt32(name) : null;
    }

    public static string? GetNullableString(this SqlDataReader reader, string name)
    {
        AssertNotNull(reader);
        return !reader.IsDBNull(name) ? reader.GetString(name) : null;
    }

    // Copied from System.Data.DataReaderExtensions.
    private static void AssertNotNull(SqlDataReader? reader)
    {
        if (reader is null)
            throw new ArgumentNullException(nameof(reader));
    }    
}