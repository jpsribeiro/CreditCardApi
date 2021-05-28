using CreditCardsAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardsAPI.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllClientsAsync();
        Task<Client> GetClientByEmailAsync(string email);
        Task<bool> CreateClientAsync(Client client);
        Task RemoveClientAsync(string email);
    }
}
