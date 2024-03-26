using Newtonsoft.Json;


class Program
{
    public static void Main()
    {

        while (true)
        {
            Console.WriteLine("\nWelcome by Jake's restaurant");
            Console.WriteLine("1) Make a reservation");
            Console.WriteLine("2) Cancel reservation");
            Console.WriteLine("3) Adjust reservation");
            Console.WriteLine("4) Contactinfo");
            Console.WriteLine("5) Table details");
            Console.WriteLine("6) Quit");
            Console.Write("Make a choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Make a reservation");
                    var manager = new Reserveringen();

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
                            Console.WriteLine("The amount of people for this reservation exceeds the limit");
                            Console.WriteLine("Please call (31 612316367) the restaurant to complete this reservation");
                            break;
                        }

                    while(true)
                    {
                    Console.WriteLine("Do you want to note something? Y/N  ");
                    string notitieAntw = Console.ReadLine().ToUpper()!; 
                        if (notitieAntw == "Y")
                        {  
                           Console.WriteLine("---------------------------------");
                           Console.WriteLine("Write your note:"); 
                           Console.WriteLine("---------------------------------");
                           string notitieZelf = Console.ReadLine()!;
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
                            Console.WriteLine("Invalid input. fill in Y or N.");
                        }
                        
                      
                    }


                    Console.Write("Enter the date and time of your reservation (yyyy-mm-dd hh:mm): ");
                    DateTime datumTijd;
                    while (!DateTime.TryParse(Console.ReadLine(), out datumTijd))
                    {
                        Console.WriteLine("Invalid date or time , try again");
                        Console.Write("Enter the date and time of your reservation (yyyy-mm-dd hh:mm): ");
                    }

                    manager.VoegReserveringToe(naam, aantalPersonen, datumTijd);
                    break;

                case "2":
                    Console.WriteLine("Cancel the reservation");

                    Console.Write("Enter the name of the person who made the reservation: ");
                    string gastNaam = Console.ReadLine();

                    Reserveringen reserveringAnnuleren = new Reserveringen();
                    reserveringAnnuleren.AnnuleerReservering(gastNaam);
                    break;
                
                case "3":
                    Console.WriteLine("Adjust reservation");
                    Console.Write("Enter the name of the person who made the reservation: ");
                    string zoekNaam = Console.ReadLine();

                    Reserveringen reserveringen = new Reserveringen();
                    var reservation = reserveringen.GetReservationByName(zoekNaam);

                    if (reservation != null)
                    {
                        Console.WriteLine("Your current reservation details:\n" + JsonConvert.SerializeObject(reservation, Formatting.Indented));

                        Console.Write("Fill in the new date and time of the reservation (yyyy-mm-dd hh:mm): ");
                        string nieuweDatumTijd = Console.ReadLine();

                        reservation.DatumTijd = DateTime.ParseExact(nieuweDatumTijd, "yyyy-MM-dd HH:mm", null);
                        reserveringen.SaveReservationsToJson();

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
                    Console.WriteLine("We have:");
                    Console.WriteLine("- Eight two-person tables,");
                    Console.WriteLine("- Five four-person tables,");
                    Console.WriteLine("- Two six-person tables.");
                    break;

                case "6":
                    Console.WriteLine("Quit");
                    return;

                default:
                    Console.WriteLine("Invalid choice, Choose a valid option");
                    break;
            }
        }
    }
}
