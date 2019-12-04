using System;
using System.Collections.Generic;
using System.Linq;using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Spektro_API_Azure.Model
{
    public class CustomerModel
    {
        private int id;
        private string userModel;
        private string email;
        private string koreord;
        private string firstName;
        private string lastName;
        private bool emailNotification;
        private bool smsNotification;
        private int phoneNo;
    }

    public CustomerModel(){}

    public CustomerModel(int id, string userModel, string email, string kodeord, string firstName, string lastName, bool emailNotification, bool smsNotification, int phoneNo) {
        this.id = id;
        this.userModel=userModel;
        this.email=email;
        this.kodeord=kodeord;
        this.firstName=firstName;
        this.lastName=lastName;
        this.emailNotification=emailNotification;
        this.smsNotification=smsNotification;
        this.phoneNo=phoneNo;
    }

public int Id { get => id; set => id = value; }
public string UserModel { get => userModel; set => userModel = value; }
public string Email { get => email; set => email = value; }
public string Kodeord { get => kodeord; set => kodeord = value; }
public string FirstName { get => firstName; set => firstName = value; }
public string LastName { get => lastName; set => lastName = value; }
public bool EmailNotification { get => emailNotification; set => emailNotification = value; }
public bool SmsNotification { get => smsNotification; set => smsNotification = value; }
public int PhoneNo { get=>phoneNo; set =>phoneNo=value;}
}
