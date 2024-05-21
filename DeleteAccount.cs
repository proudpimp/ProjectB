public static class DeleteAccount
{
    public static bool ThisAccount()
    {
        System.Console.WriteLine("Enter your emailadress: ");
        string emailadress  = Console.ReadLine();
        while(string.IsNullOrWhiteSpace(emailadress)|| !emailadress.EndsWith("@gmail.com") && !emailadress.EndsWith("@hotmail.com") && !emailadress.EndsWith("@icloud.com")
            && !emailadress.EndsWith("@outlook.com") && !emailadress.EndsWith("@yahoo.com") && !emailadress.EndsWith("@proton.me") && !emailadress.EndsWith("@aol.com"))
        {
            Console.WriteLine("Please enter a valid email address. We only accept email addresses from the following domains: @gmail.com, @hotmail.com, @icloud.com, @yahoo.com, @proton.me, @aol.com.");
            emailadress = Console.ReadLine();
        }
        System.Console.WriteLine("Enter your password: ");
        string password = Console.ReadLine();
        while(password.Length <8)
        {
            Console.WriteLine("Password cannot be empty or less than 8 characters.");
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