namespace Authorization.DTOs.Entities
{
    public class CardDto
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public int SecurityCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Expiration { get; set; }
        public int Limit { get; set; }
    }
}
