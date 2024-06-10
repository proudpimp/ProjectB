public static class ChangePostcode
{
    public static void ChangePost(Account loggedInAccount)
    {
        string newpostcode;
        do
        {
            System.Console.WriteLine("Enter your new Postcode: ");
            newpostcode = Console.ReadLine();
            if(newpostcode.Length != 6)
            {
                System.Console.WriteLine("Postcode has to have length of 4 numbers and 2 letters. ");
            }
        
        } while (newpostcode.Length != 6);
        loggedInAccount.ChangePostcode(newpostcode);
        Account.SaveAccountInformationToJson();
        Console.WriteLine("Postcode changed successfully.");





    }
}