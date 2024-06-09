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
            DateTime reservationDateTime = DateTime.Now.AddDays(1);
            string note = "Vegetarian";
            string tcode = "4A";
            int safetynum = new Random().Next(1000,10000);
            bool result = Reserveringen.VoegReserveringToe(guestName, numberOfPeople, reservationDateTime,tcode, note,safetynum);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void VoegReserveringToe_False()
        {
            string guestName = "Jane Doe";
            int numberOfPeople = 2;
            DateTime reservationDateTime = DateTime.Now;
            string note = "Gluten Free";
            string tcode = "2B";
            int safetynum = new Random().Next(1000,10000);

            
            bool result = Reserveringen.VoegReserveringToe(guestName, numberOfPeople, reservationDateTime,tcode, note,safetynum);

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void CancelReservation_True()
        {
            string guestName = "John Doe";
            int numberOfPeople = 4;
            DateTime reservationDateTime = DateTime.Now.AddMinutes(1);
            string note = "Vegan";
            string tcode = "4B";
            int safetynum = new Random().Next(1000,10000);
            Reserveringen.VoegReserveringToe(guestName, numberOfPeople, reservationDateTime,tcode, note,safetynum);

            var result = Reserveringen.AnnuleerReservering(guestName,safetynum);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void CancelReservation_False()
        {
            string guestName = "John Doe";
            int numberOfPeople = 4;
            DateTime reservationDateTime = DateTime.Now.AddMinutes(1);
            string note = "Vegan";
            string tcode = "4B";
            int safetynum = new Random().Next(1000,10000);
            Reserveringen.VoegReserveringToe(guestName, numberOfPeople, reservationDateTime,tcode, note,safetynum);
            string fakeNaam = "Rotterdam";

            var result = Reserveringen.AnnuleerReservering(fakeNaam,safetynum);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void MakeAccount_True()
        {
            string name = "John Doe";
            string email = "jdoe@gmail.com";
            string password = "JohnDoe1";
            DateTime birthday = DateTime.Now.AddYears(-5);
            string postcode = "1212ZB";
            string phoneNumber = "0612345678";
            int verificationNum = new Random().Next(1000,10000);

            var result = Account.VoegAccountToe(name,email,password,birthday,postcode,phoneNumber,verificationNum);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void MakeAccount_False()
        {
            string name = "";
            string email = "jdoe@gmail.com";
            string password = "JohnDoe1";
            DateTime birthday = DateTime.Now.AddYears(-5);
            string postcode = "1212ZB";
            string phoneNumber = "1612345678";
            int verificationNum = new Random().Next(1000,10000);

            var result = Account.VoegAccountToe(name,email,password,birthday,postcode,phoneNumber,verificationNum);
            Assert.IsFalse(result);
        }
    }
}
