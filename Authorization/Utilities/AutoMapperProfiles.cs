using Authorization.DAL.Entities;
using Authorization.DTOs.Entities;
using AutoMapper;

namespace Authorization.Utilities
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Transaction, TransactionDto>();
            CreateMap<User, UserDto>();
        }
    }
}
