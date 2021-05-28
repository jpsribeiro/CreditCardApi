using CreditCardsAPI.Interfaces;
using CreditCardsAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardsAPI.Persistence.Repositories
{
    public class VirtualCreditCardRepository : IVirtualCreditCard
    {
        private readonly CreditCardDbContext _creditCardDbContext;

        public VirtualCreditCardRepository(CreditCardDbContext creditCardDbContext)
        {
            _creditCardDbContext = creditCardDbContext;
        }

        public async Task<int> CreatedVirtualCreditCard(string email)
        {
            var CreditCard = await _creditCardDbContext.CreditCards.Where(x => x.ClientEmail == email).SingleOrDefaultAsync();

            if (CreditCard != null)
            {
                Random randNum = new Random();
                int num = randNum.Next(100000000, 999999999);

                var VirtualCreditCard = new VirtualCreditCard(num, DateTime.Now, DateTime.Now.AddYears(6), CreditCard, CreditCard.Number);

                _creditCardDbContext.VirtualCreditCards.Add(VirtualCreditCard);
                await _creditCardDbContext.SaveChangesAsync();

                return num;
            }

            return 0;
        }

        public async Task<List<VirtualCreditCard>> GetAllVirtualCreditCards()
        {
            return await _creditCardDbContext.VirtualCreditCards.ToListAsync();
        }

        public async Task<List<VirtualCreditCard>> GetVirtualCreditCardsByEmail(string Email)
        {
            var Client = await _creditCardDbContext.Clients.Include(x => x.CreditCard)
                                .ThenInclude(x => x.VirtualCreditCards)
                                .Where(x => x.Email == Email)
                                .SingleOrDefaultAsync();

            return Client.CreditCard.VirtualCreditCards.OrderBy(x => x.Date).ToList();
        }

        public async Task RemoveVirtualCreditCard(int number)
        {
            var virtualcreditcard = await _creditCardDbContext.VirtualCreditCards.Where(x => x.Number == number).SingleOrDefaultAsync();

            if (virtualcreditcard != null) 
            {
                _creditCardDbContext.VirtualCreditCards.Remove(virtualcreditcard);
                await _creditCardDbContext.SaveChangesAsync();
            }

            return; 
        }

        public async Task VirtualCreditCardPayment(int number, double pay)
        {
            var VirtualCreditCard = await _creditCardDbContext.VirtualCreditCards.Include(x => x.CreditCard).Where(x => x.Number == number).SingleOrDefaultAsync();

            if (VirtualCreditCard != null)
            {
                VirtualCreditCard.CreditCard.Balance -= Math.Abs(pay);
                await _creditCardDbContext.SaveChangesAsync();
            }

            return;
        }
    }
}
