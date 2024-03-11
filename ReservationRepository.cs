/* Assignment #2: Abstract Classes, Event Driven Applications, and Exception Handling
 * Group 7: Benjamin Mellott, Jaxson Burdett, Joshua Rustulka, Shannon Hilland
 * Date: 03/10/2024
 * This program allows users to book flights, and check their reservations.
 * The flight will be booked and saved to a bin file.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assignment2
{
    /*
    * The ReservationRepository class hold the CreateReservationCode method which when called, chooses 1 random char from the alphabet char list.
    * Then it randomly selects a 4 digit number and appends the char and number together and returns it as a string.
    */
    public class ReservationRepository
    {
        public static string CreateReservationCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var stringChar = new char[1];
            Random random = new Random();
            for (int i = 0; i < stringChar.Length; i++)
            {
                stringChar[i] = chars[random.Next(chars.Length)];
            }

            var reservationCode = new StringBuilder();

            reservationCode.Append(stringChar);
            reservationCode.Append(random.Next(1000, 9999));

            return reservationCode.ToString();
        }
    }
}
