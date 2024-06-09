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

            bool result = Reserveringen.AnnuleerReservering(guestName,safetynum);
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

            bool result = Reserveringen.AnnuleerReservering(fakeNaam,safetynum);
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

            bool result = Account.VoegAccountToe(name,email,password,birthday,postcode,phoneNumber,verificationNum);
            Assert.IsTrue(result);
        }
        // [TestMethod]
        // public void MakeAccount_False()
        // {
        //     string name = "John Doe";
        //     string email = "jdoe@gmail.com";
        //     string password = "John";
        //     DateTime birthday = DateTime.Now.AddYears(-5);
        //     string postcode = "1212ZB";
        //     string phoneNumber = "1612345678";
        //     int verificationNum = new Random().Next(1000,10000);
        //     bool result = Account.VoegAccountToe(name,email,password,birthday,postcode,phoneNumber,verificationNum);
        //     Assert.IsFalse(result);
        // }
        [TestMethod]
        public void LoginAccount_True()
        {
            string name = "John Doe";
            string email = "jdoe@gmail.com";
            string password = "JohnDoe1";
            DateTime birthday = DateTime.Now.AddYears(-5);
            string postcode = "1212ZB";
            string phoneNumber = "1612345678";
            int verificationNum = new Random().Next(1000,10000);
            Account.VoegAccountToe(name,email,password,birthday,postcode,phoneNumber,verificationNum);
            bool result = Account.AccountExists(email,password);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void LoginAccount_False()
        {
            string name = "John Doe";
            string email = "jdoe@gmail.com";
            string fakeEmail = "testjoe@gmail.com";
            string password = "JohnDoe1";
            DateTime birthday = DateTime.Now.AddYears(-5);
            string postcode = "1212ZB";
            string phoneNumber = "1612345678";
            int verificationNum = new Random().Next(1000,10000);
            Account.VoegAccountToe(name,email,password,birthday,postcode,phoneNumber,verificationNum);
            bool result = Account.AccountExists(fakeEmail,password);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void DeleteAccount_True()
        {
            string name = "John Doe";
            string email = "jdoe@gmail.com";
            string password = "John";
            DateTime birthday = DateTime.Now.AddYears(-5);
            string postcode = "1212ZB";
            string phoneNumber = "1612345678";
            int verificationNum = new Random().Next(1000,10000);
            Account.VoegAccountToe(name,email,password,birthday,postcode,phoneNumber,verificationNum);
            bool result = Account.AccountExists(email,password,verificationNum);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void DeleteAccount_False()
        {
            string name = "John Doe";
            string email = "jdoe@gmail.com";
            string fakeEmail = "testjoe@gmail.com";
            string password = "JohnDoe1";
            DateTime birthday = DateTime.Now.AddYears(-5);
            string postcode = "1212ZB";
            string phoneNumber = "1612345678";
            int verificationNum = new Random().Next(1000,10000);
            Account.VoegAccountToe(name,email,password,birthday,postcode,phoneNumber,verificationNum);
            bool result = Account.AccountExists(fakeEmail,password,verificationNum);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void MakeReservationForAcc_True()
        {
            string name = "John Doe";
            string email = "jdoe@gmail.com";
            string password = "JohnDoe1";
            DateTime birthday = DateTime.Now.AddYears(-5);
            string postcode = "1212ZB";
            string phoneNumber = "0612345678";
            int verificationNum = new Random().Next(1000,10000);
            Account.VoegAccountToe(name,email,password,birthday,postcode,phoneNumber,verificationNum);
            Account.AccountExists(email,password);
            int aantalPersonen = 4;
            DateTime datumTijd = DateTime.Now.AddMinutes(1);
            string tableCode = "4A";
            string notitie = "No saus";
            bool result = Reserveringen.VoegReserveringToeEmail(email,name,aantalPersonen,datumTijd,tableCode,notitie);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void MakeReservationForAcc_False()
        {
            string name = "John Doe";
            string email = "jdoe@gmail.com";
            string password = "JohnDoe1";
            DateTime birthday = DateTime.Now.AddYears(-5);
            string postcode = "1212ZB";
            string phoneNumber = "0612345678";
            int verificationNum = new Random().Next(1000,10000);
            Account.VoegAccountToe(name,email,password,birthday,postcode,phoneNumber,verificationNum);
            Account.AccountExists(email,password);
            int aantalPersonen = 4;
            DateTime datumTijd = DateTime.Now.AddYears(-100);
            string tableCode = "4A";
            string notitie = "No saus";
            bool result = Reserveringen.VoegReserveringToeEmail(email,name,aantalPersonen,datumTijd,tableCode,notitie);
            Assert.IsFalse(result);
        }
    }
}
