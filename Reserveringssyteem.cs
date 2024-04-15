using Newtonsoft.Json;

public class Reserveringen
{
    private List<TafelReservering> reserveringen = new List<TafelReservering>();
    private static readonly string JsonFilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "GitHub", "ProjectB", "Reservations.json");

    public const int Max6Tafels = 2;
    public const int Max2Tafels = 8;
    public const int Max4Tafels = 5;

    public Reserveringen()
    {
        LoadReservationsFromJson();
    }

public bool VoegReserveringToe(string gastNaam, int aantalPersonen, DateTime datumTijd,string notitie)
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
    
    var nieuweReservering = new TafelReservering(gastNaam, aantalPersonen, datumTijd, tafelType,notitie);
    reserveringen.Add(nieuweReservering);
    Console.WriteLine("Reservation successfully added for " + gastNaam);

    string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    string fileName = $"Reservation_{gastNaam}_Confirmation.txt";
    string fullPath = Path.Combine(folderPath, fileName);

    string bevestigingTekst = "Thank you for your reservation at Jake's Restaurant. We look forward to welcoming you! \nBelow are the details of your reservation:\n\n" +
                            $"Reservationc for {gastNaam}\n" +
                            $"Amount of people: {aantalPersonen}\n" +
                            $"Date and time: {datumTijd.ToString("yyyy-MM-dd HH:mm")}\n" +
                            $"Tabeltype: {tafelType}\n" +
                            $"Notitie: {notitie}";


    try
    {
        File.WriteAllText(fullPath, bevestigingTekst);
        Console.WriteLine("The reservation confirmation has been successfully saved for " + gastNaam + " in " + fullPath + ".");
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred while saving the reservation confirmation: " + ex.Message);
    }
    SaveReservationsToJson();
    return true;
}

    private int BepaalTafelType(int aantalPersonen)
    {
        if (aantalPersonen <= 2) return 2;
        if (aantalPersonen <= 4) return 4;
        if (aantalPersonen <= 6) return 6;
        return 0;
    }

    public bool ControleerBeschikbaarheid(DateTime datumTijd, int tafelType)
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
    public void AnnuleerReservering(string gastNaam)
    {
        var reservering = GetReservationByName(gastNaam);
        if (reservering != null)
        {
            reserveringen.Remove(reservering);
            SaveReservationsToJson();
            Console.WriteLine("The reservation has been successfully canceled.");
        }
        else
        {
            Console.WriteLine("No reservation found with the given name.");
        }
    }
    public void SaveReservationsToJson()
    {
        string json = JsonConvert.SerializeObject(reserveringen, Formatting.Indented);
        File.WriteAllText(JsonFilePath, json);
    }

    private void LoadReservationsFromJson()
    {
        if (File.Exists(JsonFilePath))
        {
            string json = File.ReadAllText(JsonFilePath);
            reserveringen = JsonConvert.DeserializeObject<List<TafelReservering>>(json) ?? new List<TafelReservering>();
        }
    }

    public TafelReservering? GetReservationByName(string guestName)
    {
        return reserveringen.FirstOrDefault(r => r.GastNaam.Equals(guestName, StringComparison.OrdinalIgnoreCase));
    }


public void GetAvailableTablesForDay(DateTime date)
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