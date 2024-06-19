// Flygplansklassen
class Flygplan : Vehicle
{
    public double WingSpan { get; set; }

    public Flygplan(string regNr, uint wheelCount, string color, double wingSpan)
        : base(regNr, wheelCount, color)
    {
        WingSpan = wingSpan;
    }
}
