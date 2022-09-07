namespace IntelligentSampleEnginePOC.API.Core.Helpers;

public static class EnumHelper
{
    public static TEnum SafeParse<TEnum>(int value) where TEnum : struct, Enum
    {
        var exists = Enum.IsDefined(typeof(TEnum), value);
        if (!exists)
            throw new InvalidOperationException("The given value is not valid within the provided Enum.");

        return (TEnum)(object)value;    // Can't figure out a way to avoid boxing 'value'.
    }
}