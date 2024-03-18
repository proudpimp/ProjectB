class Program
{
    public static void Main()
    {

        while (true)
        {
           
            Console.WriteLine("\nWelkom bij Jakes's restaurant");
           
            Console.WriteLine("1) Reserveren");
            Console.WriteLine("2) Reservering Annuleren");
            Console.WriteLine("3) Reservering Wijzigen");
            Console.WriteLine("4) Contactinfo");
            Console.WriteLine("5) Quit");

           
            Console.Write("Maak een keuze: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Reservering Maken");
                    var manager = new Reserveringen();

                    Console.Write("Voer uw naam in: ");
                    string naam = Console.ReadLine();

                    Console.Write("Voer het aantal personen in: ");
                    int aantalPersonen;
                    while (!int.TryParse(Console.ReadLine(), out aantalPersonen) || aantalPersonen <= 0)
                    {
                        Console.WriteLine("Ongeldige invoer, probeer het opnieuw.");
                        Console.Write("Voer het aantal personen in: ");
                    }

                    Console.Write("Voer de datum en tijd van de reservering in (yyyy-mm-dd hh:mm): ");
                    DateTime datumTijd;
                    while (!DateTime.TryParse(Console.ReadLine(), out datumTijd))
                    {
                        Console.WriteLine("Ongeldige datum, probeer het opnieuw.");
                        Console.Write("Voer de datum en tijd van de reservering in (yyyy-mm-dd hh:mm): ");
                    }

                    manager.VoegReserveringToe(naam, aantalPersonen, datumTijd);
                    break;

                case "2":
                    Console.WriteLine("Reservering Annuleren");
                    break;
                
                case "3":
                    Console.WriteLine();
                    break;

                case "4":
                    Console.WriteLine("Contactinfo: ");
                    RestaurantInfo restaurant = new RestaurantInfo("Wijnhaven 107","3011 WN Rotterdam","+ 31 612316367","09:00/20:00, Mon/Sat");
                    System.Console.WriteLine(restaurant.Info());
                    break;

                case "5":
                    Console.WriteLine("Quit");
                    return; 

                default:
                    Console.WriteLine("Ongeldige keuze, kies de goede optie");
                    break;
            }
        }
    }
}
