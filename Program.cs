using Newtonsoft.Json;


static class Program
{
    static readonly string[] validTableCodes = new string[] { "2A", "2B", "2C", "2D", "2E", "2F", "2G", "2H", "4A", "4B", "4C", "4D", "6A", "6B" };

    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nWelcome at Jake's restaurant");
            Console.WriteLine("1) Make a reservation");
            Console.WriteLine("2) Cancel reservation");
            Console.WriteLine("3) Adjust reservation");
            Console.WriteLine("4) Contact info");
            Console.WriteLine("5) Menu");
            Console.WriteLine("6) Table details");
            Console.WriteLine("7) Quit");

            Console.Write("Make a choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
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
                        break;
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
                    break;

                case "2":
                    Console.WriteLine("Cancel the reservation");

                    Console.Write("Enter the name of the person who made the reservation: ");
                    string gastNaam = Console.ReadLine();

                    Reserveringen.AnnuleerReservering(gastNaam);
                    break;
                
                case "3":
                    Console.WriteLine("Adjust reservation");
                    Console.Write("Enter the name of the person who made the reservation: ");
                    string zoekNaam = Console.ReadLine();

                    var reservation = Reserveringen.GetReservationByName(zoekNaam);

                    if (reservation != null)
                    {
                        Console.WriteLine("Your current reservation details:");
                        Console.WriteLine("-------------------------------------");
                        Console.WriteLine($"Name: {reservation.GastNaam}");
                        Console.WriteLine($"Number of People: {reservation.AantalPersonen}");
                        Console.WriteLine($"Date and Time: {reservation.DatumTijd.ToString("yyyy-MM-dd HH:mm")}");
                        Console.WriteLine($"Table Type: {reservation.TafelType}");
                        Console.WriteLine($"Notes: {reservation.Notitie}");
                        Console.WriteLine($"Notes: {reservation.TableCode}");

                        Console.WriteLine("-------------------------------------");

                        Console.Write("Fill in the new date and time of the reservation (yyyy-mm-dd hh:mm): ");
                        string nieuweDatumTijd = Console.ReadLine();

                        reservation.DatumTijd = DateTime.ParseExact(nieuweDatumTijd, "yyyy-MM-dd HH:mm", null);
                        Reserveringen.SaveReservationsToJson();

                        Console.WriteLine("The reservation has been adjusted.");
                    }
                    else
                    {
                        Console.WriteLine("No reservation found under this name. ");
                    }
                    break;

                case "4":
                    RestaurantInfo restaurant = new RestaurantInfo("Wijnhaven 107","3011 WN Rotterdam","+ 31 612316367","12:00-22:00, Mon-Sat");
                    System.Console.WriteLine(restaurant.Info());
                    break;

                case "5":
                    while (true)
                    {
                        Console.WriteLine("Menu:");
                        Console.WriteLine("Discover our menu:");
                        Console.WriteLine("1) -Starters");
                        Console.WriteLine("2) -Breakfast");
                        Console.WriteLine("3) -Burgers");
                        Console.WriteLine("4) -Wraps");
                        Console.WriteLine("5) -Sandwiches");
                        Console.WriteLine("6) -Bites");
                        Console.WriteLine("Q) -Back to main menu");
                        Console.Write("Make a choice: ");
                        string MenuChoice = Console.ReadLine().ToUpper();

                        if (MenuChoice == "Q")
                            break;

                        switch (MenuChoice)
                        {
                            case "1":
                                Console.WriteLine("Starters:");
                                Console.WriteLine("1) - Basket of fries ($4)");
                                Console.WriteLine("2) - Quesadilla ($5)");
                                Console.WriteLine("3) - Chicken Tender Basket x3 ($6)");
                                Console.WriteLine("4) - Chicken Wings x4 ($7)");
                                Console.WriteLine("Q) - Return to menu");
                                string starterChoice = Console.ReadLine().ToUpper();
                                if (starterChoice == "Q")
                                {
                                    break;
                                }
                                break;
                                
                            case "2":
                                Console.WriteLine("Breakfast:");
                                Console.WriteLine("5) -Breakfast Sandwich ($10)");
                                Console.WriteLine("6) -Omelette ($7)");
                                Console.WriteLine("7) -Boiled Eggs x2 ($5)");
                                Console.WriteLine("Q) - Return to menu");

                                string BreakfastChoice = Console.ReadLine();
                                if (BreakfastChoice == "Q")
                                {
                                    break;
                                }
                                break;

                            case "3":
                                Console.WriteLine("Burgers:");
                                Console.WriteLine("8) -Cheeseburger ($8)");
                                Console.WriteLine("9) -Veggie Burger ($7)");
                                Console.WriteLine("10) -Chicken Burger ($9)");
                                Console.WriteLine("Q) - Return to menu");

                                string BurgerChoice = Console.ReadLine();
                                if (BurgerChoice == "Q")
                                {
                                    break;
                                }
                                break;

                            case "4":
                                Console.WriteLine("Wraps:");
                                Console.WriteLine("11) -Chicken Wrap ($8)");
                                Console.WriteLine("12) -Vegetable Wrap ($7)");
                                Console.WriteLine("13) -Tuna Wrap ($9)");
                                Console.WriteLine("Q) - Return to menu");

                                string WrapChoice = Console.ReadLine();
                                if (WrapChoice == "Q")
                                {
                                    break;
                                }
                                break;

                            case "5":
                                Console.WriteLine("Sandwiches:");
                                Console.WriteLine("14) -BLT Sandwich ($8)");
                                Console.WriteLine("15) -Club Sandwich ($9)");
                                Console.WriteLine("16) -Grilled Cheese Sandwich ($7)");
                                Console.WriteLine("Q) - Return to menu");

                                string SandwichChoice = Console.ReadLine();
                                if (SandwichChoice == "Q")
                                {
                                    break;
                                }
                                break;

                            case "6":
                                Console.WriteLine("Bites:");
                                Console.WriteLine("17) -Slice of Pizza ($3)");
                                Console.WriteLine("18) -Hot Dog ($4)");
                                Console.WriteLine("19) -Nachos ($6)");
                                Console.WriteLine("Q) - Return to menu");

                                string BitesChoice = Console.ReadLine();
                                if (BitesChoice == "Q")
                                {
                                    break;
                                }
                                break;

                            default:
                                Console.WriteLine("Invalid choice, Choose a valid option");
                                break;
                        }
                    }
                    break;

                case "6":
                    Console.WriteLine("We have:");
                    Console.WriteLine("- Eight two-person tables,");
                    Console.WriteLine("- Five four-person tables,");
                    Console.WriteLine("- Two six-person tables.");
                    break;
                    
                case "7":
                    Console.WriteLine("Quit");
                    return;
                default:
                    Console.WriteLine("Invalid choice, Choose a valid option");
                    break;
            }

        }
    }

}
