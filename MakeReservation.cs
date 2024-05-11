public static class MakeReservation
{

    static readonly string[] validTableCodes = new string[] { "2A", "2B", "2C", "2D", "2E", "2F", "2G", "2H", "4A", "4B", "4C", "4D", "6A", "6B" };
    public static void Make()
    {
        DateTime datumTijd;
        Console.Write("Enter the date and time of your reservation (yyyy-mm-dd hh:mm): ");
        while (!DateTime.TryParse(Console.ReadLine(), out datumTijd) || datumTijd < DateTime.Now || datumTijd.Hour < 12 || datumTijd.Hour >= 22)
        {
            Console.WriteLine("Invalid date or time. Please enter a valid date and time between 12:00 and 22:00.");
            Console.Write("Enter the date and time of your reservation (yyyy-mm-dd hh:mm): ");
        }

        Console.WriteLine("\nChecking availability for: " + datumTijd.ToString("yyyy-MM-dd HH:mm"));
        Reserveringen.GetAvailableTablesForDay(datumTijd);

        Console.Write("Fill in your name: ");
        string naam = Console.ReadLine();

        Console.Write("How many people: ");
        int aantalPersonen;
        while (!int.TryParse(Console.ReadLine(), out aantalPersonen) || aantalPersonen <= 0)
        {
            Console.WriteLine("Invalid input, try again.");
            Console.Write("How many people: ");
        }

        if (aantalPersonen > 6)
        {
            Console.WriteLine("The amount of people for this reservation exceeds the limit.");
            Console.WriteLine("Please call (31 612316367) the restaurant to complete this reservation.");
            return;
        }

        Console.WriteLine("\nChoose your table by entering the table code (e.g., 6A, 4C):");
        Console.WriteLine("\nRestaurant Table Map:");
        Console.WriteLine("  [4A]                              [4B]");
        Console.WriteLine("                   [6A]                 ");
        Console.WriteLine("[2A]   [2B]                    [2C]   [2D]");
        Console.WriteLine("");
        Console.WriteLine("[2E]   [2F]                    [2G]   [2H]");
        Console.WriteLine("                   [6B]                 ");
        Console.WriteLine("  [4C]                               [4D]");
        string[] restrictedTableCodes;

        if (aantalPersonen == 1 || aantalPersonen == 2)
        {
            List<string> tempCodes = new();
            foreach (var code in validTableCodes)
            {
                if (code[0] == '2')
                {
                    tempCodes.Add(code);
                }
            }

            restrictedTableCodes = tempCodes.ToArray();
        }



        else if (aantalPersonen == 3 || aantalPersonen == 4)
        {
            List<string> tempCodes = new List<string>();
            foreach (var code in validTableCodes)
            {
                if (code[0] == '4')
                {
                    tempCodes.Add(code);
                }
            }
            restrictedTableCodes = tempCodes.ToArray();
        }
        else if (aantalPersonen == 5 || aantalPersonen == 6)
        {
            List<string> tempCodes = new List<string>();
            foreach (var code in validTableCodes)
            {
                if (code[0] == '6')
                {
                    tempCodes.Add(code);
                }
            }
            restrictedTableCodes = tempCodes.ToArray();
        }
        else
        {
            restrictedTableCodes = validTableCodes;
        }
        Console.WriteLine("\nChoose your table by entering the table code (e.g., 6A, 4C):");
        foreach (var code in restrictedTableCodes)
        {
            if (Reserveringen.IsTableAvailable(code, datumTijd))
            {
                Console.WriteLine($"[{code}]");
            }
        }
        string tableCode;
        while (true)
        {
            tableCode = Console.ReadLine();
            if (restrictedTableCodes.Contains(tableCode, StringComparer.OrdinalIgnoreCase) && Reserveringen.IsTableAvailable(tableCode, datumTijd))
            {
                break;
            }
            Console.WriteLine("Invalid table code or not suitable for the number of people or not available. Please enter a valid table code:");
        }

        if (Reserveringen.BepaalTafelType(aantalPersonen) == 0 || !Reserveringen.ControleerBeschikbaarheid(datumTijd, Reserveringen.BepaalTafelType(aantalPersonen)))
        {
            Console.WriteLine("Unfortunately, there is no availability on the selected date and time for the number of people.");
            
            while (true)
            {
                Console.WriteLine("1) Decrease your party size.");
                Console.WriteLine("2) Quit");
                Console.Write("Make a choice: ");
                string retryChoice = Console.ReadLine();

                switch (retryChoice)
                {
                    case "1":
                        Console.Write("How many people: ");
                        while (!int.TryParse(Console.ReadLine(), out aantalPersonen) || aantalPersonen <= 0 || Reserveringen.BepaalTafelType(aantalPersonen) == 0 || !Reserveringen.ControleerBeschikbaarheid(datumTijd, Reserveringen.BepaalTafelType(aantalPersonen)))
                        {
                            Console.WriteLine("Invalid input, try again.");
                            Console.Write("How many people: ");
                        }
                        break;
                    case "2":

                        return;

                    default:
                        Console.WriteLine("Invalid choice, Choose a valid option");
                        break;
                }

                if (retryChoice == "1" || retryChoice == "2")
                    break;
            }
        }

        string notitieZelf = "";
        Console.WriteLine("Do you have a diet preference or a note for us? Y/N  ");
        string notitieAntw = Console.ReadLine().ToUpper();
        while (true)
        {
            if (notitieAntw == "Y")
            {
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Write your note or diet preference:");
                notitieZelf = Console.ReadLine()!;
                Console.WriteLine("Thanks for pointing it out, this is your note:");
                Console.WriteLine("---------------------------------");
                Console.WriteLine($"{notitieZelf}");
                Console.WriteLine("---------------------------------");
                break;
            }
            else if (notitieAntw == "N")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Fill in Y or N.");
                notitieAntw = Console.ReadLine().ToUpper();
            }
        }

        Reserveringen.VoegReserveringToe(naam, aantalPersonen, datumTijd, tableCode, notitieZelf);
    }
}