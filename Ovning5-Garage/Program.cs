// Övning 5 - Garage
// Konsolbaserad applikation som tar hjälp av ett kommandoradsgränssnitt (CLI).
// ConsoleUI är vyn - ansvarig för att hantera användarinmatning och utdata.
// GarageHandler är datamodellen - ansvarig för att hantera logiken och data relaterade till garaget.
// Manager är kontrollern - ansvarig för att hantera interaktionen mellan vyn och modellen.

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