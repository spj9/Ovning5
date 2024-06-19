using System;

class GarageHandler : IHandler
{
    private Garage<IVehicle>? garage;
    private bool hasPop = false;

    // Lista antalet fordon i varje kategori (bilar, motorcyklar, bussar, flygplan, båtar) i garaget.
    public void ListVehiclesByCategory()
    {
        if (garage != null)
        {
            var enumerator = garage.GetEnumerator();
            int cars = 0;
            int motorbikes = 0;
            int buses = 0;
            int boats = 0;
            int airplanes = 0;

            while (enumerator.MoveNext())
            {
                switch (enumerator.Current.GetType().Name)
                {
                    case "Bil":
                        cars++;
                        break;
                    case "Motorcykel":
                        motorbikes++;
                        break;
                    case "Buss":
                        buses++;
                        break;
                    case "Flygplan":
                        airplanes++;
                        break;
                    case "Båt":
                        boats++;
                        break;

                    default:
                        break;
                }
            }
            Console.WriteLine($"Bilar: {cars}; Motorcyklar: {motorbikes}; Bussar: {buses}; Båtar: {boats}; Flygplan: {airplanes}");
        }
        else { throw new ArgumentNullException(); }
    }

    // Metod som fyller garaget med exempelfordon om det inte har fyllts på tidigare.
    public void PopulateGarage()
    {
        if (garage != null)
        {
            if (hasPop)
            {
                Console.WriteLine("Garaget har redan ett antal exempelfordon.");
                return;
            }
            IVehicle[] vehicles =
            {
                new Bil("abc123", 4, "gul", FuelType.Bensin),
                new Bil("def456", 4, "blå", FuelType.Bensin),
                new Bil("xyz098", 4, "röd", FuelType.Diesel),
                new Buss("ett234", 6, "grön", 40),
                new Motorcykel("hej567", 2, "svart", 310),
                new Båt("båt010", 0, "silver", 40),
                new Flygplan("jas339", 7, "vit", 45.5)
            };
            garage.Populate(vehicles);
            hasPop = true;
            Console.WriteLine("Exempelfordon skapades!");
        }
        else { throw new ArgumentNullException(); }
    }

    // Skapa garage
    public void Create(uint capacity)
    {
        garage = new Garage<IVehicle>(capacity);
        hasPop = false;
    }

    // Lista alla fordon
    public void ListAllVehicles()
    {
        garage?.PerformOnAll((v) => Console.WriteLine(v));
    }

    // Lägg till fordon
    public int AddVehicle(IVehicle vehicle)
    {
        if (garage != null)
        {
            var alreadyExistsCheck = garage.Find(vehicle.RegNr);
            if (alreadyExistsCheck == null)
            {
                return garage.Add(vehicle);
            }
            else
            {
                return -2;
            }
        }
        throw new ArgumentNullException();
    }

    // Ta bort fordon
    public int RemoveVehicle(string regNr)
    {
        if (garage != null)
        {
            return garage.Remove(regNr);
        }
        throw new ArgumentNullException();
    }

    // Sök på reg.nr
    public void FindByRegNr(string regNr)
    {
        if (garage != null)
        {
            var vehicle = garage.Find(regNr);
            if (vehicle == null)
            {
                Console.WriteLine($"Hittade inget fordon med reg.nr: {regNr}");
            }
            else
            {
                Console.WriteLine(vehicle.ToString());
            }
        }
        else { throw new ArgumentNullException(); }
    }

    // Sök på egenskaper
    public void SearchByAttr(string vehicleType, string color, uint? wheelCount)
    {
        if (garage != null)
        {
            var targetVehicles = garage.SearchByProps(vehicleType, color, wheelCount);

            if (!targetVehicles.Any())
                { Console.WriteLine("Inga fordon hittades"); }

            else
            {
                foreach (var item in targetVehicles)
                { if (item != null) 
                    { Console.WriteLine(item.ToString()); }
                }
            }
        }
        else { throw new ArgumentNullException(); }
    }
}
