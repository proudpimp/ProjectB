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
    public string Info() => $"Adres: {Adres}\nPostcode: {Postcode}\nNumber: {PhoneNumber}\nOpeningstijden: {Openingstijden}";
    
}