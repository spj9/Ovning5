// Abstrakt fordonsklass som implementerar IVehicle-gr�nssnittet. 
// Gemensamma egenskaper; Reg.nr. Antal hjul samt fordonets f�rg.
abstract class Vehicle : IVehicle
{
    public string RegNr { get; set; }
    public uint WheelCount { get; set; }
    public string Color { get; set; }

    public Vehicle(string regNr, uint wheelCount, string color)
    {
        RegNr = regNr;
        WheelCount = wheelCount;
        Color = color;
    }

    // ToString
    public override string ToString()
    {
        return $"Fordonstyp: {this.GetType().Name}; Reg.nr: {RegNr}; Antal hjul: {WheelCount}; F�rg: {Color}";
    }
}
