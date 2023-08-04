using System.Text.Json.Serialization;

namespace Authorization.DAL.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string SecurityCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Expiration { get; set; }
        public int Limit { get; set; }

        [JsonIgnore]
        public User User { get; set; }
        public ICollection<Transaction> Transactions { get; set;}
    }
}
