using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
public class Account
{
    private static List<Account> Accounts = new();

    private static string filepath
    {
        get
        {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "GitHub", "ProjectB", "Accounts.json");
            }
            else
            {
                return "./Accounts.json";
            }
        }
    }
    public string Name {get;set;}
    public string Emailadress {get;set;}

    public string Password {get;set;}

    public DateTime DateOfBirth {get;set;}

    public string Postcode  {get;set;}

    public string PhoneNumber {get;set;}
    public int verificationNumber{get;set;}


    static Account()
    {
        LoadAccountsFromJson();
    }
    public Account(string name, string emailadress, string password, DateTime dateOfBirth, string postcode, string phoneNumber,int verificationNumber)
    {
        Name = name;
        Emailadress = emailadress;
        Password = password;
        DateOfBirth = dateOfBirth;
        Postcode = postcode;
        PhoneNumber = phoneNumber;
        this.verificationNumber = verificationNumber;
    }


    public void ChangePassword(string password)
    {
        Password = password; 
    }
    public void ChangePhoneNumber(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }
    public void ChangePostcode(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }
    public static void SaveAccountInformationToJson()
    {

        string acc_json = JsonConvert.SerializeObject(Accounts, Formatting.Indented);
        File.WriteAllText(filepath, acc_json);
        
    }
    public static bool VoegAccountToe(string name, string emailadres, string password, DateTime dateOfBirth, string postcode, string phoneNumber,int verificationNumber)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            System.Console.WriteLine("Name cannot be empty.");
            return false;
        }
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (!Regex.IsMatch(emailadres,emailPattern))
        {
            System.Console.WriteLine("Email needs to be a valid email adress.");
            return false;
        }
        if(password.Length < 8)
        {
            System.Console.WriteLine("Password cannot be less than 8 characters.");
            return false;
        }
        if(postcode.Length != 6)
        {
            System.Console.WriteLine("Postcode has to have length of 4 numbers and 2 letters.");
            return false;
        }
        if(phoneNumber.Length != 10 || !phoneNumber.StartsWith("06"))
        {
            System.Console.WriteLine("Phone number must have exactly 10 digits and start with '06'");
            return false;
        }
        var nieuweAccount = new Account(name, emailadres, password, dateOfBirth, postcode, phoneNumber,verificationNumber);
        Accounts.Add(nieuweAccount);
        System.Console.WriteLine($"Account succesfully created for {name}");
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string fileName = $"Account_{name}_Confirmation.txt";
        string fullPath = Path.Combine(folderPath, fileName);
        string bevestigingTekst = "Thank you for creating an account at Jake's Restaurant. We look forward to welcoming you! \nBelow are the details of your account:\n\n" +
                            $"Name: {name}\n" +
                            $"Emailadress: {emailadres}\n" +
                            $"Password: {password}\n" +
                            $"DateOfBirth: {dateOfBirth}\n" +
                            $"Postcode: {postcode}\n" +
                            $"PhoneNumber: {phoneNumber}\n" +
                            $"**VerificationNumber: {verificationNumber}";
        File.WriteAllText(fullPath,bevestigingTekst);
        System.Console.WriteLine($"The account creation confirmation has been succesfully saved for {name} in {fullPath}.");
        SaveAccountInformationToJson();
        return true;

    }
    public static bool AccountExists(string emailadres,string password)
    {
        foreach (var account in Accounts)
        {
            if (account.Emailadress == emailadres && account.Password == password)
            {
                return true;
            }
        }
        return false;
    }
    public static bool AccountExists(string emailadres,string password,int verificationNumber)
    {
        foreach (var account in Accounts)
        {
            if (account.Emailadress == emailadres && account.Password == password && verificationNumber == account.verificationNumber)
            {
                Accounts.Remove(account);
                SaveAccountInformationToJson();
                return true;
            }
        }
        return false;
    }
    public static string GetUserName(string email)
    {
        foreach(var account in Accounts)
        {
            if(account.Emailadress == email)
            {
                return account.Name;
            }
        }
        return null;
    }
    private static void LoadAccountsFromJson()
    {
        if (File.Exists(filepath))
        {
            string json = File.ReadAllText(filepath);
            Accounts = JsonConvert.DeserializeObject<List<Account>>(json) ?? new List<Account>();
        }
    }

    public static List<Account> GetAccounts()
    {
        return Accounts;
    }

    public static Account GetAccount(string email)
    {
        return Accounts.First(account => account.Emailadress == email);
    }

}