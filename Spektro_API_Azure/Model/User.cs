using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spektro_API_Azure.Model
{
    public class User
    {
        public string Email { get => Email; set => Email = value; }
        public string Kodeord { get => Kodeord; set => Kodeord = value; }

        public User(string email, string kodeord)
        {
            Email = email;
            Kodeord = kodeord;
        }

    }
}
