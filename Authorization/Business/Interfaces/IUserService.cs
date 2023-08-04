using Authorization.DTOs.Entities;

namespace Authorization.Business.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserByCredentials(string email, string password);
    }
}
