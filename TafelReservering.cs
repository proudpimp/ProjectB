public class TafelReservering
{
    public string GastNaam { get; set; }
    public int AantalPersonen { get; set; }
    public DateTime DatumTijd { get; set; }
    public int TafelType { get; set; }
    public string Notitie{get;set;}

    public TafelReservering(string gastNaam, int aantalPersonen, DateTime datumTijd, int tafelType,string notitie)
    {
        GastNaam = gastNaam;
        AantalPersonen = aantalPersonen;
        DatumTijd = datumTijd;
        TafelType = tafelType;
        Notitie = notitie;
    }
}