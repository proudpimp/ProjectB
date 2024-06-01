public static class AccountReservations
{
    public static void View()
    {
        string email = Login.CurrentUserEmail;
        var reserveringen = Reserveringen.GetReservationByEmail(email);
        if (reserveringen != null)
        {
            foreach(var reservering in reserveringen)
            {
                System.Console.WriteLine("Your current reservation details:");
                System.Console.WriteLine("-------------------------------------");
                System.Console.WriteLine($"Email: {reservering.Email}");
                System.Console.WriteLine($"Name: {reservering.GastNaam}");
                System.Console.WriteLine($"Number of People: {reservering.AantalPersonen}");
                System.Console.WriteLine($"Date and Time: {reservering.DatumTijd.ToString("yyyy-MM-dd HH:mm")}");
                System.Console.WriteLine($"Notes: {reservering.Notitie}");
                System.Console.WriteLine($"Tablecode: {reservering.TableCode}");
                System.Console.WriteLine("-------------------------------------");

            }
        }
    }
}