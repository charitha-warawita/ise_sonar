namespace IntelligentSampleEnginePOC.API.Core.Model;

public class Cost
{
    public double Amount { get; }
    public string Currency { get; }

    public Cost(double amount, string currency)
    {
        if (currency.Length != 3)
            throw new ArgumentOutOfRangeException(nameof(currency), "Currency code is invalid");
        
        Amount = amount;
        Currency = currency;
    }
}