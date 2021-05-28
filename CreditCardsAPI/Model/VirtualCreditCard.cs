using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CreditCardsAPI.Model
{
    public class VirtualCreditCard
    {
        public VirtualCreditCard()
        { 
        }

        public VirtualCreditCard(int number, DateTime date, DateTime validDatetime, CreditCard creditCard, int creditCardNumber)
        {
            Number = number;
            Date = date;
            ValidDatetime = validDatetime;
            CreditCard = creditCard;
            CreditCardNumber = creditCardNumber;
        }

        public int Number { get; set; }

        public DateTime Date { get; set; }

        public DateTime ValidDatetime { get; set; }

        [ForeignKey("CreditCard")]
        public int CreditCardNumber { get; set; }

        [JsonIgnore]
        public virtual CreditCard CreditCard { get; set; }
    }
}
