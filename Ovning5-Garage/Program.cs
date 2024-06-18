// Övning 5 - Garage
// Detta är en konsolbaserad applikation som tar hjälp av ett kommandoradsgränssnitt (CLI).
// ConsoleUI representerar vyn, ansvarig för att hantera användarinmatning och utdata.
// GarageHandler representerar modellen, ansvarig för att hantera logiken och data relaterade till garaget.
// Manager representerar controllern, ansvarig för att orkestrera interaktionen mellan vyn och modellen.

internal class Program
{
    private static void Main(string[] args)
    {
        var cli = new ConsoleUI();
        var handler = new GarageHandler();
        var manager = new Manager(cli, handler);
        manager.Run();
    }
}