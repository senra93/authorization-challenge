using System.ComponentModel.DataAnnotations;

namespace Authorization.DTOs.Card
{
    public record CreateCardDto
    {
        [Required(ErrorMessage = "The UserId cannot be zero.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "The Name cannot be empty or null.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Limit cannot be zero.")]
        public int Limit { get; set; }

        [Required(ErrorMessage = "The Expiration cannot be null.")]
        public DateTime Expiration { get; set; }
    }
}
