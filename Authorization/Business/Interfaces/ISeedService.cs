using Authorization.DAL.Entities;

namespace Authorization.Business.Interfaces
{
    public interface ISeedService
    {
        Task SeedAsync();
        Task<List<Card>> CardsAsync();
        Task<List<User>> UsersAsync();
        Task<List<Transaction>> TransactionsAsync();
    }
}
