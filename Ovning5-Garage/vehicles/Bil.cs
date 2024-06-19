// Enums för bränsletyper
public enum FuelType
{
    Diesel,
    Bensin
}

// Bil-klassen
class Bil : Vehicle
{
    public FuelType FuelType { get; set; }

    public Bil(string regNr, uint wheelCount, string color, FuelType fuelType)
        : base(regNr, wheelCount, color)
    {
        FuelType = fuelType;
    }
}
