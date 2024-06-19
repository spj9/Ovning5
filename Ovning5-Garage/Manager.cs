class Manager
{
    private IUI _conUI;
    private IHandler _handler;
    private bool _garageCreated = false;

    public Manager(IUI cui, IHandler handler)
    {
        _conUI = cui;
        _handler = handler;
    }

    // Kör programmets huvudloop, som visar huvudmenyn för användaren och hanterar användarinmatning.
    public void Run()
    {
        while (true)
        {
            UserAction input = _conUI.ShowMainMenu(_garageCreated);

            switch (input)
            {
                case UserAction.CreateNewGarage:
                    CreateNewGarage();
                    break;
                case UserAction.ListAll:
                    _handler.ListAllVehicles();
                    break;
                case UserAction.Add:
                    AddVehicle();
                    break;
                case UserAction.Remove:
                    RemoveVehicle();
                    break;
                case UserAction.FindByRegNr:
                    FindByRegNr();
                    break;
                case UserAction.ListByCategory:
                    _handler.ListVehiclesByCategory();
                    break;
                case UserAction.Populate:
                    _handler.PopulateGarage();
                    break;
                case UserAction.SearchByAttr:
                    SearchByAttr();
                    break;
            }
        }
    }

    // Sök efter fordon i garaget baserat på deras typ, färg och antal hjul.
    private void SearchByAttr()
    {
        System.Console.WriteLine("Här presenteras ett antal alternativ att söka på.\n");
        string vehicleType = _conUI.AskForVehicleType(permitAny: true);
        string color = _conUI.AskForColor();
        uint? wheelCount = _conUI.AskForWheelCountToSearchFor();
        _handler.SearchByAttr(vehicleType, color, wheelCount);
    }

    // Sök på reg.nr
    private void FindByRegNr()
    {
        string regNr = _conUI.AskForRegNr();
        _handler.FindByRegNr(regNr);
    }

    // Ta bort fordon
    private void RemoveVehicle()
    {
        string regNr = _conUI.AskForRegNr();
        int slot = _handler.RemoveVehicle(regNr);
        if (slot == -1)
            { Console.WriteLine("Fordonet kan inte hittas i garaget"); }
        else
            { Console.WriteLine($"Tog bort fordonet {regNr} på plats {slot}"); }
    }

    // Lägg till fordon
    private void AddVehicle()
    {
        Tuple<string, string, uint, string> vehicleDetails = _conUI.AskForVehicleDetails();
        int slot;
        switch (vehicleDetails.Item1)
        {
            case "Car":
                FuelType fuelType = _conUI.AskForFuelType();
                slot = _handler.AddVehicle(
                    new Bil(
                        vehicleDetails.Item2,
                        vehicleDetails.Item3,
                        vehicleDetails.Item4,
                        fuelType
                    )
                );
                PrintFeedBack(slot);
                break;
            case "Motorbike":
                uint topSpeed = _conUI.AskForUint(
                    query: "Vilken är motorcykelns toppfart (km/tim)?"
                );
                slot = _handler.AddVehicle(
                    new Motorcykel(
                        vehicleDetails.Item2,
                        vehicleDetails.Item3,
                        vehicleDetails.Item4,
                        topSpeed
                    )
                );
                PrintFeedBack(slot);
                break;
            case "Bus":
                uint seatCount = _conUI.AskForUint(query: "Hur många sittplatser har bussen?");
                slot = _handler.AddVehicle(
                    new Buss(
                        vehicleDetails.Item2,
                        vehicleDetails.Item3,
                        vehicleDetails.Item4,
                        seatCount
                    )
                );
                PrintFeedBack(slot);
                break;
            case "Airplane":
                double wingSpan = _conUI.AskForDouble(query: "Vad är flygplanets vingspann?");
                slot = _handler.AddVehicle(
                    new Flygplan(
                        vehicleDetails.Item2,
                        vehicleDetails.Item3,
                        vehicleDetails.Item4,
                        wingSpan
                    )
                );
                PrintFeedBack(slot);
                break;
            case "Boat":
                uint length = _conUI.AskForUint("Ange båtens längd (i fot):");
                slot = _handler.AddVehicle(
                    new Båt(
                        vehicleDetails.Item2,
                        vehicleDetails.Item3,
                        vehicleDetails.Item4,
                        length
                    )
                );
                PrintFeedBack(slot);
                break;
            default:
                throw new ArgumentException();
        }

        void PrintFeedBack(int slot)
        {
            if (slot == -2)
                { Console.WriteLine("Ett fordon med det reg.nr finns redan i garaget!"); }
            else if (slot == -1)
                { Console.WriteLine("Det gick inte att parkera fordonet eftersom garaget är fullt."); }
            else
                { Console.WriteLine($"Parkerade fordonet på plats {slot}"); }
        }
    }

    // Skapa nytt garage
    void CreateNewGarage()
    {
        uint capacity = _conUI.AskForUint(query: "Ange kapacitet för det nya garaget:", successFeedback: $"Ett nytt garage har skapats!");
        _handler.Create(capacity);
        _garageCreated = true;
    }
}
