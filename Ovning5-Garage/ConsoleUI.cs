class ConsoleUI : IUI
{
    // Visa huvudmenyn f�r anv�ndaren och returnera UserAction baserat p� anv�ndarens input.
    public UserAction ShowMainMenu(bool garageAlreadyExists = true)
    {
        Console.WriteLine("\n=======================");
        Console.WriteLine("�vning 5 - Garaget \n" + "\n1. Skapa nytt garage");
        if (garageAlreadyExists)
        {
            Console.WriteLine("2. Skapa exempelfordon i garaget"
                               + "\n3. L�gg till ett nytt fordon"
                               + "\n4. Radera fordon"
                               + "\n5. Lista alla fordon detaljerat"
                               + "\n6. Lista alla fordon per kategori"
                               + "\n7. S�k fordon via egenskaper"
                               + "\n8. S�k fordon p� reg.nr" );
        }
        Console.WriteLine("0. Avsluta");
        Console.WriteLine("-----------------------");

        while (true)
        {
            var input = Console.ReadLine();

            if (!garageAlreadyExists && input != "1" && input != "0") { Console.WriteLine("Felaktig inmatning!");
                continue;
            }

            switch (input)
            {
                case "0":
                    Environment.Exit(0);
                    break;
                case "1":
                    return UserAction.CreateNewGarage;
                case "2":
                    return UserAction.Populate;
                case "3":
                    return UserAction.Add;
                case "4":
                    return UserAction.Remove;
                case "5":
                    return UserAction.ListAll;
                case "6":
                    return UserAction.ListByCategory;
                case "7":
                    return UserAction.SearchByAttr;
                case "8":
                    return UserAction.FindByRegNr;
                default:
                    Console.WriteLine("Felaktig inmatning!");
                    break;
            }
        }
    }

    // Validera och returnera ett positivt heltal inmatat av anv�ndaren.
    public uint PromptForUint(string query = "", string successFeedback = "")
    {
        while (true)
        {
            Log(query);
            var input = Console.ReadLine();
            if (uint.TryParse(input, out uint result))
            {
                Log(successFeedback);
                return result;
            }
            else
            {
                Console.WriteLine("V�nligen ange ett positivt antal!");
            }
        }
    }

    // Metod f�r att skriva ett meddelande till konsolen?
    private void Log(string m)
    {
        System.Console.WriteLine(m);
    }
    // -------------------------------------------
    // -------------------------------------------

    public Tuple<string, string, uint, string> AskForVehicleDetails()
    {
        string vehicleType = PromptForVehicleType();
        string regNr = PromptForRegNr();
        uint wheelCount = PromptForWheelCount();
        string color = PromptForColor();

        return Tuple.Create(vehicleType, regNr, wheelCount, color);
    }

    // Ange f�rgen att s�ka efter. 
    private string PromptForColor()
    {
        Console.WriteLine("Vilken f�rg p� fordonet?");
        while (true)
        {
            var input = Console.ReadLine();

            if (input != null && input.All(l => Char.IsLetter(l)))
            {
                return input;
            }
            else { Console.WriteLine("Felaktig inmatning!"); }
        }
    }

    // Metod f�r att ange antal hjul att s�ka efter. L�s input fr�n konsolen och kontrollera om den �r giltig. 
    // Om s� �r fallet returnera v�rdet. Om inte, ge felmeddelande och forts�tt fr�ga anv�ndaren tills en giltig input givits.
    private uint PromptForWheelCount()
    {
        Console.WriteLine("Hur m�nga hjul har fordonet?");
        while (true)
        {
            var input = Console.ReadLine();

            if (uint.TryParse(input, out uint result) && result < 11)
            {
                return result;
            }
            else { Console.WriteLine("Felaktig inmatning. Ange ett antal mellan 0 och 10 hjul."); }
        }
    }

    // Metod f�r att ange fordonstyp att s�ka efter. L�s input fr�n konsolen och kontrollera om den �r giltig. 
    // Om s� �r fallet returnera som en str�ng. Om inte, ge felmeddelande och forts�tt fr�ga anv�ndaren tills en giltig input givits.
    public string PromptForVehicleType(bool permitAny = false)
    {
        Console.WriteLine("Ange typ av fordon:"
                + "\n1. Bil"
                + "\n2. Motorcykel"
                + "\n3. Buss"
                + "\n4. Flygplan"
                + "\n5. B�t"
                + (permitAny ? "\n6. Alla" : "")
        );

        while (true)
        {
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    return "Bil";
                case "2":
                    return "Motorcykel";
                case "3":
                    return "Buss";
                case "4":
                    return "Flygplan";
                case "5":
                    return "B�t";
                case "6":
                    if (permitAny)
                    { return "Alla"; }
                    else { Console.WriteLine("Felaktig inmatning!"); }
                    break;
                default:
                    Console.WriteLine("Felaktig inmatning!");
                    break;
            }
        }
    }

    // Metod f�r att ange reg.nr att s�ka efter. L�s input fr�n konsolen och kontrollera om den �r giltig. 
    // Om s� �r fallet returnera som en str�ng. Om inte, ge felmeddelande och forts�tt fr�ga anv�ndaren tills en giltig input givits.
    public string PromptForRegNr()
    {
        Console.WriteLine("Ange fordonets reg.nr:");
        while (true)
        {
            var input = Console.ReadLine();

            if ( input != null && input.Length == 6 && input.Substring(0, 3).All(w => Char.IsLetter(w)) && input.Substring(3).All(d => Char.IsDigit(d)) )
            {
                return input;
            }
            else { Console.WriteLine("Felaktig inmatning. Giltigt format p� reg.nr �r: XXX000"); }
        }
    }

    // Metod f�r att ange br�nsletyp att s�ka efter. L�s input fr�n konsolen och kontrollera om den �r giltig. 
    // Om s� �r fallet returnera br�nsletypen. Om inte, ge felmeddelande och forts�tt fr�ga anv�ndaren tills en giltig input givits.
    public FuelType PromptForFuelType()
    {
        Console.WriteLine("Ange fordonets br�nsletyp:" + "\n1. Bensin" + "\n2. Diesel");

        while (true)
        {
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    return FuelType.Bensin;
                case "2":
                    return FuelType.Diesel;
                default:
                    Console.WriteLine("Felaktig inmatning!");
                    break;
            }
        }
    }

    public double PromptForDouble(string query = "", string successMsg = "")
    {
        while (true)
        {
            Log(query);
            var input = Console.ReadLine();
            if (double.TryParse(input, out double result) && result > 0)
            {
                Log(successMsg);
                return result;
            }
            else { Console.WriteLine("V�nligen f�rs�k igen, endast siffror �r till�tna."); }
        }
    }

    // Metod f�r att ange f�rgen att s�ka efter. L�s input fr�n konsolen och kontrollera om den �r giltig. 
    // Om s� �r fallet returnera som en str�ng. Om inte, ge felmeddelande och forts�tt fr�ga anv�ndaren tills en giltig input givits.
    string IUI.PromptForColor()
    {
        Console.WriteLine("Vilken f�rg efters�kes? Ange \"Alla\" f�r att visa oavsett f�rg.");

        while (true)
        {
            var input = Console.ReadLine();

            if (input != null && input.All(l => Char.IsLetter(l)))
            {
                return input;
            }
            else { Console.WriteLine("Felaktig inmatning!"); }
        }
    }

    // Metod f�r att ange antal hjul att s�ka efter. L�s input fr�n konsolen och kontrollera om den �r giltig. 
    // Om s� �r fallet returnera v�rdet. Om inte, ge felmeddelande och forts�tt fr�ga anv�ndaren tills en giltig input givits.
    public uint? PromptWheelCountToSearchFor()
    {
        Console.WriteLine("Ange antal hjul att s�ka efter. Ange \"Alla\" f�r att visa oavsett antal hjul.");

        while (true)
        {
            var input = Console.ReadLine();

            if (uint.TryParse(input, out uint result))
            {
                return result;
            }
            else if (input != null && input.Equals("alla", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            else { Console.WriteLine("Felaktig inmatning!"); }
        }
    }
}
