using CreditCardsAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardsAPI.Interfaces
{
    public interface ICreditCardRepository
    {
        Task<List<CreditCard>> GetAllCreditCardsAsync();
        Task<CreditCard> GetAllCreditCardsByEmailAsync(string email);
        Task<CreditCard> CreateCreditCardAsync(string email);
        Task UpdateBalance(double balance, int num);
        Task DeleteCreditCard(int num);
    }
}
