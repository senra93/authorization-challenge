using Authorization.Business.Interfaces;
using Authorization.DAL;
using Authorization.DAL.Entities;
using Authorization.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Business
{
    public class SeedService: ISeedService
    {
        private readonly AuthorizationDbContext _authorizationDbContext;

        public SeedService(AuthorizationDbContext authorizationDbContext) 
        { 
            _authorizationDbContext = authorizationDbContext;
        }

        public async Task SeedAsync()
        {
            try
            {
                var cards = new List<Card>()
                {
                    new Card()
                    {
                        Id = 1,
                        UserId = 1,
                        Expiration = DateTime.Now.AddMonths(5),
                        StartDate = DateTime.Now.AddMonths(-3),
                        Limit = 1000000,
                        Name = "Juan Perez",
                        Number = "123456789012345",
                        SecurityCode = "412",
                    },
                    new Card()
                    {
                        Id = 2,
                        UserId = 1,
                        Expiration = DateTime.Now.AddMonths(5),
                        StartDate = DateTime.Now.AddMonths(-3),
                        Limit = 1000000,
                        Name = "Juan Perez",
                        Number = "123456789012245",
                        SecurityCode = "123",
                    },
                };

                var transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Id = 1,
                        Description = "Supermarket",
                        CardId = 1,
                        Amount = 10000,
                        Date = DateTime.Now.AddMonths(-2),
                    },
                    new Transaction()
                    {
                        Id = 2,
                        Description = "Shop",
                        CardId = 1,
                        Amount = 15000,
                        Date = DateTime.Now,
                    },
                    new Transaction()
                    {
                        Id = 3,
                        Description = "e-commerce",
                        CardId = 1,
                        Amount = 2000,
                        Date = DateTime.Now.AddMonths(-1),
                    },
                    new Transaction()
                    {
                        Id = 4,
                        Description = "e-commerce",
                        CardId = 2,
                        Amount = 2000,
                        Date = DateTime.Now.AddMonths(-1),
                    },
                };

                var users = new List<User>()
                {
                    new User()
                    {
                        Id = 1,
                        Email = "juanPerez@gmail.com",
                        Password = Convert.ToBase64String( PasswordHasher.HashPassword("password")),
                    },
                };

                _authorizationDbContext.Cards.AddRange(cards);
                _authorizationDbContext.Users.AddRange(users);
                _authorizationDbContext.Transactions.AddRange(transactions);
                await _authorizationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Card>> CardsAsync()
        {
            try
            {
                return await _authorizationDbContext.Cards.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Transaction>> TransactionsAsync()
        {
            try
            {
                return await _authorizationDbContext.Transactions.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<User>> UsersAsync()
        {
            try
            {
                return await _authorizationDbContext.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
