// MC-klassen
class Motorcykel : Vehicle
{
    public uint TopSpeed { get; set; }

    public Motorcykel(string regNr, uint wheelCount, string color, uint topSpeed)
        : base(regNr, wheelCount, color)
    {
        TopSpeed = topSpeed;
    }
}
