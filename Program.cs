static class Program
{
    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nWelcome at Jake's restaurant");
            Console.WriteLine("1) Login");
            Console.WriteLine("2) Continue as guest");
            Console.WriteLine("3) Delete Account");
            Console.WriteLine("4) Create an account");
            Console.WriteLine("5) Quit");

            Console.Write("Make a choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                        Login.ToAccount();
                        break;
                case "2":
                        Console.WriteLine("1) Make a reservation");
                        Console.WriteLine("2) Cancel reservation");
                        Console.WriteLine("3) Adjust reservation");
                        Console.WriteLine("4) Contact info");
                        Console.WriteLine("5) Menu");
                        Console.WriteLine("6) Table details");
                        Console.WriteLine("7) Quit");
                        string choice2 = Console.ReadLine();
                        switch (choice2)
                        {
                            case "1":
                                MakeReservation.Make();
                                break;
                            case "2":
                                CancelReservation.Cancel();
                                break;
                            case "3":
                                new AdjustReservation().Adjust();
                                break;
                            case "4":
                                System.Console.WriteLine(new RestaurantInfo("Wijnhaven 107","3011 WN Rotterdam","+ 31 612316367","12:00-22:00, Mon-Sat").Info());
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
                        break;
                case "3":
                    DeleteAccount.ThisAccount();
                    break;
                case "4":
                    MakeAccount.NewAccount();
                    break;
                case "5":
                    System.Console.WriteLine("Quit");
                    return;
                default:
                    Console.WriteLine("Invalid choice, Choose a valid option");
                    break;
            }

        }
    }

}