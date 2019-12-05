namespace Spektro_API_Azure.Model
{
    public class CustomerModel
    {
        private int id;
        private string userRole;
        private string email;
        private string kodeord;
        private string firstName;
        private string lastName;
        private bool emailNotification;
        private bool smsNotification;
        private string phoneNo;

        public CustomerModel() { }

        public CustomerModel(int id, string userRole, string email, string kodeord, string firstName, string lastName, bool emailNotification, bool smsNotification, string phoneNo)
        {
            this.id = id;
            this.userRole = userRole;
            this.email = email;
            this.kodeord = kodeord;
            this.firstName = firstName;
            this.lastName = lastName;
            this.emailNotification = emailNotification;
            this.smsNotification = smsNotification;
            this.phoneNo = phoneNo;
        }

        public int Id { get => id; set => id = value; }
        public string UserRole { get => userRole; set => userRole = value; }
        public string Email { get => email; set => email = value; }
        public string Kodeord { get => kodeord; set => kodeord = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public bool EmailNotification { get => emailNotification; set => emailNotification = value; }
        public bool SmsNotification { get => smsNotification; set => smsNotification = value; }
        public string PhoneNo { get => phoneNo; set => phoneNo = value; }
    } 
}
