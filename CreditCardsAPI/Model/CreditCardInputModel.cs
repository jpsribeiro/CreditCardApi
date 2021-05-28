using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardsAPI.Model
{
    public class CreditCardInputModel
    {
        public string ClientEmail { get; set; }

        public double Balance { get; set; }
    }
}
