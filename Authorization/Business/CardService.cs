using Authorization.Business.Interfaces;
using Authorization.DAL;
using Authorization.DAL.Entities;
using Authorization.DTOs;
using Authorization.DTOs.Card;
using Authorization.DTOs.Card.Responses;
using Authorization.DTOs.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Authorization.Business
{
    public class CardService : ICardService
    {
        private readonly AuthorizationDbContext _authorizationDbContext;
        private readonly IUFEService _ufeService;
        private readonly IMapper _mapper;

        public CardService(AuthorizationDbContext authorizationDbContext, IUFEService ufeService, IMapper mapper)
        {
            _authorizationDbContext = authorizationDbContext;
            _ufeService = ufeService;
            _mapper = mapper;
        }

        public async Task<Response> CreateAsync(CreateCardDto createCardDto)
        {
            try
            {
                var existsCardNumber = true;
                var securityCode = GenerateRandomNumber(3);
                var cardNumber = GenerateRandomNumber(15);

                do
                {
                    existsCardNumber = _authorizationDbContext.Cards.Any(c => c.Number == cardNumber);
                } while (existsCardNumber);

                var card = new Card()
                {
                    UserId = createCardDto.UserId,
                    Name = createCardDto.Name,
                    Limit = createCardDto.Limit,
                    SecurityCode = securityCode,
                    Expiration = createCardDto.Expiration,
                    StartDate = DateTime.Now,
                    Number = cardNumber
                };

                _authorizationDbContext.Cards.Add(card);
                await _authorizationDbContext.SaveChangesAsync();

                return new Response("Card created successfully", 201); //COMPLETAR RETORNO
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<GetBalanceResponseDto> GetBalanceAsync(int cardId, DateTime balanceDate)
        {
            try
            {
                if (cardId == 0)
                {
                    throw new ArgumentException(nameof(cardId));
                }

                var card = _authorizationDbContext.Cards.FirstOrDefault(c => c.Id == cardId);

                if (card == null)
                {
                    return null;
                }

                var transactions = _authorizationDbContext.Transactions
                                        .Where(
                                            t => t.CardId == cardId && 
                                            t.Date.Year == balanceDate.Year && 
                                            t.Date.Month == balanceDate.Month)
                                        .ToList();

                var totalAmount = transactions.Sum(c => c.Amount);
                var transactionsDto = _mapper.Map<List<TransactionDto>>(transactions);

                var response = new GetBalanceResponseDto(card.Number, card.Name, transactionsDto, totalAmount);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PaymentResponseDto> PayAsync(PaymentDto payment)
        {
            var card = await _authorizationDbContext
                                .Cards
                                .FirstOrDefaultAsync(
                                    c => c.Number == payment.CardNumber && 
                                    c.Name.ToUpper() == payment.CardName.ToUpper() &&
                                    c.SecurityCode == payment.SecurityCode && 
                                    c.Expiration.Month == payment.Expiration.Month &&
                                    c.Expiration.Year == payment.Expiration.Year);

            if (card == null) return null;

            var balance = await GetBalanceAsync(card.Id, DateTime.Now);
            var fee = await _ufeService.GetFee();
            var paymentWithFee = payment.Amount + fee;

            if (card.Limit < balance.Total + paymentWithFee)
            {
                return new PaymentResponseDto(0, "The card has no limit available to do the payment.");
            }

            var transaction = new Transaction()
            {
                Amount = paymentWithFee,
                CardId = card.Id,
                Date = DateTime.Now,
                Description = "Shop",
            };

            _authorizationDbContext.Transactions.Add(transaction);
            var transactionId = await _authorizationDbContext.SaveChangesAsync();

            return new PaymentResponseDto(transactionId);
        }

        private string GenerateRandomNumber(int digits)
        {
            Random random = new Random();
            string result = "";

            for (int i = 0; i < digits; i++)
            {
                result += random.Next(0, 10);
            }

            return result;
        }
    }
}
