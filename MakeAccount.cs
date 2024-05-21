public static class MakeAccount
{
    static readonly Random random = new Random();
    public static void NewAccount()
    {
        int verificationNum = random.Next(1000,10000);
        System.Console.WriteLine("Fill in your name: ");
        string name = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Name cannot be left blank. Please enter your name.");
            name = Console.ReadLine();
        }
        System.Console.WriteLine("Fill in your Emailadress: ");
        string eadres = Console.ReadLine();
        while(string.IsNullOrWhiteSpace(eadres)|| !eadres.EndsWith("@gmail.com") && !eadres.EndsWith("@hotmail.com") && !eadres.EndsWith("@icloud.com")
            && !eadres.EndsWith("@outlook.com") && !eadres.EndsWith("@yahoo.com") && !eadres.EndsWith("@proton.me") && !eadres.EndsWith("@aol.com"))
        {
            Console.WriteLine("Email adress cannot be left blank. Please enter a valid Email adress.");
            eadres = Console.ReadLine();
        }
        string password;
        do
        {
            Console.WriteLine("Enter your password: ");
            password = Console.ReadLine();
            if(password.Length <8)
            {
                System.Console.WriteLine($"Password cannot be empty or less than 8 characters.");
            }
        } while (password.Length <8);
        DateTime birthday;
        Console.Write("Enter the date of your birthday: ");
        while (!DateTime.TryParse(Console.ReadLine(), out birthday) || birthday < DateTime.Now.AddYears(-100))
        {
            Console.WriteLine("Invalid date or you are too old to make an account.");
            Console.Write("Enter the date of your birthday: ");
        }
        string postcode;
        do
        {
            System.Console.WriteLine("Enter your Postcode: ");
            postcode = Console.ReadLine();
            if(postcode.Length != 6)
            {
                System.Console.WriteLine("Postcode has to have length of 4 numbers and 2 letters. ");
            }
        } while (postcode.Length != 6);
        string phoneNumber;
        do
        {
            System.Console.WriteLine("Enter your Phone Number: ");
            phoneNumber = Console.ReadLine();
            if(phoneNumber.Length != 10 || !phoneNumber.StartsWith("06"))
            {

                System.Console.WriteLine("Phone number must have exactly 10 digits and start with '06'");
            }
        } while (phoneNumber.Length != 10 || !phoneNumber.StartsWith("06"));

        Account.VoegAccountToe(name,eadres,password,birthday,postcode,phoneNumber,verificationNum);
    }
}