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
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCardRepository _creditCardRepository;

        public CreditCardController(ICreditCardRepository creditCardRepository) 
        {
            _creditCardRepository = creditCardRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _creditCardRepository.GetAllCreditCardsAsync());
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetCreditCard(string email)
        {
            if (!email.Contains('@'))
            {
                return BadRequest("E-mail invalido!");
            }

            return Ok(await _creditCardRepository.GetAllCreditCardsByEmailAsync(email));
        }

        [HttpPost("{email}")]
        public async Task<IActionResult> Post(string email)
        {
            if (!email.Contains('@'))
            {
                return BadRequest("E-mail invalido!");
            }

            var CreatedCreditCard = await _creditCardRepository.CreateCreditCardAsync(email);

            if (!string.IsNullOrEmpty(CreatedCreditCard.ClientEmail))
            {
                return Ok("Cartão " + CreatedCreditCard.Number + " registrado com Sucesso");
            }

            return Ok("Cartão não registrado");
        }

        [HttpPut("{num}")]
        public IActionResult Put([FromBody] CreditCardInputModel creditCardInputModel, int num) 
        {
            if (creditCardInputModel == null)
            {
                return BadRequest();
            }

            _creditCardRepository.UpdateBalance(creditCardInputModel.Balance, num);

            return NoContent();
        }

        [HttpDelete("{Number}")]
        public IActionResult Delete(int Number)
        {
            _creditCardRepository.DeleteCreditCard(Number);

            return NoContent();
        }


    }
}
