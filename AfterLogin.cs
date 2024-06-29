// public static class AfterLogin
// {
//     private static Account loggedInAccount;

//     public static void DisplayAfterLogIn(Account account)
//     {
//         loggedInAccount = account;

        
//         while(true)
//         {
//             Console.WriteLine("1) View my reservations");
//             Console.WriteLine("2) Make a new reservation");
//             Console.WriteLine("3) Cancel a reservation");
//             Console.WriteLine("4) Adjust a reservation");
//             Console.WriteLine("5) Update Account details");
//             Console.WriteLine("6) Menu");
//             Console.WriteLine("7) Table details");
//             Console.WriteLine("8) Logout");
//             string choice = Console.ReadLine();
//             switch(choice)
//             {
//                 case "1":
//                     Account.View();
//                     break;
//                 case "2":
//                     Account.MakeReservationForAcc();
//                     break;
//                 case "3":
//                     Account.Cancel();
//                     break;
//                 case "4":
//                     new AdjustReservationforAcc().Adjust();
//                     break;
//                 case "5":
//                    Account.UpdateAccount(loggedInAccount);
//                     break;
//                 case "6":
//                     Menu.MenuChoice();
//                     break;
//                 case "7":
//                     TableDetails.Details();
//                     break;
//                 case "8":
//                     Console.WriteLine("You have succesfully logged out.");
//                     return;
//                 default:
//                     System.Console.WriteLine("Invalid choice");
//                     break;
//             }

//         }
//     }
// }