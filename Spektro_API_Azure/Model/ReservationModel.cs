using System;

namespace Spektro_API_Azure.Model
{
    public class ReservationModel
    {
        private string firstName;
        private string lastName;
        private string emailAddress;
        private string phoneNumber;
        private bool emailNotifcation;
        private bool smsNotification;
        private int noOfPeople;
        private DateTime dateOfReservation;
        private int id;


        public ReservationModel() { }



        public ReservationModel(string firstName, string lastName, string emailAddress, string phoneNumber, bool emailNotifcation, bool smsNotification, int noOfPeople, DateTime dateOfReservation)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.emailAddress = emailAddress;
            this.phoneNumber = phoneNumber;
            this.emailNotifcation = emailNotifcation;
            this.smsNotification = smsNotification;
            this.noOfPeople = noOfPeople;
            this.dateOfReservation = dateOfReservation;
        }

        public ReservationModel(string firstName, string lastName, string emailAddress, string phoneNumber, bool emailNotifcation, bool smsNotification, int noOfPeople, DateTime dateOfReservation, int id)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.emailAddress = emailAddress;
            this.phoneNumber = phoneNumber;
            this.emailNotifcation = emailNotifcation;
            this.smsNotification = smsNotification;
            this.noOfPeople = noOfPeople;
            this.dateOfReservation = dateOfReservation;
            this.id = id;
        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string EmailAddress { get => emailAddress; set => emailAddress = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public bool EmailNotifcation { get => emailNotifcation; set => emailNotifcation = value; }
        public bool SmsNotification { get => smsNotification; set => smsNotification = value; }
        public int NoOfPeople { get => noOfPeople; set => noOfPeople = value; }
        public DateTime DateOfReservation { get => dateOfReservation; set => dateOfReservation = value; }
        public int Id { get => id; set => id = value; }
    }
}
