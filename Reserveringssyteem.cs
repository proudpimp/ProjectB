using System.Buffers;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

public static class Reserveringen
{
    private static List<TafelReserveringForGuest> reserveringen = new List<TafelReserveringForGuest>();
    private static List<TafelReserveringForAcc> reserveringenForAcc = new List<TafelReserveringForAcc>();

    private static string JsonFilePath
    {
        get
        {
           
                return "./Reservations.json";
           
        }
    }

    private static string JsonFilePathForAcc
    {
        get
        {
           
                return "./ReservationsForAcc.json";
          
        }
    }

    public const int Max6Tafels = 2;
    public const int Max2Tafels = 8;
    public const int Max4Tafels = 5;

    static Reserveringen()
    {
        reserveringen = LoadFromJson<List<TafelReserveringForGuest>>(JsonFilePath) ?? new List<TafelReserveringForGuest>();
        reserveringenForAcc = LoadFromJson<List<TafelReserveringForAcc>>(JsonFilePathForAcc) ?? new List<TafelReserveringForAcc>();
    }

    public static bool IsTableAvailable(string tableCode, DateTime datumTijd)
    {
        bool noReservations = true;

        foreach (var reservation in reserveringen)
        {
            if (reservation.TableCode == tableCode && reservation.DatumTijd.Date == datumTijd.Date)
            {
                noReservations = false;
                break;
            }
        }

        return noReservations;
    }

    public static bool VoegReserveringToe(string gastNaam, int aantalPersonen, DateTime datumTijd, string tableCode, string notitie, int safetyNumber)
    {
        if (datumTijd < DateTime.Now)
        {
            Console.WriteLine("You cannot make a reservation for a past date and time.");
            return false;
        }

        int reservationHour = datumTijd.Hour;

        if (reservationHour < 12 || reservationHour > 21)
        {
            Console.WriteLine("Reservations can only be made between 12:00 and 21:00.");
            return false;
        }

        int tafelType = BepaalTafelType(aantalPersonen);
        if (tafelType == 0 || !ControleerBeschikbaarheid(datumTijd, tafelType))
        {
            Console.WriteLine("Unfortunately, there is no availability on the selected date and time for the number of people.");
            return false;
        }

        var nieuweReservering = new TafelReserveringForGuest(gastNaam, aantalPersonen, datumTijd, tafelType, tableCode, notitie, safetyNumber);
        reserveringen.Add(nieuweReservering);
        Console.WriteLine("Reservation successfully added for " + gastNaam);

        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string fileName = $"Reservation_{gastNaam}_Confirmation.txt";
        string fullPath = Path.Combine(folderPath, fileName);

        string bevestigingTekst = "Thank you for your reservation at Jake's Restaurant. We look forward to welcoming you! \nBelow are the details of your reservation:\n\n" +
                                $"Reservation for {gastNaam}\n" +
                                $"Amount of people: {aantalPersonen}\n" +
                                $"Date and time: {datumTijd.ToString("yyyy-MM-dd HH:mm")}\n" +
                                $"TableCode: {tableCode}\n" +
                                $"SafetyNumber: {safetyNumber}\n" +
                                $"Note: {notitie}";

        try
        {
            File.WriteAllText(fullPath, bevestigingTekst);
            Console.WriteLine("The reservation confirmation has been successfully saved for " + gastNaam + " in " + fullPath + ".");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while saving the reservation confirmation: " + ex.Message);
        }
        SaveToJson(JsonFilePath, reserveringen);
        return true;
    }

    public static bool VoegReserveringToeEmail(string email, string gastNaam, int aantalPersonen, DateTime datumTijd, string tableCode, string notitie)
    {
        if (datumTijd < DateTime.Now)
        {
            Console.WriteLine("You cannot make a reservation for a past date and time.");
            return false;
        }

        int reservationHour = datumTijd.Hour;

        if (reservationHour < 12 || reservationHour > 21)
        {
            Console.WriteLine("Reservations can only be made between 12:00 and 21:00.");
            return false;
        }

        int tafelType = BepaalTafelType(aantalPersonen);
        if (tafelType == 0 || !ControleerBeschikbaarheid(datumTijd, tafelType))
        {
            Console.WriteLine("Unfortunately, there is no availability on the selected date and time for the number of people.");
            return false;
        }

        var nieuweReservering = new TafelReserveringForAcc(email, gastNaam, aantalPersonen, datumTijd, tafelType, tableCode, notitie);
        reserveringenForAcc.Add(nieuweReservering);
        Console.WriteLine("Reservation successfully added for " + gastNaam);

        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string fileName = $"Reservation_{gastNaam}_Confirmation.txt";
        string fullPath = Path.Combine(folderPath, fileName);

        string bevestigingTekst = "Thank you for your reservation at Jake's Restaurant. We look forward to welcoming you! \nBelow are the details of your reservation:\n\n" +
                                $"Reservation for {gastNaam}\n" +
                                $"Amount of people: {aantalPersonen}\n" +
                                $"Date and time: {datumTijd.ToString("yyyy-MM-dd HH:mm")}\n" +
                                $"TableCode: {tableCode}\n" +
                                $"Note: {notitie}";

        try
        {
            File.WriteAllText(fullPath, bevestigingTekst);
            Console.WriteLine("The reservation confirmation has been successfully saved for " + gastNaam + " in " + fullPath + ".");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while saving the reservation confirmation: " + ex.Message);
        }
        Account.AddPointsToAccount(nieuweReservering);
        SaveToJson(JsonFilePathForAcc, reserveringenForAcc);
        return true;
    }

