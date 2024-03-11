/* Assignment #2: Abstract Classes, Event Driven Applications, and Exception Handling
 * Group 7: Benjamin Mellott, Jaxson Burdett, Joshua Rustulka, Shannon Hilland
 * Date: 03/10/2024
 * This program allows users to book flights, and check their reservations.
 * The flight will be booked and saved to a bin file.
 */

// The initialization and place where routes are registered in the file.
namespace Assignment2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Flights", typeof(FlightFinder));
            Routing.RegisterRoute("Reservations", typeof(Reservations));
        }
    }
}
