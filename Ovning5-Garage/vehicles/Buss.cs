// Buss-klassen
class Buss : Vehicle
{
    public uint SeatCount { get; set; }

    public Buss(string regNr, uint wheelCount, string color, uint seatCount)
        : base(regNr, wheelCount, color)
    {
        SeatCount = seatCount;
    }
}
