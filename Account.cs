using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
public class Account : IAdjust
{
    private static List<Account> Accounts = new();

    static readonly Random random = new Random();

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

    public int Points {get;set;}

    static readonly string[] validTableCodes = new string[] { "2A", "2B", "2C", "2D", "2E", "2F", "2G", "2H", "4A", "4B", "4C", "4D", "4E", "6A", "6B" };

    public static string CurrentUserEmail{get;set;}
    public static string CurrentUserName{get;set;}

    public static Account loggedInAccount;
    
    


    static Account()
    {
        LoadAccountsFromJson();
    }
    public Account(string name, string emailadress, string password, DateTime dateOfBirth, string postcode, string phoneNumber,int verificationNumber,int points = 0)
    {
        Name = name;
        Emailadress = emailadress;
        Password = password;
        DateOfBirth = dateOfBirth;
        Postcode = postcode;
        PhoneNumber = phoneNumber;
        this.verificationNumber = verificationNumber;
        Points = points;
    }


    public void ChangePassword(string password)
    {
        Password = password; 
    }
    public void ChangePhoneNumber(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }
    public void ChangePostcode(string postcode)
    {
        Postcode = postcode;
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

    
    public static void View()
    {
        string email = CurrentUserEmail;
        var reserveringen = Reserveringen.GetReservationByEmail(email);
        if (reserveringen != null)
        {
            foreach(var reservering in reserveringen)
            {
                System.Console.WriteLine("Your current reservation details:");
                System.Console.WriteLine("-------------------------------------");
                System.Console.WriteLine($"Email: {reservering.Email}");
                System.Console.WriteLine($"Name: {reservering.GastNaam}");
                System.Console.WriteLine($"Number of People: {reservering.AantalPersonen}");
                System.Console.WriteLine($"Date and Time: {reservering.DatumTijd.ToString("yyyy-MM-dd HH:mm")}");
                System.Console.WriteLine($"Notes: {reservering.Notitie}");
                System.Console.WriteLine($"Tablecode: {reservering.TableCode}");
                System.Console.WriteLine("-------------------------------------");

            }
        }
    }

    public static void Cancel()
    {
        string email = CurrentUserEmail;
        
        Reserveringen.AnnuleerReserveringforAcc(email);
        
        
    }

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

    public static void Changephonenum(Account loggedInAccount)
    {
        string newphonenum;
        do
        {
            System.Console.WriteLine("Enter your Phone Number: ");
            newphonenum = Console.ReadLine();
            if(newphonenum.Length != 10 || !newphonenum.StartsWith("06"))
            {

                System.Console.WriteLine("Phone number must have exactly 10 digits and start with '06'");
            }
        } while (newphonenum.Length != 10 || !newphonenum.StartsWith("06"));
        loggedInAccount.ChangePhoneNumber(newphonenum);
        Account.SaveAccountInformationToJson();
        Console.WriteLine("Phone number changed successfully.");

    }

    public static void ChangePost(Account loggedInAccount)
    {
        string newpostcode;
        do
        {
            System.Console.WriteLine("Enter your new Postcode: ");
            newpostcode = Console.ReadLine();
            if(newpostcode.Length != 6)
            {
                System.Console.WriteLine("Postcode has to have length of 4 numbers and 2 letters. ");
            }
        
        } while (newpostcode.Length != 6);
        loggedInAccount.ChangePostcode(newpostcode);
        Account.SaveAccountInformationToJson();
        Console.WriteLine("Postcode changed successfully.");
    }

    public static void MakeReservationForAcc()
    {
        string email = CurrentUserEmail;
        DateTime datumTijd;
        Console.Write("Enter the date and time of your reservation (yyyy-mm-dd hh:mm): ");
        while (!DateTime.TryParse(Console.ReadLine(), out datumTijd) || datumTijd < DateTime.Now || datumTijd.Hour < 12 || datumTijd.Hour >= 22)
        {
            Console.WriteLine("Invalid date or time. Please enter a valid date and time between 12:00 and 22:00.");
            Console.Write("Enter the date and time of your reservation (yyyy-mm-dd hh:mm): ");
        }

        Console.WriteLine("\nChecking availability for: " + datumTijd.ToString("yyyy-MM-dd HH:mm"));
        Reserveringen.GetAvailableTablesForDay(datumTijd);
        string naam = CurrentUserName;


        Console.Write("How many people: ");
        int aantalPersonen;
        while (!int.TryParse(Console.ReadLine(), out aantalPersonen) || aantalPersonen <= 0)
        {
            Console.WriteLine("Invalid input, try again.");
            Console.Write("How many people: ");
        }

        if (aantalPersonen > 6)
        {
            Console.WriteLine("The amount of people for this reservation exceeds the limit.");
            Console.WriteLine("Please call (31 612316367) the restaurant to complete this reservation.");
            return;
        }

        if (aantalPersonen == 6)
        {
            if (!Reserveringen.IsTableAvailable("6A", datumTijd) && !Reserveringen.IsTableAvailable("6B", datumTijd))
            {
                Console.WriteLine("Unfortunately, we do not have availability for 6 people as all suitable tables are reserved. Please try a different date or reduce the number of people.");
                return;
            }
        }
        else if (aantalPersonen == 4)
        {
            if (!Reserveringen.IsTableAvailable("4A", datumTijd) && !Reserveringen.IsTableAvailable("4B", datumTijd) &&
                !Reserveringen.IsTableAvailable("4C", datumTijd) && !Reserveringen.IsTableAvailable("4D", datumTijd)
                && !Reserveringen.IsTableAvailable("4E", datumTijd))
            {
                Console.WriteLine("Unfortunately, we do not have availability for 4 people as all suitable tables are reserved. Please try a different date or reduce the number of people.");
                return;
            }
        }
        else if (aantalPersonen == 2)
        {
            if (!Reserveringen.IsTableAvailable("2A", datumTijd) && !Reserveringen.IsTableAvailable("2B", datumTijd) &&
                !Reserveringen.IsTableAvailable("2C", datumTijd) && !Reserveringen.IsTableAvailable("2D", datumTijd)&&
                !Reserveringen.IsTableAvailable("2E", datumTijd) && !Reserveringen.IsTableAvailable("2F", datumTijd) &&
                !Reserveringen.IsTableAvailable("2G", datumTijd) && !Reserveringen.IsTableAvailable("2H", datumTijd))
            {
                Console.WriteLine("Unfortunately, we do not have availability for 2 people as all suitable tables are reserved. Please try a different date or reduce the number of people.");
                return;
            }
        }

        Console.WriteLine("\nChoose your table by entering the table code (e.g., 6A, 4C):");
        Console.WriteLine("\nRestaurant Table Map:");
        Console.WriteLine("  [4A]                              [4B]");
        Console.WriteLine("                   [6A]                 ");
        Console.WriteLine("[2A]   [2B]                    [2C]   [2D]");
        Console.WriteLine("                   [4E]                 ");
        Console.WriteLine("[2E]   [2F]                    [2G]   [2H]");
        Console.WriteLine("                   [6B]                 ");
        Console.WriteLine("  [4C]                               [4D]");
        string[] restrictedTableCodes;

        if (aantalPersonen == 1 || aantalPersonen == 2)
        {
            List<string> tempCodes = new();
            foreach (var code in validTableCodes)
            {
                if (code[0] == '2')
                {
                    tempCodes.Add(code);
                }
            }

            restrictedTableCodes = tempCodes.ToArray();
        }



        else if (aantalPersonen == 3 || aantalPersonen == 4)
        {
            List<string> tempCodes = new List<string>();
            foreach (var code in validTableCodes)
            {
                if (code[0] == '4')
                {
                    tempCodes.Add(code);
                }
            }
            restrictedTableCodes = tempCodes.ToArray();
        }
        else if (aantalPersonen == 5 || aantalPersonen == 6)
        {
            List<string> tempCodes = new List<string>();
            foreach (var code in validTableCodes)
            {
                if (code[0] == '6')
                {
                    tempCodes.Add(code);
                }
            }
            restrictedTableCodes = tempCodes.ToArray();
        }
        else
        {
            restrictedTableCodes = validTableCodes;
        }
        Console.WriteLine("\nChoose your table by entering the table code (e.g., 6A, 4C):");
        foreach (var code in restrictedTableCodes)
        {
            if (Reserveringen.IsTableAvailable(code, datumTijd))
            {
                Console.WriteLine($"[{code}]");
            }
        }
        string tableCode;
        while (true)
        {
            tableCode = Console.ReadLine().ToUpper();
            if (restrictedTableCodes.Contains(tableCode, StringComparer.OrdinalIgnoreCase) && Reserveringen.IsTableAvailable(tableCode, datumTijd))
            {
                break;
            }
            Console.WriteLine("Invalid table code or not suitable for the number of people or not available. Please enter a valid table code:");
        }

        if (Reserveringen.BepaalTafelType(aantalPersonen) == 0 || !Reserveringen.ControleerBeschikbaarheid(datumTijd, Reserveringen.BepaalTafelType(aantalPersonen)))
        {
            Console.WriteLine("Unfortunately, there is no availability on the selected date and time for the number of people.");
            
            while (true)
            {
                Console.WriteLine("1) Decrease your party size.");
                Console.WriteLine("2) Quit");
                Console.Write("Make a choice: ");
                string retryChoice = Console.ReadLine();

                switch (retryChoice)
                {
                    case "1":
                        Console.Write("How many people: ");
                        while (!int.TryParse(Console.ReadLine(), out aantalPersonen) || aantalPersonen <= 0 || Reserveringen.BepaalTafelType(aantalPersonen) == 0 || !Reserveringen.ControleerBeschikbaarheid(datumTijd, Reserveringen.BepaalTafelType(aantalPersonen)))
                        {
                            Console.WriteLine("Invalid input, try again.");
                            Console.Write("How many people: ");
                        }
                        break;
                    case "2":

                        return;

                    default:
                        Console.WriteLine("Invalid choice, Choose a valid option");
                        break;
                }

                if (retryChoice == "1" || retryChoice == "2")
                    break;
            }
        }

        string notitieZelf = "";
        Console.WriteLine("Do you have a diet preference or a note for us? Y/N  ");
        string notitieAntw = Console.ReadLine().ToUpper();
        while (true)
        {
            if (notitieAntw == "Y")
            {
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Write your note or diet preference:");
                notitieZelf = Console.ReadLine()!;
                Console.WriteLine("Thanks for pointing it out, this is your note:");
                Console.WriteLine("---------------------------------");
                Console.WriteLine($"{notitieZelf}");
                Console.WriteLine("---------------------------------");
                break;
            }
            else if (notitieAntw == "N")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Fill in Y or N.");
                notitieAntw = Console.ReadLine().ToUpper();
            }
        }
        Reserveringen.VoegReserveringToeEmail(email,naam, aantalPersonen, datumTijd, tableCode, notitieZelf);

    }


