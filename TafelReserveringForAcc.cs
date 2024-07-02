public class TafelReserveringForAcc : TafelReservering
{
    public string Email { get; set; }
    public TafelReserveringForAcc(string Email,string gastNaam, int aantalPersonen, DateTime datumTijd, int tafelType, string tableCode, string notitie)
        : base(gastNaam, aantalPersonen, datumTijd, tafelType, tableCode, notitie)
    {
        this.Email = Email;
    }

    public static int CalculatePoints(TafelReserveringForAcc reservering)
    {
        int PointsToAdd = reservering.AantalPersonen * 10;

        return PointsToAdd;
    }
    
    
}