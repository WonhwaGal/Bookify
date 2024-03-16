
using Bookify.Infrastructure;

namespace Bookify.Models.Services.Impl
{
    public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
    {
        public User? GetById(Guid id)
        {
            return dbContext.Users.FirstOrDefault(user => user.Id == id);
        }

        public int Create(User item)
        {
            throw new NotImplementedException();
        }

        public ICollection<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Update(User item)
        {
            throw new NotImplementedException();
        }
    }
}
