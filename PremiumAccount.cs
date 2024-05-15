public class PremiumAccount : Account
{
    public int Punten; 

    public PremiumAccount(string name, string emailadres, string password, string geboortedatum, string postcode, string telefoonnummer, int punten) : base(name, emailadres, password, geboortedatum, postcode, telefoonnummer)
    {
        Punten = punten;
    }

    
}
