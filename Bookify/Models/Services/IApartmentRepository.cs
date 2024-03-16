using Bookify.Models.Results;

namespace Bookify.Models.Services
{
    public interface IApartmentRepository: IRepository<Apartment>
    {
        Result<IReadOnlyList<Apartment>> GetByPer(
            DateOnly startDate, DateOnly endDate, decimal? maxPrice);
    }
}