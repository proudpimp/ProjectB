public static class MakeAccount
{
    public static void NewAccount()
    {
        System.Console.WriteLine("Fill in your name: ");
        string naam = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(naam))
        {
            Console.WriteLine("Name cannot be left blank. Please enter your name.");
            naam = Console.ReadLine();
        }
        System.Console.WriteLine("Fill in your Emailadress: ");
        string eadres = Console.ReadLine();
        while(string.IsNullOrWhiteSpace(eadres))
        {
            Console.WriteLine("Email adress cannot be left blank. Please enter a valid Email adress.");
            eadres = Console.ReadLine();
        }
        Console.WriteLine("Enter your password: ");
        string password = Console.ReadLine();
        do
        {
            System.Console.WriteLine($"Password cannot be empty or less than 8 characters.");
            System.Console.WriteLine("Enter your password: ");
            password = Console.ReadLine();
        } while (password.Length <8);
        DateTime birthday;
        Console.Write("Enter the date of your birthday: ");
        while (!DateTime.TryParse(Console.ReadLine(), out birthday) || birthday < DateTime.Now.AddYears(-100))
        {
            Console.WriteLine("Invalid date or you are too old to make an account.");
            Console.Write("Enter the date of your birthday: ");
        }
        System.Console.WriteLine("Enter your Postcode: ");
        string postcode = Console.ReadLine();
        do
        {
            System.Console.WriteLine("Postcode has to have length of 4 numbers and 2 letters. ");
            postcode =  Console.ReadLine();
        } while (postcode.Length != 6);
        System.Console.WriteLine("Enter your Phone Number: ");
        string phoneNumber = Console.ReadLine();
        do
        {
            System.Console.WriteLine("Phone number must have exactly 10 digits and start with '06'");
            phoneNumber = Console.ReadLine();
        } while (phoneNumber.Length != 10 || !phoneNumber.StartsWith("06"));

        Account.VoegAccountToe(naam,eadres,password,birthday,postcode,phoneNumber);
    }
}