class B�t : Vehicle
{
    public uint Length { get; set; }

    public B�t(string regNr, uint wheelCount, string color, uint length)
        : base(regNr, wheelCount, color)
    {
        Length = length;
    }
}
