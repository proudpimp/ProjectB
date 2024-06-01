public static class MenuAfter
{
    public static void Log()
    {
        while(true)
        {
            Console.WriteLine("1) View my reservations");
            Console.WriteLine("2) Make a new reservation");
            Console.WriteLine("3) Cancel a reservation");
            Console.WriteLine("4) Adjust a reservation");
            Console.WriteLine("5) Update contact information");
            Console.WriteLine("6) View your points");
            Console.WriteLine("7) View and personal details");
            Console.WriteLine("8) Change password");
            Console.WriteLine("9) Menu");
            Console.WriteLine("10) Table details");
            Console.WriteLine("11) Logout");
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "1":
                    AccountReservations.View();
                    break;
                case "2":
                    MakeReservationFor.Account();
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
                case "6":
                    break;
                case "7":
                    break;
                case "8":
                    break;
                case "9":
                    break;
                case "10":
                    break;
                case "11":
                    break;
                default:
                    System.Console.WriteLine("Invalid choice");
                    break;
            }

        }
    }
}