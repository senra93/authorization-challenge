namespace Authorization.DTOs.Card.Responses
{
    public record PaymentResponseDto {
        public long TransactionId { get; init; }
        public string Message { get; init; }

        public PaymentResponseDto(long transactionId, string message = "") 
        {
            TransactionId = transactionId;
            Message = message;
        }
    }
}
