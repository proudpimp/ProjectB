public class PremiumAccount : Account
{
    public int Punten; 

    public PremiumAccount(string name, string emailadres, string password, DateTime birthOfDate, string postcode, string phoneNumber, int punten,int verificationNumber) : base(name, emailadres, password, birthOfDate, postcode, phoneNumber,verificationNumber)
    {
        Punten = punten;
    }

    
}
