public static class ChangePhoneNumber
{
    public static void Changephonenum(Account loggedInAccount)
    {
        string newphonenum;
        do
        {
            System.Console.WriteLine("Enter your Phone Number: ");
            newphonenum = Console.ReadLine();
            if(newphonenum.Length != 10 || !newphonenum.StartsWith("06"))
            {

                System.Console.WriteLine("Phone number must have exactly 10 digits and start with '06'");
            }
        } while (newphonenum.Length != 10 || !newphonenum.StartsWith("06"));
        loggedInAccount.ChangePhoneNumber(newphonenum);
        Account.SaveAccountInformationToJson();
        Console.WriteLine("Phone number changed successfully.");





    }
}