public class AdjustReservationforAcc : IAdjust
{
    public void Adjust()
    {
         string email = Account.CurrentUserEmail;
        var reserveringen = Reserveringen.GetReservationByEmail(email);
        if (reserveringen != null)
        {
            foreach(var reservering in reserveringen)
            {
                System.Console.WriteLine("Your current reservation details:");
                System.Console.WriteLine("-------------------------------------");
                System.Console.WriteLine($"Email: {reservering.Email}");
                System.Console.WriteLine($"Name: {reservering.GastNaam}");
                System.Console.WriteLine($"Number of People: {reservering.AantalPersonen}");
                System.Console.WriteLine($"Date and Time: {reservering.DatumTijd.ToString("yyyy-MM-dd HH:mm")}");
                System.Console.WriteLine($"Notes: {reservering.Notitie}");
                System.Console.WriteLine($"Tablecode: {reservering.TableCode}");
                System.Console.WriteLine("-------------------------------------");

            


            }

            Console.WriteLine("Which reservation would you like to adjust?");
            Console.Write("Fill in the date and time of the reservation you want to adjust (yyyy-mm-dd hh:mm): ");
            DateTime userdatum;
            while (!DateTime.TryParse(Console.ReadLine(), out userdatum) || userdatum < DateTime.Now)
            {
                Console.WriteLine("Invalid date. Please enter a future date and time in the format yyyy-mm-dd hh:mm:");
            }

            var reserveringToAdjust = reserveringen.FirstOrDefault(r => r.DatumTijd == userdatum);
            if (reserveringToAdjust == null)
            {
                Console.WriteLine("No reservation found with the specified date and time.");
                return;
            }

            Console.Write("Enter the new date and time (yyyy-mm-dd hh:mm): ");
            DateTime newDatumTijd;
            while (!DateTime.TryParse(Console.ReadLine(), out newDatumTijd) || newDatumTijd < DateTime.Now)
            {
                Console.WriteLine("Invalid date. Please enter a future date and time in the format yyyy-mm-dd hh:mm:");
            }

            reserveringToAdjust.DatumTijd = newDatumTijd;
            Reserveringen.SaveReservationsToAccount();
        

            Console.WriteLine("Reservation updated successfully.");
        }
        else
        {
            Console.WriteLine("No reservations found for the current user.");
        }
        

        


        
    }
}

