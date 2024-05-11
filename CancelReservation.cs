public static class CancelReservation
{
    public static void Cancel()
    {
        System.Console.WriteLine("Cancel the reservation");
        System.Console.WriteLine("Enter the name of the person who made the reservation:");
        string gastNaam = Console.ReadLine();
        Reserveringen.AnnuleerReservering(gastNaam);

    }
}