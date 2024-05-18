public static class CancelReservation
{
    public static void Cancel()
    {
        System.Console.WriteLine("Cancel the reservation");
        System.Console.WriteLine("Enter the name of the person who made the reservation:");
        string gastNaam = Console.ReadLine();
        System.Console.WriteLine("Enter the magic numbers (4 digits): ");
        int magicnumber = Convert.ToInt32(Console.ReadLine());
        Reserveringen.AnnuleerReservering(gastNaam,magicnumber);

    }
}