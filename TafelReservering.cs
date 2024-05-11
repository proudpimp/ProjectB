using Newtonsoft.Json;

public class TafelReservering
{
    public string GastNaam { get; set; }
    public int AantalPersonen { get; set; }
    public DateTime DatumTijd { get; set; }
    [JsonIgnore]
    public int TafelType { get; set; }
    public string TableCode {get; set;}
    public string Notitie{get;set;}

    public TafelReservering(string gastNaam, int aantalPersonen, DateTime datumTijd, int tafelType, string tableCode, string notitie)
    {
        GastNaam = gastNaam;
        AantalPersonen = aantalPersonen;
        DatumTijd = datumTijd;
        TafelType = tafelType;
        TableCode = tableCode;
        Notitie = notitie;
    }
}