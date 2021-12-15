using System.Threading.Tasks;
using EscapeHit.App.Repositories;

namespace EscapeHit.App.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext db;

        public UserRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task Create(UserEntity invoice)
        {
            db.Add(invoice);
            await db.SaveChangesAsync();
        }

        public Task<UserEntity> FindById(int id)
        {
            return Task.FromResult(new UserEntity());
        }
    }
}
