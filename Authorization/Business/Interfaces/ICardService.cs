using Authorization.DTOs;
using Authorization.DTOs.Card;
using Authorization.DTOs.Card.Responses;

namespace Authorization.Business.Interfaces
{
    public interface ICardService
    {
        Task<GetBalanceResponseDto> GetBalanceAsync(int cardId, DateTime date);
        Task<PaymentResponseDto> PayAsync(PaymentDto payment);
        Task<Response> CreateAsync(CreateCardDto createCardDto);
    }
}
