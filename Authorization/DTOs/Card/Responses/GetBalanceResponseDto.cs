using Authorization.DTOs.Entities;

namespace Authorization.DTOs.Card.Responses
{
    public record GetBalanceResponseDto(string CardNumber, string CardName, List<TransactionDto> Transactions, decimal Total);
}
