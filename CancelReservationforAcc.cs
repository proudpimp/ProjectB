public static class AccountCancelation
{
    public static void Cancel()
    {
        string email = Login.CurrentUserEmail;
        
        Reserveringen.AnnuleerReserveringforAcc(email);
        
        
    }
}