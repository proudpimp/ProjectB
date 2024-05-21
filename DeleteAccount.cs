public static class DeleteAccount
{
    public static bool ThisAccount()
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
        while(string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Password cannot be empty.Please enter a valid Email adress.");
            password = Console.ReadLine();
        }
        System.Console.WriteLine("Enter your verification number: ");
        int verificationNumber;
        while (!int.TryParse(Console.ReadLine(), out verificationNumber))
        {
            Console.WriteLine("You cant type charachters, try again.");
        }
        if(Account.AccountExists(emailadress,password,verificationNumber))
        {
            System.Console.WriteLine("You have succesfully deleted your account.");
            return true;
        }
        else
        {
            System.Console.WriteLine("The credentials are not correct.");
            return false;
        }
    }
}