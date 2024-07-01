using Newtonsoft.Json;
public class TafelReserveringForGuest : TafelReservering
{
    public int SafetyNumber { get; set; }

    public TafelReserveringForGuest(string gastNaam, int aantalPersonen, DateTime datumTijd, int tafelType, string tableCode, string notitie, int safetyNumber)
        : base(gastNaam, aantalPersonen, datumTijd, tafelType, tableCode, notitie)
    {
        SafetyNumber = safetyNumber;
    }
}