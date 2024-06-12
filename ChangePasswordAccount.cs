public static class ChangePasswordAccount
{
    public static void ChangePassw(Account loggedInAccount)
    {
        Console.WriteLine("Enter your new password: ");
        string newPassword = Console.ReadLine();

        
        while (newPassword.Length < 8)
        {
            Console.WriteLine("Password cannot be less than 8 characters. Please enter a valid new password: ");
            newPassword = Console.ReadLine();
        }

      
        loggedInAccount.ChangePassword(newPassword);
        Account.SaveAccountInformationToJson();
        Console.WriteLine("Password changed successfully.");
        
    }
    
}
