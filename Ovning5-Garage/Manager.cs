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

    // K�r programmets huvudloop, som visar huvudmenyn f�r anv�ndaren och hanterar anv�ndarinmatning.
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

    // S�k efter fordon i garaget baserat p� deras typ, f�rg och antal hjul.
    private void SearchByAttr()
    {
        System.Console.WriteLine("H�r presenteras ett antal alternativ att s�ka p�.\n");
        string vehicleType = _conUI.AskForVehicleType(permitAny: true);
        string color = _conUI.AskForColor();
        uint? wheelCount = _conUI.AskForWheelCountToSearchFor();
        _handler.SearchByAttr(vehicleType, color, wheelCount);
    }

    // S�k p� reg.nr
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
            { Console.WriteLine($"Tog bort fordonet {regNr} p� plats {slot}"); }
    }

    // L�gg till fordon
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
                    query: "Vilken �r motorcykelns toppfart (km/tim)?"
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
                uint seatCount = _conUI.AskForUint(query: "Hur m�nga sittplatser har bussen?");
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
                double wingSpan = _conUI.AskForDouble(query: "Vad �r flygplanets vingspann?");
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
                uint length = _conUI.AskForUint("Ange b�tens l�ngd (i fot):");
                slot = _handler.AddVehicle(
                    new B�t(
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
                { Console.WriteLine("Det gick inte att parkera fordonet eftersom garaget �r fullt."); }
            else
                { Console.WriteLine($"Parkerade fordonet p� plats {slot}"); }
        }
    }

    // Skapa nytt garage
    void CreateNewGarage()
    {
        uint capacity = _conUI.AskForUint(query: "Ange kapacitet f�r det nya garaget:", successFeedback: $"Ett nytt garage har skapats!");
        _handler.Create(capacity);
        _garageCreated = true;
    }
}
