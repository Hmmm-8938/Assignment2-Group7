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
    public class MyExceptions : Exception
    {
        /* Exception class that generates the exception messages for each exception that is defined
         * such as if the flight is full, null or name hasn't been given or a citizenship hasn't been given
         */ 
        public MyExceptions(string message)
        {
            if (message == "FLIGHT FULL")
            {

            }
            else if (message == "FLIGHT NULL")
            {

            }
            else if (message == "NAME NULL")
            {

            }
            else if (message == "CITIZENSHIP NULL")
            {

            }
        }
    }
}
