using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ProjectBTest
{
    [TestClass]
    public class ProjectBTest
    {
        private Reserveringen _reserveringen;

        [TestInitialize]
        public void TestInitialize()
        {
            _reserveringen = new Reserveringen();
        }

        [TestMethod]
        public void VoegReserveringToeTest()
        {
            string guestName = "John Doe";
            int numberOfPeople = 4;
            DateTime reservationDateTime = DateTime.Now.AddDays(1);
            string note = "Vegetarian";

            bool result = _reserveringen.VoegReserveringToe(guestName, numberOfPeople, reservationDateTime, note);

            Assert.IsTrue(result);
        }
    }

        
}
