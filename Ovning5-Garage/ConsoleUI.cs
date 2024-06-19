class ConsoleUI : IUI
{
    // Visa huvudmenyn för användaren och returnera UserAction baserat på användarens input.
    public UserAction ShowMainMenu(bool garageAlreadyExists = true)
    {
        Console.WriteLine("\n=======================");
        Console.WriteLine("Övning 5 - Garaget \n" + "\n1. Skapa nytt garage");
        if (garageAlreadyExists)
        {
            Console.WriteLine("2. Skapa exempelfordon i garaget"
                               + "\n3. Lägg till ett nytt fordon"
                               + "\n4. Radera fordon"
                               + "\n5. Lista alla fordon detaljerat"
                               + "\n6. Lista alla fordon per kategori"
                               + "\n7. Sök fordon via egenskaper"
                               + "\n8. Sök fordon på reg.nr" );
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

    // Validera och returnera ett positivt heltal inmatat av användaren.
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
                Console.WriteLine("Vänligen ange ett positivt antal!");
            }
        }
    }

    // Metod för att skriva ett meddelande till konsolen?
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

    // Ange färgen att söka efter. 
    private string PromptForColor()
    {
        Console.WriteLine("Vilken färg på fordonet?");
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

    // Metod för att ange antal hjul att söka efter. Läs input från konsolen och kontrollera om den är giltig. 
    // Om så är fallet returnera värdet. Om inte, ge felmeddelande och fortsätt fråga användaren tills en giltig input givits.
    private uint PromptForWheelCount()
    {
        Console.WriteLine("Hur många hjul har fordonet?");
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

    // Metod för att ange fordonstyp att söka efter. Läs input från konsolen och kontrollera om den är giltig. 
    // Om så är fallet returnera som en sträng. Om inte, ge felmeddelande och fortsätt fråga användaren tills en giltig input givits.
    public string PromptForVehicleType(bool permitAny = false)
    {
        Console.WriteLine("Ange typ av fordon:"
                + "\n1. Bil"
                + "\n2. Motorcykel"
                + "\n3. Buss"
                + "\n4. Flygplan"
                + "\n5. Båt"
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
                    return "Båt";
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

    // Metod för att ange reg.nr att söka efter. Läs input från konsolen och kontrollera om den är giltig. 
    // Om så är fallet returnera som en sträng. Om inte, ge felmeddelande och fortsätt fråga användaren tills en giltig input givits.
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
            else { Console.WriteLine("Felaktig inmatning. Giltigt format på reg.nr är: XXX000"); }
        }
    }

    // Metod för att ange bränsletyp att söka efter. Läs input från konsolen och kontrollera om den är giltig. 
    // Om så är fallet returnera bränsletypen. Om inte, ge felmeddelande och fortsätt fråga användaren tills en giltig input givits.
    public FuelType PromptForFuelType()
    {
        Console.WriteLine("Ange fordonets bränsletyp:" + "\n1. Bensin" + "\n2. Diesel");

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
            else { Console.WriteLine("Vänligen försök igen, endast siffror är tillåtna."); }
        }
    }

    // Metod för att ange färgen att söka efter. Läs input från konsolen och kontrollera om den är giltig. 
    // Om så är fallet returnera som en sträng. Om inte, ge felmeddelande och fortsätt fråga användaren tills en giltig input givits.
    string IUI.PromptForColor()
    {
        Console.WriteLine("Vilken färg eftersökes? Ange \"Alla\" för att visa oavsett färg.");

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

    // Metod för att ange antal hjul att söka efter. Läs input från konsolen och kontrollera om den är giltig. 
    // Om så är fallet returnera värdet. Om inte, ge felmeddelande och fortsätt fråga användaren tills en giltig input givits.
    public uint? PromptWheelCountToSearchFor()
    {
        Console.WriteLine("Ange antal hjul att söka efter. Ange \"Alla\" för att visa oavsett antal hjul.");

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
