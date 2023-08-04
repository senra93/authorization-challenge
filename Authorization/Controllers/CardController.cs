using Authorization.Business.Interfaces;
using Authorization.DTOs;
using Authorization.DTOs.Card;
using Authorization.DTOs.Card.Responses;
using Authorization.DTOs.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet("balance/{id}/{date}")]
        public async Task<ActionResult<GetBalanceResponseDto>> GetBalanceAsync(int id, string date)
        {
            DateTime balanceDate;

            if (id == 0) return BadRequest("id");

            if (!DateTime.TryParse(date, out balanceDate)) return BadRequest("date");

            var balance = await _cardService.GetBalanceAsync(id, balanceDate);

            if (balance == null)
            {
                return NotFound();
            }

            return balance;
        }

        [HttpPost()]
        public async Task<ActionResult<Response>> CreateAsync([FromBody] CreateCardDto createCardDto)
        {
            try
            {
                if (createCardDto.Expiration <= DateTime.Now || (createCardDto.Expiration.Year == DateTime.Now.Year && createCardDto.Expiration.Month <= DateTime.Now.Month))
                {
                    return BadRequest("Expiration is not valid.");
                }

                var response = await _cardService.CreateAsync(createCardDto);
                return Created("", response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("pay")]
        public async Task<ActionResult<PaymentResponseDto>> PayAsync([FromBody] PaymentDto payDto)
        {
            try
            {
                var response = await _cardService.PayAsync(payDto);

                if (response == null) return NotFound("CardNumber");

                if (response.TransactionId == 0) return BadRequest(response.Message);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}