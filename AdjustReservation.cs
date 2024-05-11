public static class AdjustReservation
{
    public static void Adjust()
    {
        System.Console.WriteLine("Adjust reservation");
        System.Console.WriteLine("Enter the name of the person who made the reservation:");
        string zoekNaam = Console.ReadLine();
        
        var reservation = Reserveringen.GetReservationByName(zoekNaam);
        if(reservation != null)
        {
            System.Console.WriteLine("Your current reservation details:");
            System.Console.WriteLine("-------------------------------------");
            System.Console.WriteLine($"Name: {reservation.GastNaam}");
            System.Console.WriteLine($"Number of People: {reservation.AantalPersonen}");
            System.Console.WriteLine($"Date and Time: {reservation.DatumTijd.ToString("yyyy-MM-dd HH:mm")}");
            System.Console.WriteLine($"Notes: {reservation.Notitie}");
            System.Console.WriteLine($"Tablecode: {reservation.TableCode}");
            System.Console.WriteLine("-------------------------------------");

            System.Console.Write("Fill in the new date and time of the reservation (yyyy-mm-dd hh:mm): ");
            string nieuweDatumTijd = Console.ReadLine();
            reservation.DatumTijd = DateTime.ParseExact(nieuweDatumTijd, "yyyy-MM-dd HH:mm", null);
            Reserveringen.SaveReservationsToJson();
            Console.WriteLine("The reservation has been adjusted.");
        }
        else
        {
            System.Console.WriteLine("No reservation found under this name.");
        }
    }
}