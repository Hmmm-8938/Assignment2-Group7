/* Assignment #2: Abstract Classes, Event Driven Applications, and Exception Handling
 * Group 7: Benjamin Mellott, Jaxson Burdett, Joshua Rustulka, Shannon Hilland
 * Date: 03/10/2024
 * This program allows users to book flights, and check their reservations.
 * The flight will be booked and saved to a bin file.
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public partial class Reservations : ContentPage
    {
        //List variables
        private List<ReservationList> reservationsList;
        public List<ReservationList> foundReservations;
        public List<ReservationList> ReservationsList
        {
            get { return reservationsList; }
        }
        public Reservations()
        {
            InitializeComponent();
            ReservationList reservationRepository = new ReservationList();
            reservationsList = reservationRepository.CreateReservations();
            
        }
        /* 
        * FindReservations is called when the button in Reservations.xaml is pressed under the search for reservation section.
        * FindReservations takes the inputs from entry boxes in Reservations.xaml and stores them as variables. 
        * The method then reads through ReservationLists and add any matching reservations with the entry information into a new list called foundReservations.
        * Then the reservation that match teh criteria are displayed and if no reservations are found a message displays saying none were found.
        */
        public void FindReservations(object sender, EventArgs e)
        {
            //Converts reservationCode, airlineName and customerName ToUpper so code will run whether lowercase or uppercase is inputted.
            string reservationCode = reservationCodeEntry.Text;
            if(reservationCodeEntry.Text != null )
            {
                
                char c = reservationCode[0];
                string cString = c.ToString();
                cString = cString.ToUpper();
                string codeSuffix = ($"{reservationCode[1]}{reservationCode[2]}{reservationCode[3]}{reservationCode[4]}");
                reservationCode = ($"{cString}{codeSuffix}");
            } 
            else
            {
                reservationCode = null;
            }
            string airline = airlineEntry.Text;
            if(airlineEntry.Text != null)
            {
                airline = airlineEntry.Text.ToUpper();
            }
            else
            {
                airline = null;
            }
            string name = customerNameEntry.Text;
            if (customerNameEntry.Text != null)
            {
                name = customerNameEntry.Text.ToUpper();
            } 
            else
            {
                name = null;
            }

            // Foreach statement that runs through every reservation in ReservationList to see if all inputted entry match with all stored reservation values.
            foundReservations = new List<ReservationList>();
            foreach (ReservationList reservation in ReservationsList)
            {
                if ((reservation.ReservationCode == reservationCode) && (reservation.Airline == airline) && (reservation.CustomerName == name))
                {
                    foundReservations.Add(reservation);
                }
                else if ((reservation.ReservationCode == reservationCode) && (reservation.Airline == airline) && (name == null))
                {
                    foundReservations.Add(reservation);
                }
                else if ((reservation.ReservationCode == reservationCode) && (airline == null) && (reservation.CustomerName == name))
                {
                    foundReservations.Add(reservation);
                }
                else if ((reservation.ReservationCode == reservationCode) && (airline == null) && (name == null))
                {
                    foundReservations.Add(reservation);
                }
                else if ((reservationCode == null) && (airline == null) && (name == null))
                {
                    foundReservations.Add(reservation);
                }
                else if ((reservationCode == null) && (airline == null) && (reservation.CustomerName == name))
                {
                    foundReservations.Add(reservation);
                }
                else if ((reservationCode == null) && (reservation.Airline == airline) && (name == null))
                {
                    foundReservations.Add(reservation);
                }
                else if ((reservationCode == null) && (reservation.Airline == airline) && (reservation.CustomerName ==  name))
                {
                    foundReservations.Add(reservation);
                }
            }
            //Checks if any reservations were found and displays accordingly
            if (foundReservations.Count <= 0)
            {
                reservationPicker.Title = "No matching reservations found";
                reservationPicker.ItemsSource = null;
                customerNameEntry.Text = null;
                airlineEntry.Text = null;
                reservationCodeEntry.Text = null;
            }
            else
            {
                reservationPicker.Title = "Please Select a Reservation";
                reservationPicker.ItemsSource = foundReservations;
                customerNameEntry.Text = null;
                airlineEntry.Text = null;
                reservationCodeEntry.Text = null;
                
            }

        }

        //Updates the reservation fields when an item in the picker is clicked on.
        private async void PickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picked = (Picker)sender;
            int indexSelected = picked.SelectedIndex;

            if (indexSelected != -1)
            {
                ReservationList reservation = foundReservations[indexSelected];
                DisplayAlert("Flight Details", $"Reservation Code: {reservation.ReservationCode}\n Customer Name: {reservation.CustomerName}\n Citizenship: {reservation.CustomerCitizenship}\n Flightcode: {reservation.FlightCode}\n Airline: {reservation.Airline}\n Departing From:{reservation.DepartingAirport} on {reservation.WeekdayOfFlight} at {reservation.DepartureTimeOfFlight}\n Arriving To: {reservation.ArrivalAirport}\n Cost: ${reservation.FlightPrice} CAD", "Let's Fly!");
            }

        } 

    }
}
