using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ProjectBTest
{
    [TestClass]
    public class ProjectBTest
    {
        [TestMethod]
        public void VoegReserveringToe_True()
        {
            string guestName = "John Doe";
            int numberOfPeople = 4;
            DateTime reservationDateTime = DateTime.Now.AddHours(15);
            string note = "Vegetarian";
            string tcode = "4A";

            bool result = Reserveringen.VoegReserveringToe(guestName, numberOfPeople, reservationDateTime,tcode, note);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void VoegReserveringToe_False()
        {
            string guestName = "Jane Doe";
            int numberOfPeople = 2;
            DateTime reservationDateTime = DateTime.Now.AddHours(-5);
            string note = "Gluten Free";
            string tcode = "2B";
            
            bool result = Reserveringen.VoegReserveringToe(guestName, numberOfPeople, reservationDateTime,tcode, note);

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void CancelReservation_True()
        {
            string guestName = "John Doe";
            int numberOfPeople = 4;
            DateTime reservationDateTime = DateTime.Now.AddHours(-5);
            string note = "Vegan";
            string tcode = "4B";
            Reserveringen.VoegReserveringToe(guestName, numberOfPeople, reservationDateTime,tcode, note);

            var result = Reserveringen.AnnuleerReservering(guestName);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void CancelReservation_False()
        {
            string guestName = "John Doe";
            int numberOfPeople = 4;
            DateTime reservationDateTime = DateTime.Now.AddHours(-5);
            string note = "Vegan";
            string tcode = "4B";
            Reserveringen.VoegReserveringToe(guestName, numberOfPeople, reservationDateTime,tcode, note);
            string fakeNaam = "Rotterdam";

            var result = Reserveringen.AnnuleerReservering(fakeNaam);
            Assert.IsFalse(result);
        }
    }
}
