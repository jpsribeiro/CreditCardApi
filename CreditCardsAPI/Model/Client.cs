using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardsAPI.Model
{
    public class Client
    {
        public Client() { }

        public Client(string name, string email) 
        {
            this.Name = name;
            this.Email = email;
        }

        public string Name { get; set; }
        public string Email { get; set; }

        public virtual CreditCard CreditCard { get; set; }

    }
}
