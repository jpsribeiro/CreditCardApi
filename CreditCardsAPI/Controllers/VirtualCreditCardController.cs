using CreditCardsAPI.Interfaces;
using CreditCardsAPI.Model;
using CreditCardsAPI.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardsAPI.Controllers
{
    [Route("api/[controller]")]
    public class VirtualCreditCardController : ControllerBase
    {

        private readonly IVirtualCreditCard _virtualCreditCard;

        public VirtualCreditCardController(IVirtualCreditCard virtualCreditCard)
        {
            _virtualCreditCard = virtualCreditCard;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok( await _virtualCreditCard.GetAllVirtualCreditCards());
        }

        [HttpGet("{Email}")]
        public async Task<IActionResult> GetByEmail(string Email)
        {
            return Ok( await _virtualCreditCard.GetVirtualCreditCardsByEmail(Email));
        }

        [HttpPost("{Email}")]
        public async Task<IActionResult> Post(string Email) 
        {
            var VirtualCreditCardCreated = await _virtualCreditCard.CreatedVirtualCreditCard(Email);

            if (VirtualCreditCardCreated != 0)
            {
                return Ok("Cartão de Credito Virtual " + VirtualCreditCardCreated + " gerado com sucesso");
            }

            return Ok("Cartão de Credito Virtual não gerado");
        }

        [HttpPut("{Number}/{Payment}")]
        public async Task<IActionResult> Put(int Number, double Payment) 
        {
            await _virtualCreditCard.VirtualCreditCardPayment(Number, Payment);

            return Ok();
        }

        [HttpDelete("{Number}")]
        public async Task<IActionResult> Delete(int Number) 
        {
            await _virtualCreditCard.RemoveVirtualCreditCard(Number);

            return Ok();
        }

    }
}
