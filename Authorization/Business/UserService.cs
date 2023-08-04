using Authorization.Business.Interfaces;
using Authorization.DAL;
using Authorization.DTOs.Entities;
using Authorization.Utilities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Business
{
    public class UserService: IUserService
    {
        private readonly AuthorizationDbContext _authorizationDbContext;
        private readonly IMapper _mapper;

        public UserService(AuthorizationDbContext authorizationDbContext, IMapper mapper) 
        {
            _authorizationDbContext = authorizationDbContext;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserByCredentials(string email, string password)
        {
            if (email == string.Empty || password == string.Empty)
            {
                throw new ArgumentException("Email and Password cannot be empty");
            }

            try
            {
                var user = await _authorizationDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null) return null;

                var isVerifiedPassword = PasswordHasher.VerifyPassword(password, user.Password);

                if (!isVerifiedPassword) return null;

                var userDto = _mapper.Map<UserDto>(user);

                return userDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
