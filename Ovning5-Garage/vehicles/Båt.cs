class Båt : Vehicle
{
    public uint Length { get; set; }

    public Båt(string regNr, uint wheelCount, string color, uint length)
        : base(regNr, wheelCount, color)
    {
        Length = length;
    }
}
