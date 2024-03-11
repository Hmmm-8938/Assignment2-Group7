using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Assignment #2: Abstract Classes, Event Driven Applications, and Exception Handling
 * Group 7: Benjamin Mellott, Jaxson Burdett, Joshua Rustulka, Shannon Hilland
 * Date: 03/10/2024
 * This program allows users to book flights, and check their reservations.
 * The flight will be booked and saved to a bin file.
 */

namespace Assignment2
{
    /*
     * This class is for our AirportRespository it takes the airports.csv file and creates airport objects.
     * The class variables that are instantiated here are airportCode and airportName.
     */
    public class AirportRepository
    {
        // Class Variables
        private string airportCode;
        private string airportName;
        // Getters to modify these variables outside of the class.
        public string AirportCode
        {
            get { return airportCode; }
        }
        public string AirportName
        {
            get { return airportName; }
        }

        // Defining the file that the creation of airports will read from.
        public static string AIRPORTS_TEXT = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\Resources\raw\airports.csv");

        // Default Constructor
        public AirportRepository()
        {

        }
        
        // Defined Constructor
        public AirportRepository(string airportCode, string airportName)
        {
            this.airportCode = airportCode;
            this.airportName = airportName;
        }

        /*
         * CreateAirports method 
         * Will create a list of type airport respository and for each line in the file it will create an airport
         */
        public List<AirportRepository> CreateAirports()
        {
            List<AirportRepository> airports = new List<AirportRepository>();
            string[] lines = File.ReadAllLines(AIRPORTS_TEXT);
            foreach (string line in lines) 
            {
                AirportRepository airport = CreateAirport(line);
                if (airport == null)
                {
                    break;
                }
                else
                {
                    airports.Add(airport);
                }
            
            }
            return airports;
        }

        // The the ToString method which will print off the airport information
        public override string ToString()
        {
            return (AirportCode + " " + AirportName);
        }

        // Creates a single airport using the delimiter of a comma as it is reading from a csv file.
        private AirportRepository CreateAirport(string line)
        {
            string[] items = line.Split(',');
            string airportCode = items[0];
            string airportName = items[1];

            AirportRepository airport = new AirportRepository(airportCode, airportName);

            return airport;
        }
    }
}
