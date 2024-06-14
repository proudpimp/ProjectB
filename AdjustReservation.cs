public class AdjustReservation : IAdjust
{
    public void Adjust()
    {
        System.Console.WriteLine("Adjust reservation");
        System.Console.WriteLine("Enter the name of the person who made the reservation:");
        string zoekNaam = Console.ReadLine();
        System.Console.WriteLine("Enter the magic code");
        int safetyNum;
        while (!int.TryParse(Console.ReadLine(), out safetyNum) || !Reserveringen.IsMagicNumberEqual(safetyNum))
        {
            System.Console.WriteLine("Invalid input. Please enter a valid 4 digit number. ");
        }

        var reservation = Reserveringen.GetReservationByName(zoekNaam, safetyNum);
        if (reservation != null)
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
            DateTime nieuweaDatumTijd;
            while (!DateTime.TryParse(Console.ReadLine(), out nieuweaDatumTijd) || nieuweaDatumTijd < DateTime.Now ) 
            {
                System.Console.WriteLine("Invalid date. Please enter a future date and time in the format yyyy-mm-dd hh:mm:");
            }
            reservation.DatumTijd = nieuweaDatumTijd;
            Reserveringen.SaveReservations();
            Console.WriteLine("The reservation has been adjusted.");
        }
        else
        {
            System.Console.WriteLine("No reservation found under this name.");
        }
    }
}
