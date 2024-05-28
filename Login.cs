using System.Text.RegularExpressions;
public static class Login
{
    public static string CurrentUserEmail{get;set;}
    public static string CurrentUserName{get;set;}

    public static bool ToAccount()
    {
        while(true)
        {
            System.Console.WriteLine("Enter your emailadress: ");
            string emailadress  = Console.ReadLine();
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            while(string.IsNullOrWhiteSpace(emailadress) || !Regex.IsMatch(emailadress, emailPattern))
            {
                Console.WriteLine("Please enter a valid email address.");
                emailadress = Console.ReadLine();
            }
            System.Console.WriteLine("Enter your password: ");
            string password = Console.ReadLine();
            while(password.Length <8)
            {
                Console.WriteLine("Password cannot be less than 8 characters.");
                password = Console.ReadLine();
            }

            if(Account.AccountExists(emailadress,password))
            {
                CurrentUserEmail = emailadress;
                CurrentUserName = Account.GetUserName(emailadress);
                System.Console.WriteLine("You have succesfully logged in.");
                MenuAfter.Log();
                return true;
            }
            else
            {
                System.Console.WriteLine("The credentials are not correct. Try again.");
            }
        }

    }
}