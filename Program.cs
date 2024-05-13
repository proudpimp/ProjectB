static class Program
{
    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nWelcome at Jake's restaurant");
            Console.WriteLine("1) Make a reservation");
            Console.WriteLine("2) Cancel reservation");
            Console.WriteLine("3) Adjust reservation");
            Console.WriteLine("4) Contact info");
            Console.WriteLine("5) Menu");
            Console.WriteLine("6) Table details");
            Console.WriteLine("7) Quit");

            Console.Write("Make a choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    MakeReservation.Make();
                    break;

                case "2":
                    CancelReservation.Cancel();
                    break;

                case "3":
                    AdjustReservation.Adjust();
                    break;

                case "4":
                    RestaurantInfo restaurant = new RestaurantInfo("Wijnhaven 107","3011 WN Rotterdam","+ 31 612316367","12:00-22:00, Mon-Sat");
                    System.Console.WriteLine(restaurant.Info());
                    break;

                case "5":
                    Menu.MenuChoice();
                    break;

                case "6":
                    TableDetails.Details();
                    break;
                    
                case "7":
                    Console.WriteLine("Quit");
                    return;
                default:
                    Console.WriteLine("Invalid choice, Choose a valid option");
                    break;
            }

        }
    }

}
