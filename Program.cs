class Program
{
    public static void Main()
    {

        while (true)
        {
            Console.WriteLine("\nWelkom bij Jake's restaurant");
            Console.WriteLine("1) Reserveren");
            Console.WriteLine("2) Reservering Annuleren");
            Console.WriteLine("3) Reservering Wijzigen");
            Console.WriteLine("4) Contactinfo");
            Console.WriteLine("5) Quit");
            Console.Write("Maak een keuze: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Reservering Maken");
                    var manager = new Reserveringen();

                    Console.Write("Voer uw naam in: ");
                    string naam = Console.ReadLine();

                    Console.Write("Voer het aantal personen in: ");
                    int aantalPersonen;
                    while (!int.TryParse(Console.ReadLine(), out aantalPersonen) || aantalPersonen <= 0)
                    {
                        Console.WriteLine("Ongeldige invoer, probeer het opnieuw.");
                        Console.Write("Voer het aantal personen in: ");
                    }

                    while(true)
                    {
                    Console.WriteLine("Wilt U een extra notitie maken bij uw reservering? Y/N  ");
                    string notitieAntw = Console.ReadLine().ToUpper()!; 
                        if (notitieAntw == "Y")
                        {  
                           Console.WriteLine("---------------------------------");
                           Console.WriteLine("Maak hier uw notitie:"); 
                           Console.WriteLine("---------------------------------");
                           string notitieZelf = Console.ReadLine()!;
                           Console.WriteLine("Bedankt voor het aangeven, dit is uw notitie:"); 
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
                            Console.WriteLine("Ongeldige Invoer. Voer Y of N in.");
                        }
                        
                      
                    }


                    Console.Write("Voer de datum en tijd van de reservering in (yyyy-mm-dd hh:mm): ");
                    DateTime datumTijd;
                    while (!DateTime.TryParse(Console.ReadLine(), out datumTijd))
                    {
                        Console.WriteLine("Ongeldige datum, probeer het opnieuw.");
                        Console.Write("Voer de datum en tijd van de reservering in (yyyy-mm-dd hh:mm): ");
                    }

                    manager.VoegReserveringToe(naam, aantalPersonen, datumTijd);
                    break;

                case "2":
                    Console.WriteLine("Reservering Annuleren");

                    Console.Write("Voer de naam in waaronder de reservering is gemaakt: ");
                    string gastNaam = Console.ReadLine();

                    string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    string fileNamePattern = $"Reserveringen_{gastNaam}_bevestiging.txt";

                    string fullPath = Path.Combine(folderPath, fileNamePattern);

                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                        Console.WriteLine("Uw reservering is succesvol geannuleerd.");
                    }
                    else
                    {
                        Console.WriteLine("Er is geen reservering gevonden onder de opgegeven naam.");
                    }
                    break;
                
                case "3":
                    Console.WriteLine("Reservering Wijzigen");
                    Console.Write("Voer uw naam in voor de reservering die u wilt wijzigen: ");
                    string zoekNaam = Console.ReadLine();

                    string folderPath1 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string fileName = $"Reserveringen_{zoekNaam}_bevestiging.txt";
                    string fullPath1 = Path.Combine(folderPath1, fileName);

                    if (File.Exists(fullPath1))
                    {
                        string currentDetails = File.ReadAllText(fullPath1);
                        Console.WriteLine("Uw huidige reservering details:\n" + currentDetails);

                        Console.Write("Voer de nieuwe datum en tijd van de reservering in (yyyy-mm-dd hh:mm): ");
                        string nieuweDatumTijd = Console.ReadLine();

                        string updatedDetails = currentDetails.Replace(currentDetails.Substring(currentDetails.IndexOf("Datum en tijd: "), currentDetails.IndexOf("\nTafeltype: ") - currentDetails.IndexOf("Datum en tijd: ")), $"Datum en tijd: {nieuweDatumTijd}");

                        File.WriteAllText(fullPath1, updatedDetails);
                        Console.WriteLine("Uw reservering is bijgewerkt.");
                    }
                    else
                    {
                        Console.WriteLine("Geen reservering gevonden onder deze naam.");
                    }
                    break;

                case "4":
                    RestaurantInfo restaurant = new RestaurantInfo("Wijnhaven 107","3011 WN Rotterdam","+ 31 612316367","09:00/20:00, Mon/Sat");
                    System.Console.WriteLine(restaurant.Info());
                    break;

                case "5":
                    Console.WriteLine("Quit");
                    return; 

                default:
                    Console.WriteLine("Ongeldige keuze, kies de goede optie");
                    break;
            }
        }
    }
}
