using System.ComponentModel;
using System.Runtime.InteropServices;
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

    public DateTime BirthOfDate {get;set;}

    public string Postcode  {get;set;}

    public string PhoneNumber {get;set;}

    public Account(string name, string emailadress, string password, DateTime birthOfDate, string postcode, string phoneNumber)
    {
        Name = name;
        Emailadress = emailadress;
        Password = password;
        BirthOfDate = birthOfDate;
        Postcode = postcode;
        PhoneNumber = phoneNumber;
    }

    public void ChangeEmail(string emailadres)
    {
        Emailadress = emailadres;
    }


    public void ChangePassword(string password)
    {
        Password = password; 
    }
    public void ChangePhoneNumber(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }
    public static void SaveAccountInformationToJson()
    {

        string acc_json = JsonConvert.SerializeObject(Accounts, Formatting.Indented);
        File.WriteAllText(filepath, acc_json);
        
    }
    public static bool VoegAccountToe(string name, string emailadres, string password, DateTime birthOfDate, string postcode, string phoneNumber)
    {
        LoadAccountsFromJson();
        var nieuweAccount = new Account(name, emailadres, password, birthOfDate, postcode, phoneNumber);
        Accounts.Add(nieuweAccount);
        SaveAccountInformationToJson();
        return true;

    }
    private static void LoadAccountsFromJson()
    {
        if (File.Exists(filepath))
        {
            string json = File.ReadAllText(filepath);
            Accounts = JsonConvert.DeserializeObject<List<Account>>(json) ?? new List<Account>();
        }
    }

}