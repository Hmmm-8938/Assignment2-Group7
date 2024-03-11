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
    * ReservationList inherits from FlightRepository. ReservationList take and reads from the reservation binary file, splits, converts then into reservation objects
    * and stores them in a Reservation List.
    */

    public class ReservationList : FlightRepository
    {
        // Reservation list class variables
        private string PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\Resources\raw\reservations.bin");
        private string reservationCode;
        private string customerName;
        private string customerCitizenship;
        // The defined properties for the reservation list class variables
        public string ReservationCode
        {
            get { return reservationCode; }
        }
        public string CustomerName
        {
            get { return customerName; }
        }
        public string CustomerCitizenship
        {
            get { return customerCitizenship; }
        }

        // Reservation List default constructor
        public ReservationList() 
        {
        
        }

        // Reservation List defined constructor
        public ReservationList(string reservationCode, string flightcode, string airline, string departingAirport, string arrivalAirport, string dayOfWeek, string departureTimeOfFlight, string availableSeats, string flightCost, string customerName, string customerCitizenship) 
            : base (flightcode, airline, departingAirport, arrivalAirport, dayOfWeek, departureTimeOfFlight, availableSeats, flightCost)
        {
            this.reservationCode = reservationCode;
            this.customerName = customerName;
            this.customerCitizenship = customerCitizenship;
        }

        /*
         * This will create reservations based on reading each line in 
         * the Binary File and catch an exception if it reachs the end 
         * so the program won't stop unepectedly
         */
        public List<ReservationList> CreateReservations()
        {
            List<ReservationList> reservations = new List<ReservationList>();
            List<string> binFileList = new List<string>();
            using BinaryReader reader = new BinaryReader(File.Open(PATH, FileMode.Open));
            {
                try
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        string line = reader.ReadString();
                        binFileList.Add(line);

                    }
                }
                catch (EndOfStreamException)
                {

                }
                catch (Exception ex)
                {

                }
                foreach (string line in binFileList)
                {
                    ReservationList reservation = CreateReservation(line);
                    if (reservation == null)
                    {
                        break;
                    }
                    else
                    {
                        reservations.Add(reservation);
                    }
                }
                return reservations;
            }
        }
        // Creates the reservation with the information given in the array of items 
        private ReservationList CreateReservation(string line)
        {
            
            string[] items = line.Split(',');
            string reservationCode = items[0].Trim();
            string flightcode = items[1].Trim();
            string airline = items[2].Trim();
            string departingAirport = items[3];
            string arrivalAirport = items[4].Trim();
            string dayOfWeek = items[5].Trim();
            string departureTimeOfFlight = items[6].Trim();
            string availableSeats = items[7].Trim();
            string flightCost = items[8].Trim();
            string customerName = items[9].ToUpper().Trim(); 
            string customerCitizenship = items[10].ToUpper().Trim();
            ReservationList reservationObject = new ReservationList(reservationCode, flightcode, airline, departingAirport, arrivalAirport, dayOfWeek, departureTimeOfFlight, availableSeats, flightCost, customerName, customerCitizenship);
            return reservationObject;

        }

        // The ToString method which shows the information for the reservation in a string.
        public override string ToString()
        {
            return $"{ReservationCode}, {CustomerName}, {CustomerCitizenship}, {base.ToString()}";
        }

    }
}