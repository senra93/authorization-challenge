using Authorization.Business.Interfaces;
using Authorization.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly ISeedService _seedService;

        public SeedController(ISeedService seedService) { _seedService = seedService; }

        [HttpGet]
        public async Task SeedAsync()
        {
            await _seedService.SeedAsync();
        }

        [HttpGet("cards")]
        public async Task<ActionResult<List<Card>>> GetCardsAsync()
        {
            return await _seedService.CardsAsync();
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<User>>> GetUsersAsync()
        {
            return await _seedService.UsersAsync();
        }

        [HttpGet("transactions")]
        public async Task<ActionResult<List<Transaction>>> GetTransactionsAsync()
        {
            return await _seedService.TransactionsAsync();
        }
    }
}
