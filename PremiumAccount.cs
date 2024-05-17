public class PremiumAccount : Account
{
    public int Punten; 

    public PremiumAccount(string name, string emailadres, string password, DateTime birthOfDate, string postcode, string phoneNumber, int punten) : base(name, emailadres, password, birthOfDate, postcode, phoneNumber)
    {
        Punten = punten;
    }

    
}