    public static void MakeNewAccount()
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
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        while(string.IsNullOrWhiteSpace(eadres) || !Regex.IsMatch(eadres, emailPattern))
        {
            Console.WriteLine("Please enter a valid email address.");
            eadres = Console.ReadLine();
        }
        string password;
        do
        {
            Console.WriteLine("Enter your password: ");
            password = Console.ReadLine();
            if(password.Length <8)
            {
                System.Console.WriteLine($"Password cannot be less than 8 characters.");
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

    public static bool Login()
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
          
                Account loggedInAccount = Account.GetAccount(emailadress);

             
                DisplayAfterLogIn(loggedInAccount);
                return true;
            }
            else
            {
                System.Console.WriteLine("The credentials are not correct. Try again.");
            }
        }

    }

    public static void UpdateAccount(Account account)
    {
        loggedInAccount = account;

        while(true)
        {
            System.Console.WriteLine("1) Change Password");
            System.Console.WriteLine("2) Change Postcode");
            System.Console.WriteLine("3) Change Phone Number");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Account.ChangePassw(loggedInAccount);
                    return;
                case "2":
                    Account.ChangePost(loggedInAccount);
                    return;
                case "3":
                    Account.Changephonenum(loggedInAccount);
                    return;
                default:
                    System.Console.WriteLine("Invalid choice");
                    break;
                    
            }
        }
    }

     public static void DisplayAfterLogIn(Account account)
    {
        loggedInAccount = account;

        
        while(true)
        {
            Console.WriteLine("1) View my reservations");
            Console.WriteLine("2) Make a new reservation");
            Console.WriteLine("3) Cancel a reservation");
            Console.WriteLine("4) Adjust a reservation");
            Console.WriteLine("5) Update Account details");
            Console.WriteLine("6) Menu");
            Console.WriteLine("7) Table details");
            Console.WriteLine("8) View my points");
            Console.WriteLine("9) Logout");
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "1":
                    Account.View();
                    break;
                case "2":
                    Account.MakeReservationForAcc();
                    // Account.GiveDiscount(account);
                    break;
                case "3":
                    Account.Cancel();
                    break;
                case "4":
                    loggedInAccount.Adjust();
                    break;
                case "5":
                   Account.UpdateAccount(loggedInAccount);
                    break;
                case "6":
                    Menu.MenuChoice();
                    break;
                case "7":
                    TableDetails.Details();
                    break;
                case "8": 
                    Console.WriteLine($"Points: {loggedInAccount.Points}");
                    break;
                case "9":
                    Console.WriteLine("You have succesfully logged out.");
                    return;
                default:
                    System.Console.WriteLine("Invalid choice");
                    break;
            }

        }
    }


    public static bool DeleteAccount()
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
                System.Console.WriteLine("The credentials are not correct. Try again.");
            }

        }
    }

    public void Adjust()
    {
         string email = Account.CurrentUserEmail;
        var reserveringen = Reserveringen.GetReservationByEmail(email);
        if (reserveringen != null)
        {
            foreach(var reservering in reserveringen)
            {
                System.Console.WriteLine("Your current reservation details:");
                System.Console.WriteLine("-------------------------------------");
                System.Console.WriteLine($"Email: {reservering.Email}");
                System.Console.WriteLine($"Name: {reservering.GastNaam}");
                System.Console.WriteLine($"Number of People: {reservering.AantalPersonen}");
                System.Console.WriteLine($"Date and Time: {reservering.DatumTijd.ToString("yyyy-MM-dd HH:mm")}");
                System.Console.WriteLine($"Notes: {reservering.Notitie}");
                System.Console.WriteLine($"Tablecode: {reservering.TableCode}");
                System.Console.WriteLine("-------------------------------------");

            


            }

            Console.WriteLine("Which reservation would you like to adjust?");
            Console.Write("Fill in the date and time of the reservation you want to adjust (yyyy-mm-dd hh:mm): ");
            DateTime userdatum;
            while (!DateTime.TryParse(Console.ReadLine(), out userdatum) || userdatum < DateTime.Now)
            {
                Console.WriteLine("Invalid date. Please enter a future date and time in the format yyyy-mm-dd hh:mm:");
            }

            var reserveringToAdjust = reserveringen.FirstOrDefault(r => r.DatumTijd == userdatum);
            if (reserveringToAdjust == null)
            {
                Console.WriteLine("No reservation found with the specified date and time.");
                return;
            }

            Console.Write("Enter the new date and time (yyyy-mm-dd hh:mm): ");
            DateTime newDatumTijd;
            while (!DateTime.TryParse(Console.ReadLine(), out newDatumTijd) || newDatumTijd < DateTime.Now)
            {
                Console.WriteLine("Invalid date. Please enter a future date and time in the format yyyy-mm-dd hh:mm:");
            }

            reserveringToAdjust.DatumTijd = newDatumTijd;
            Reserveringen.SaveReservationsToAccount();
        

            Console.WriteLine("Reservation updated successfully.");
        }
        else
        {
            Console.WriteLine("No reservations found for the current user.");
        }
        
        
    }

    public static void AddPointsToAccount(TafelReserveringForAcc tafelreservering)
    {
        // Account loggedinaccount = GetAccount(account.Emailadress);
        int PointsToAdd = TafelReserveringForAcc.CalculatePoints(tafelreservering);
        loggedInAccount.Points += PointsToAdd;
        Account.SaveAccountInformationToJson();





    }

    // public static void GiveDiscount(Account loggedinaccount)
    // {
    //     if (loggedinaccount.Points == 100)
    //     {
    //         Console.WriteLine("You have received a discount voucher of 10 euro's."); 
    //     }
    // }

    //moet nog verder verwerkt worden

}