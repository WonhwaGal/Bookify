using Bookify.Models.Abstractions;

namespace Bookify.Models.Services
{
    public interface IRepository<T> where T : Entity
    {
        int Create(T item);

        ICollection<T> GetAll();

        T? GetById(Guid id);
        int Remove(Guid id);

        int Update (T item);
    }
}
