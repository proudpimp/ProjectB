class Program
{
    public static void Main()
    {
        RestaurantInfo restaurant = new RestaurantInfo("Wijnhaven 107","3011WN Rotterdam","0612316367","09:00/20:00, Mon/Sat");
        System.Console.WriteLine(restaurant.Info());
        var manager = new Reserveringen();

        Console.WriteLine("Welkom bij Jake's Restaurant");
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

    }
     
}