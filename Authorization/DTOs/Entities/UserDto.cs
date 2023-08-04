namespace Authorization.DTOs.Entities
{
    public record UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
