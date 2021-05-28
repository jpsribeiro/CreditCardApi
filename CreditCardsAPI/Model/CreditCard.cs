using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardsAPI.Model
{

    public class CreditCard
    {
        public CreditCard() { } 

        public int Number { get; set; }
        public double Balance { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Client")]
        public string ClientEmail { get; set; }

        [JsonIgnore]
        public virtual Client Client { get; set; }

        public virtual ICollection<VirtualCreditCard> VirtualCreditCards { get; set; }
    }
}
