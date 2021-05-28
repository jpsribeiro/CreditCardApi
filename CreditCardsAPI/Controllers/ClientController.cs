using Microsoft.AspNetCore.Mvc;
using CreditCardsAPI.Persistence;
using CreditCardsAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreditCardsAPI.Interfaces;

namespace CreditCardsAPI.Controllers
{
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository) 
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            return Ok(await _clientRepository.GetAllClientsAsync());
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetClient(string email)
        {
            if (!email.Contains('@'))
            {
                return BadRequest("E-mail invalido!");
            }

            var Client = await _clientRepository.GetClientByEmailAsync(email);

            if (Client == null)
            {
                return NotFound();
            }

            return Ok(Client);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClientInputModel clientInputModel)
        {
            if (clientInputModel == null)
            {
                return BadRequest();
            }

            if (!clientInputModel.Email.Contains('@')) 
            {
                return NotFound("E-mail invalido!");
            }

            var Client = new Client(clientInputModel.Name, clientInputModel.Email);

            var ClientCreated = await _clientRepository.CreateClientAsync(Client);

            if (ClientCreated)
            {
                return Ok("Cliente " + Client.Email + " registrado com Sucesso");
            }

            return Ok("Cliente " + Client.Email + " já registrado");
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete(string email) 
        {
            if (!email.Contains('@'))
            {
                return BadRequest("E-mail invalido!");
            }

            await _clientRepository.RemoveClientAsync(email);

            return NoContent();
        }
    }
}
