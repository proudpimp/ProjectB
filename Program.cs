class Program
{
    public static void Main()
    {
        RestaurantInfo restaurant = new RestaurantInfo("Wijnhaven 107","3011WN Rotterdam","0612316367","09:00/20:00, Mon/Sat");
        System.Console.WriteLine(restaurant.Info());

    }
}