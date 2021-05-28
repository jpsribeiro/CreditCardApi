using CreditCardsAPI.Interfaces;
using CreditCardsAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardsAPI.Persistence.Repositories
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly CreditCardDbContext _creditCardDbContext;

        public CreditCardRepository(CreditCardDbContext creditCardDbContext)
        {
            _creditCardDbContext = creditCardDbContext;
        }

        public async Task<CreditCard> CreateCreditCardAsync(string email)
        {
            var client = await _creditCardDbContext.Clients.Include(x => x.CreditCard).Where(x => x.Email == email).SingleOrDefaultAsync();

            Random randNum = new Random();
            int num = randNum.Next(100000000, 999999999);

            var CreditCard = new CreditCard();

            if (client != null && client.CreditCard == null)
            {
                CreditCard.Number = num;
                CreditCard.Date = DateTime.Now;
                CreditCard.Client = client;
                CreditCard.ClientEmail = email;

                _creditCardDbContext.CreditCards.Add(CreditCard);
                await _creditCardDbContext.SaveChangesAsync();

                return CreditCard;
            }

            return CreditCard;
        }

        public async Task DeleteCreditCard(int num)
        {
            var CreditCard = await _creditCardDbContext.CreditCards.Include(x => x.VirtualCreditCards).SingleOrDefaultAsync(x => x.Number == num);

            if (CreditCard != null)
            {
                _creditCardDbContext.CreditCards.Remove(CreditCard);
                await _creditCardDbContext.SaveChangesAsync();
            }

            return;
        }

        public async Task<List<CreditCard>> GetAllCreditCardsAsync()
        {
            return await _creditCardDbContext.CreditCards.Include(x => x.VirtualCreditCards).ToListAsync();
        }

        public async Task<CreditCard> GetAllCreditCardsByEmailAsync(string email)
        {
            var Client = await _creditCardDbContext.Clients.Include(x => x.CreditCard).ThenInclude(x => x.VirtualCreditCards).Where(x => x.Email == email).SingleOrDefaultAsync();

            return Client.CreditCard;
        }

        public async Task UpdateBalance(double balance, int num)
        {
            var CreditCard = await _creditCardDbContext.CreditCards.SingleOrDefaultAsync(x => x.Number == num);

            if (CreditCard != null)
            {
                CreditCard.Balance += balance;

                await _creditCardDbContext.SaveChangesAsync();
            }
            return;
        }
    }
}
