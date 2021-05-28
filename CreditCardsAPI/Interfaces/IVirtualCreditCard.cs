using CreditCardsAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardsAPI.Interfaces
{
    public interface IVirtualCreditCard
    {
        Task<List<VirtualCreditCard>> GetAllVirtualCreditCards();
        Task<List<VirtualCreditCard>> GetVirtualCreditCardsByEmail(string Email);
        Task<int> CreatedVirtualCreditCard(string email);
        Task VirtualCreditCardPayment(int number, double pay);
        Task RemoveVirtualCreditCard(int number);
    }
}
