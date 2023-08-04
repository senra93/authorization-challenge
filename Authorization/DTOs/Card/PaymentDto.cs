using System.ComponentModel.DataAnnotations;

namespace Authorization.DTOs.Card
{
    public record PaymentDto
    {
        [Required(ErrorMessage = "The CardNumber cannot be empty or null.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "The CardName cannot be empty or null.")]
        public string CardName { get; set; }

        [Required(ErrorMessage = "The SecurityCode cannot be empty or null.")]
        public string SecurityCode { get; set; }

        [Required(ErrorMessage = "The Expiration cannot be null.")]
        public DateTime Expiration { get; set; }

        [Required(ErrorMessage = "The Amount cannot be zero.")]
        public decimal Amount { get; set; }
    }
}
