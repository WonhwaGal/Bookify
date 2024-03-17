using Bookify.Models.Results;

namespace Bookify.Models.Services
{
    public interface IApartmentRepository: IRepository<Apartment>
    {
        Result<Guid> CreateAppartment(string name, string address, decimal price);

        Result<IReadOnlyList<Apartment>> GetByPer(
            DateOnly startDate, DateOnly endDate, decimal? maxPrice);
    }
}