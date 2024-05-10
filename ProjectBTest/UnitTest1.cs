// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using System;

// namespace ProjectBTest
// {
//     [TestClass]
//     public class ProjectBTest
//     {
//         [TestMethod]
//         public void VoegReserveringToe_True()
//         {
//             string guestName = "John Doe";
//             int numberOfPeople = 4;
//             DateTime reservationDateTime = DateTime.Now.AddHours(5);
//             string note = "Vegetarian";

//             bool result = Reserveringen.VoegReserveringToe(guestName, numberOfPeople, reservationDateTime, note);

//             Assert.IsTrue(result);
//         }

//         [TestMethod]
//         public void VoegReserveringToe_False()
//         {
//             string guestName = "Jane Doe";
//             int numberOfPeople = 2;
//             DateTime reservationDateTime = DateTime.Now.AddHours(-5);
//             string note = "Gluten Free";
            
//             bool result = Reserveringen.VoegReserveringToe(guestName, numberOfPeople, reservationDateTime, note);

//             Assert.IsFalse(result);
//         }
//     }
// }
