class RestaurantInfo
{
    public string Adres;
    public string Postcode;
    public string PhoneNumber;
    public string Openingstijden;

    public RestaurantInfo(string Adres,string Postcode, string PhoneNumber, string Openingstijden)
    {
        this.Adres = Adres;
        this.Postcode = Postcode;
        this.PhoneNumber = PhoneNumber;
        this.Openingstijden = Openingstijden;
    }
    public string Info() => $"Contact Info:\nAdress: {Adres}\nZipcode: {Postcode}\nNumber: {PhoneNumber}\nOpening/Closing times: {Openingstijden}";
    
}