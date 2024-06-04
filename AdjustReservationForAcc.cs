public class AdjustReservationforAcc : IAdjust
{
    public void Adjust()
    {
        AccountReservations.View();
        Console.WriteLine("Which reservation would you like to adjust? ");
        
    }
}