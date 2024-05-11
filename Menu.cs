public static class Menu
{
    public static void MenuChoice()
    {
        while(true)
        {
            System.Console.WriteLine("Menu:");
            System.Console.WriteLine("Discover our menu:");
            System.Console.WriteLine("1) -Starters");
            System.Console.WriteLine("2) -Breakfast");
            System.Console.WriteLine("3) -Burgeres");
            System.Console.WriteLine("4) -Wraps");
            System.Console.WriteLine("5) -Sandwiches");
            System.Console.WriteLine("6) -Bites");
            System.Console.WriteLine("Q) -Back to main menu");
            System.Console.WriteLine("Make a choice:");
            string MenuChoice = Console.ReadLine().ToUpper();

            if(MenuChoice == "Q")
            {
                break;
            }
            switch(MenuChoice)
            {
                case "1":
                    System.Console.WriteLine("Starters:");
                    System.Console.WriteLine("1) - Basket of fries ($4)");
                    System.Console.WriteLine("2) - Quesadilla ($5)");
                    Console.WriteLine("3) - Chicken Tender Basket x3 ($6)");
                    Console.WriteLine("4) - Chicken Wings x4 ($7)");
                    Console.WriteLine("Q) - Return to menu");
                    string starterChoice = Console.ReadLine().ToUpper();
                    if(starterChoice == "Q")
                    {
                        break;
                    }
                    break;
                
                case "2":
                    Console.WriteLine("Breakfast:");
                    Console.WriteLine("5) -Breakfast Sandwich ($10)");
                    Console.WriteLine("6) -Omelette ($7)");
                    Console.WriteLine("7) -Boiled Eggs x2 ($5)");
                    Console.WriteLine("Q) - Return to menu");
                    string BreakfastChoice = Console.ReadLine();
                    if(BreakfastChoice == "Q")
                    {
                        break;
                    }
                    break;
                case "3":
                    System.Console.WriteLine("Burgeres:");
                    Console.WriteLine("8) -Cheeseburger ($8)");
                    Console.WriteLine("9) -Veggie Burger ($7)");
                    Console.WriteLine("10) -Chicken Burger ($9)");
                    Console.WriteLine("Q) - Return to menu");
                    string BurgerChoice = Console.ReadLine();
                    if(BurgerChoice == "Q")
                    {
                        break;
                    }
                    break;
                case "4":
                    System.Console.WriteLine("Wraps:");
                    Console.WriteLine("11) -Chicken Wrap ($8)");
                    Console.WriteLine("12) -Vegetable Wrap ($7)");
                    Console.WriteLine("13) -Tuna Wrap ($9)");
                    Console.WriteLine("Q) - Return to menu");
                    string WrapChoice = Console.ReadLine();
                    if(WrapChoice == "Q")
                    {
                        break;
                    }
                    break;
                case "5":
                    System.Console.WriteLine("Sandwiches:");
                    Console.WriteLine("14) -BLT Sandwich ($8)");
                    Console.WriteLine("15) -Club Sandwich ($9)");
                    Console.WriteLine("16) -Grilled Cheese Sandwich ($7)");
                    Console.WriteLine("Q) - Return to menu");
                    string SandwichChoice = Console.ReadLine();
                    if(SandwichChoice == "Q")
                    {
                        break;
                    }
                    break;
                case "6":
                    System.Console.WriteLine("Bites:");
                    Console.WriteLine("17) -Slice of Pizza ($3)");
                    Console.WriteLine("18) -Hot Dog ($4)");
                    Console.WriteLine("19) -Nachos ($6)");
                    Console.WriteLine("Q) - Return to menu");
                    string BitesChoice = Console.ReadLine();
                    if(BitesChoice == "Q")
                    {
                        break;
                    }
                    break;
                default:
                    System.Console.WriteLine("Invalid choice, Choose a valid option");
                    break;
            }
        }
    }
}