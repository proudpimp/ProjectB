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
            Console.WriteLine("6) View special offers and discounts");
            Console.WriteLine("7) View and edit personal details");
            Console.WriteLine("8) Change password");
            Console.WriteLine("9) Menu");
            Console.WriteLine("10) Table details");
            Console.WriteLine("11) Logout");
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "1":
                    MyReservations.View();
                    break;
                case "2":
                    break;
            }

        }
    }
}