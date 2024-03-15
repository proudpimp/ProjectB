public class Reserveringen
{
    private List<TafelReservering> reserveringen = new List<TafelReservering>();
    public const int Max6Tafels = 2;
    public const int Max2Tafels = 8;
    public const int Max4Tafels = 5;

    public bool VoegReserveringToe(string gastNaam, int aantalPersonen, DateTime datumTijd)
    {
        int tafelType = BepaalTafelType(aantalPersonen);
        if (tafelType == 0 || !ControleerBeschikbaarheid(datumTijd, tafelType))
        {
            Console.WriteLine("Helaas, er is geen beschikbaarheid op de geselecteerde datum en tijd voor het aantal personen.");
            return false;
        }
        
        var nieuweReservering = new TafelReservering(gastNaam, aantalPersonen, datumTijd, tafelType);
        reserveringen.Add(nieuweReservering);
        Console.WriteLine("Reservering succesvol toegevoegd voor " + gastNaam);
        return true;
    }

    private int BepaalTafelType(int aantalPersonen)
    {
        if (aantalPersonen <= 2) return 2;
        if (aantalPersonen <= 4) return 4;
        if (aantalPersonen <= 6) return 6;
        return 0;
    }

    public bool ControleerBeschikbaarheid(DateTime datumTijd, int tafelType)
    {
        int maxTafels;
        switch (tafelType)
        {
            case 2: maxTafels = Max2Tafels; break;
            case 4: maxTafels = Max4Tafels; break;
            case 6: maxTafels = Max6Tafels; break;
            default: return false;
        }

        return reserveringen.Count(r => r.DatumTijd.Date == datumTijd.Date && r.TafelType == tafelType) < maxTafels;
    }
}