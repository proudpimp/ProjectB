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
        System.Console.WriteLine("Enter your password: ");
        string password = Console.ReadLine();
        while(password.Length <8)
        {
            Console.WriteLine("Password cannot be empty or less than 8 characters.");
            password = Console.ReadLine();
        }

        if(Account.AccountExists(emailadress,password))
        {
            System.Console.WriteLine("You have succesfully logged in.");
            return true;
        }
        else
        {
            System.Console.WriteLine("The credentials are not correct.");
            return false;
        }

    }
}