    public static int BepaalTafelType(int aantalPersonen)
    {
        if (aantalPersonen <= 2) return 2;
        if (aantalPersonen <= 4) return 4;
        if (aantalPersonen <= 6) return 6;
        return 0;
    }

    public static bool ControleerBeschikbaarheid(DateTime datumTijd, int tafelType)
    {
        int maxTafels;
        switch (tafelType)
        {
            case 2: maxTafels = Max2Tafels; break;
            case 4: maxTafels = Max4Tafels; break;
            case 6: maxTafels = Max6Tafels; break;
            default: return false;
        }

        return reserveringen.Count(r => r.DatumTijd.Date == datumTijd.Date && r.TafelType == tafelType) < maxTafels;
    }

    public static bool IsMagicNumberEqual(int safetyNumber)
    {
        foreach (var number in reserveringen)
        {
            if (number.SafetyNumber == safetyNumber)
            {
                return true;
            }
        }
        return false;
    }

    public static bool AnnuleerReservering(string gastNaam, int SafetyNumber)
    {
        var reservering = GetReservationByName(gastNaam, SafetyNumber);
        if (reservering != null)
        {
            reserveringen.Remove(reservering);
            SaveToJson(JsonFilePath, reserveringen);
            Console.WriteLine("The reservation has been successfully canceled.");
            return true;
        }
        else
        {
            Console.WriteLine("No reservation found with the given name.");
            return false;
        }
    }

    public static bool AnnuleerReserveringforAcc(string email)
    {
        var reservering = GetReservationByEmail(email);
        if (reservering != null)
        {
            Account.View();

            Console.WriteLine("Enter the date and time of the reservation you would like to cancel");

            DateTime datetocancel;

            while (!DateTime.TryParse(Console.ReadLine(), out datetocancel))
            {
                Console.WriteLine("The date you entered is not valid date\nTry again.");
            }
            foreach (var x in reserveringenForAcc)
            {
                if (x.DatumTijd == datetocancel)
                {
                    reserveringenForAcc.Remove(x);
                    SaveToJson(JsonFilePathForAcc, reserveringenForAcc);
                    Console.WriteLine("Your reservation has been cancelled.");
                    return true;
                }
            }
        }
        else
        {
            Console.WriteLine("Your reservation has not been found");
            return false;
        }
        return false;
    }

    public static T LoadFromJson<T>(string filePath)
{
    string json = File.ReadAllText(filePath);
    return JsonConvert.DeserializeObject<T>(json);
}


    public static void SaveToJson<T>(string filePath, T data)
    {
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public static TafelReserveringForGuest? GetReservationByName(string gastNaam, int safetyNumber)
    {
        foreach (var reservering in reserveringen)
        {
            if (reservering.GastNaam == gastNaam && reservering.SafetyNumber == safetyNumber)
            {
                return reservering;
            }
        }
        return null;
    }

    public static List<TafelReserveringForAcc> GetReservationByEmail(string email)
    {
        return reserveringenForAcc.FindAll(reservering => reservering.Email == email);
    }
    public static void SaveReservations()
    {
        SaveToJson(JsonFilePath, reserveringen);
    }
        public static void SaveReservationsToAccount()
    {
        SaveToJson(JsonFilePathForAcc, reserveringenForAcc);
    }

    public static void GetAvailableTablesForDay(DateTime date)
    {
        int availableTablesForTwo = Max2Tafels;
        int availableTablesForFour = Max4Tafels;
        int availableTablesForSix = Max6Tafels;

        foreach (var reservation in reserveringen)
        {
            if (reservation.DatumTijd.Date == date.Date)
            {
                if (reservation.TafelType == 2)
                {
                    availableTablesForTwo--;
                }
                else if (reservation.TafelType == 4)
                {
                    availableTablesForFour--;
                }
                else if (reservation.TafelType == 6)
                {
                    availableTablesForSix--;
                }
            }
        }

        int totalAvailableTables = availableTablesForTwo + availableTablesForFour + availableTablesForSix;

        Console.WriteLine($"Available tables for {date.ToString("yyyy-MM-dd")}:\n" +
                          $"- Two-person tables: {availableTablesForTwo}\n" +
                          $"- Four-person tables: {availableTablesForFour}\n" +
                          $"- Six-person tables: {availableTablesForSix}\n" +
                          $"Total available tables: {totalAvailableTables}");
    }
}
