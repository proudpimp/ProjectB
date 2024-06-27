public static class UpdateAccountDetails
{
    private static Account loggedInAccount;

    public static void UpdateAccount(Account account)
    {
        loggedInAccount = account;

        while(true)
        {
            System.Console.WriteLine("1) Change Password");
            System.Console.WriteLine("2) Change Postcode");
            System.Console.WriteLine("3) Change Phone Number");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Account.ChangePassw(loggedInAccount);
                    return;
                case "2":
                    ChangePostcode.ChangePost(loggedInAccount);
                    return;
                case "3":
                    ChangePhoneNumber.Changephonenum(loggedInAccount);
                    return;
                default:
                    System.Console.WriteLine("Invalid choice");
                    break;
                    
            }
        }
    }
}