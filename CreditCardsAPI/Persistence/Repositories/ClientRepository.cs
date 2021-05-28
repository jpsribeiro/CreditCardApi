using CreditCardsAPI.Interfaces;
using CreditCardsAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardsAPI.Persistence.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly CreditCardDbContext _creditCardDbContext;

        public ClientRepository(CreditCardDbContext creditCardDbContext)
        {
            _creditCardDbContext = creditCardDbContext;
        }

        public async Task<bool> CreateClientAsync(Client client)
        {
            if (_creditCardDbContext.Clients.Where(x => x.Email == client.Email).SingleOrDefault() == null)
            {
                _creditCardDbContext.Clients.Add(client);
                await _creditCardDbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<List<Client>> GetAllClientsAsync()
        {
            return await _creditCardDbContext.Clients.Include(x => x.CreditCard).ThenInclude(x => x.VirtualCreditCards).ToListAsync();
        }

        public async Task<Client> GetClientByEmailAsync(string email)
        {
            return await _creditCardDbContext.Clients.Include(x => x.CreditCard).ThenInclude(x => x.VirtualCreditCards).Where(x => x.Email == email).SingleOrDefaultAsync();
        }

        public async Task RemoveClientAsync(string email)
        {
            var Client = await _creditCardDbContext.Clients.Include(x => x.CreditCard).ThenInclude(x => x.VirtualCreditCards).SingleOrDefaultAsync(x => x.Email == email);

            if (Client != null)
            {
                _creditCardDbContext.Clients.Remove(Client);
                await _creditCardDbContext.SaveChangesAsync();
            }

            return;
        }
    }
}
