public static class Login
{
    public static bool ToAccount()
    {
        System.Console.WriteLine("Enter your emailadress: ");
        string emailadress  = Console.ReadLine();
        while(string.IsNullOrWhiteSpace(emailadress))
        {
            Console.WriteLine("Email adress cannot be left blank. Please enter a valid Email adress.");
            emailadress = Console.ReadLine();
        }
        System.Console.WriteLine("Enter your passowrd: ");
        string password = Console.ReadLine();
        while(string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Password cannot be empty.Please enter a valid Email adress.");
            password = Console.ReadLine();
        }

        if(Account.AccountExists(emailadress,password))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}