namespace Authorization.DTOs.Entities
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}
