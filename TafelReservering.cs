using Newtonsoft.Json;
public class TafelReservering : TafelReserveringBase
{
    public int SafetyNumber { get; set; }

    public TafelReservering(string gastNaam, int aantalPersonen, DateTime datumTijd, int tafelType, string tableCode, string notitie, int safetyNumber)
        : base(gastNaam, aantalPersonen, datumTijd, tafelType, tableCode, notitie)
    {
        SafetyNumber = safetyNumber;
    }
}