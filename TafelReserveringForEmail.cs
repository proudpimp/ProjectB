public class TafelReserveringForEmail : TafelReserveringBase
{
    public string Email { get; set; }
    public TafelReserveringForEmail(string Email,string gastNaam, int aantalPersonen, DateTime datumTijd, int tafelType, string tableCode, string notitie)
        : base(gastNaam, aantalPersonen, datumTijd, tafelType, tableCode, notitie)
    {
        this.Email = Email;
    }
}