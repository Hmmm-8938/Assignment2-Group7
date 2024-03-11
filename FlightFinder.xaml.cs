/* Assignment #2: Abstract Classes, Event Driven Applications, and Exception Handling
 * Group 7: Benjamin Mellott, Jaxson Burdett, Joshua Rustulka, Shannon Hilland
 * Date: 03/10/2024
 * This program allows users to book flights, and check their reservations.
 * The flight will be booked and saved to a bin file.
 */


namespace Assignment2;

/* This class contains all of the functions of the flight finder, it gets the flight and airport lists and 
 * compares them against user input to create a list of foundflights then auto insert the input into the reservation fields */

public partial class FlightFinder : ContentPage
{
    //Class Variables
    private List<AirportRepository> airports;
    private List<FlightRepository> flights;
    public List<FlightRepository> foundFlights;
    public List<AirportRepository> Airports
    {
        get { return airports; }
    }
    public List<FlightRepository> Flights
    {
        get { return flights; }
    }

    //Initializes required object lists needed for the rest of the functions to run
    public FlightFinder()
    {
        InitializeComponent();
        AirportRepository airportRepository = new AirportRepository();
        airports = airportRepository.CreateAirports();
        FlightRepository flightRepository = new FlightRepository();
        flights = flightRepository.CreateFlights();

    }

    /*When 'Find Flights' button is clicked, this function takes input from the user to filter 
     * relevant flights from the flights list and populate foundFlights list which is then used in
     * the reservation picker.
     */
    public void FindFlights(object sender, EventArgs e)
    {
        foundFlights = new List<FlightRepository>();
        foreach (FlightRepository flight in Flights)
        {
            string pT = null;
            string dT = null;
            string dayT = null;

            if (origin.Text != null)
            {
                pT = origin.Text.ToUpper();
            }
            else
            {
                pT = null;
            }
            string dA = flight.DepartingAirport.ToUpper();
            if (destination.Text != null)
            {
                dT = destination.Text.ToUpper();
            }
            else
            {
                dT = null;
            }
            string aA = flight.ArrivalAirport.ToUpper();
            if (day.Text != null)
            {
                dayT = day.Text.ToUpper();
            }
            else
            {
                dayT = null;
            }
            string wOF = flight.WeekdayOfFlight.ToUpper();

            if ((pT == dA) || (pT == null))
            {
                if ((dT == aA) || (dT == null))
                {
                    if ((dayT == wOF) || (dayT == null))
                    {
                        foundFlights.Add(flight);
                    }
                }
            }
        }

        flightsPicker.ItemsSource = foundFlights;



    }

    // When a flight is picked this will update the values on the front end UI
    private void PickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picked = (Picker)sender;
        int indexSelected = picked.SelectedIndex;

        if (indexSelected != -1)
        {
            FlightRepository flight = foundFlights[indexSelected];
            flightCode.Text = flight.FlightCode;
            airline.Text = flight.Airline;
            dayOfWeek.Text = flight.WeekdayOfFlight;
            time.Text = flight.DepartureTimeOfFlight;
            cost.Text = flight.FlightPrice;
        }

    }

    // Reserves the flight but also throws exceptions if a flight isn't selected, name hasn't been entered, or citizenship hasn't been entered.
    private async void Reserve(object sender, EventArgs e)
    {

        if (flightCode.Text == null || airline.Text == null || dayOfWeek.Text == null || time.Text == null || cost.Text == null)
        {
            try
            {
                throw new MyExceptions("FLIGHT NULL");
            }
            catch (MyExceptions ex)
            {
                await DisplayAlert("WHAT FLIGHT ARE YOU BOARDING?", "PLEASE SELECT A FLIGHT TO CONFIRM RESERVATION", "OK");
            }
        }
        if (customerName.Text == null)
        {
            try
            {
                throw new MyExceptions("NAME NULL");
            }
            catch (MyExceptions ex)
            {
                await DisplayAlert("WHO ARE YOU?", "PLEASE ENTER YOUR NAME TO CONFIRM RESERVATION", "OK");
            }
        }
        if (customerCitizenship.Text == null)
        {
            try
            {
                throw new MyExceptions("CITIZENSHIP NULL");
            }
            catch (MyExceptions ex)
            {
                await DisplayAlert("WHERE ARE YOU FROM?", "PLEASE ENTER YOUR COUNTRY OF CITIZENSHIP TO CONFIRM RESERVATION", "OK");
            }
        }
        string nameInput = customerName.Text;
        if (nameInput != null)
        {
            nameInput = customerName.Text.ToUpper();
        }
        else
        {
            nameInput = null;
        }
        string citizenshipInput = customerCitizenship.Text;
        if (citizenshipInput != null)
        {
            citizenshipInput = customerCitizenship.Text.ToUpper();
        }
        else
        {
            citizenshipInput = null;
        }
        string reserveCode = ReservationRepository.CreateReservationCode();
        


        /* Checks to see what flights match the list of existing flights 
         * if a flight is full it will notify the user and have them 
         * select another flight otherwise it will save stuff to the file
         */
        foreach (FlightRepository flight in flights)
        {
            if (int.Parse(flight.AvailableSeats) > 0)
            {
                if (flightCode.Text == flight.FlightCode)
                {
                    if (airline.Text == flight.Airline)
                    {
                        if (dayOfWeek.Text == flight.WeekdayOfFlight)
                        {
                            if (time.Text == flight.DepartureTimeOfFlight)
                            {
                                if (cost.Text == flight.FlightPrice)
                                {
                                    if ((customerName.Text != null) && (customerCitizenship.Text != null))
                                    {
                                        reservationCode.Text = "Reservation Code: " + reserveCode;
                                        ReservationList matchingFlight = new ReservationList(reserveCode, flight.FlightCode, flight.Airline, flight.DepartingAirport, flight.ArrivalAirport, flight.WeekdayOfFlight, flight.DepartureTimeOfFlight, flight.AvailableSeats, flight.FlightPrice, nameInput, citizenshipInput);
                                        List<ReservationList> matchingFlights = new List<ReservationList>();
                                        int available = int.Parse(flight.AvailableSeats);
                                        available -= 1;
                                        flight.AvailableSeats = available.ToString();
                                        matchingFlights.Add(matchingFlight);
                                        string PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\Resources\raw\reservations.bin");
                                        using (BinaryWriter writer = new BinaryWriter(File.Open(PATH, FileMode.Append)))
                                        {
                                            foreach (ReservationList line in matchingFlights)
                                            {
                                                writer.Write($"{line.ReservationCode}, {line.FlightCode}, {line.Airline.ToUpper()}, {line.DepartingAirport}, {flight.ArrivalAirport}, {line.WeekdayOfFlight}, {line.DepartureTimeOfFlight}, {line.AvailableSeats}, {line.FlightPrice}, {line.CustomerName}, {line.CustomerCitizenship}");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (int.Parse(flight.AvailableSeats) == 0)
            {
                break;
            }
            else
            {
                try
                {
                    throw new MyExceptions("FLIGHT FULL");
                }
                catch (MyExceptions ex)
                {
                    await DisplayAlert("FLIGHT FULL", "THE FLIGHT YOU HAVE SELECTED IS FULL", "OK");
                }
            }
            
        }

        /*
         * Resets all the values on the front end UI to allow new values to be entered.
         */
        flightsPicker.SelectedItem = null;
        flightCode.Text = null;
        airline.Text = null;
        dayOfWeek.Text = null;
        time.Text = null;
        cost.Text = null;
        customerName.Text = null;
        customerCitizenship.Text = null;

    }
}
    

    
