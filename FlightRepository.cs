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
    // The FlightRepository main purpose of this is to have all the flight objects created.
    public class FlightRepository
    {
        // The class variables for FlightRepository class
        private string flightCode;
        private string airline;
        private string departingAirport;
        private string arrivalAirport;
        private string weekdayOfFlight;
        private string departureTimeOfFlight;
        private string availableSeats;
        private string flightPrice;
         
        // Various Properties of variables defined above which allow these attributes to be used in other files
        public string FlightCode
        {
            get { return flightCode; }
        }
        public string Airline
        {
            get { return airline; }
        }
        public string DepartingAirport
        {
            get { return departingAirport; }
        }
        public string ArrivalAirport
        {
            get { return arrivalAirport; }
        }
        public string WeekdayOfFlight
        {
            get { return weekdayOfFlight; }
        }
        public string DepartureTimeOfFlight
        {
            get { return departureTimeOfFlight; }
        }
        public string AvailableSeats
        {
            get { return availableSeats; }
            set { availableSeats = value; }
        }
        public string FlightPrice
        {
            get { return flightPrice; }
        }
   
        // File path of the flights.csv file
        public static string FLIGHTS_TEXT = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\Resources\raw\flights.csv");

        // The default constructor for flightRepository
        public FlightRepository()
        {

        }

        // The defined constructor for flightRepository
        public FlightRepository(string flightCode, string airline, string departingAirport, string arrivalAirport, string weekdayOfFlight, string departureTimeOfFlight, string availableSeats, string flightPrice)
        {
            this.flightCode = flightCode;
            this.airline = airline;
            this.departingAirport = departingAirport;
            this.arrivalAirport = arrivalAirport;
            this.weekdayOfFlight = weekdayOfFlight;
            this.departureTimeOfFlight = departureTimeOfFlight;
            this.availableSeats = availableSeats;
            this.flightPrice = flightPrice;
        }
        
        // Runs at the beginning of the program to estabilish the list of all flights from the file
        public List<FlightRepository> CreateFlights()
        {
            List<FlightRepository> flights = new List<FlightRepository>();
            string[] lines = File.ReadAllLines(FLIGHTS_TEXT);
            foreach (string line in lines)
            {
                FlightRepository flight = CreateFlight(line);
                if (flight == null)
                {
                    break;
                }
                else
                {
                    flights.Add(flight);
                }
            }
            return flights;
        }

        // Shows the details of the flight in a string
        public override string ToString()
        {
            return $"{FlightCode}, {Airline}, {DepartingAirport}, {ArrivalAirport}, {WeekdayOfFlight}, {DepartureTimeOfFlight}, {FlightPrice}";
        }

        // Creates the flight object based on their index in the array of items
        private FlightRepository CreateFlight(string line)
        {
            string[] items = line.Split(',');
            string flightCode = items[0];
            string airline = items[1];
            string departingAirport = items[2];
            string arrivalAirport = items[3];
            string weekdayOfFlight = items[4];
            string departureTimeOfFlight = items[5];
            string availableSeats = items[6];
            string flightPrice = items[7];

            FlightRepository flight = new FlightRepository(flightCode, airline, departingAirport, arrivalAirport, weekdayOfFlight, departureTimeOfFlight, availableSeats, flightPrice);

            return flight;
        }
    }
}
