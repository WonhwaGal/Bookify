using Bookify.Models.Results;

namespace Bookify.Models.Services
{
    public interface IReviewRepository : IRepository<Review>
    {
        Result<IReadOnlyList<Review>> GetByPer(
            Guid appartID, DateOnly? startDate, DateOnly? endDate, byte? maxPrice);
    }
}
