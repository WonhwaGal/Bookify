using Dapper;
using Bookify.Infrastructure.Services;
using Bookify.Infrastructure.Services.Impl;
using Bogus.DataSets;
using Bookify.Models.Results;

namespace Bookify.Models.Services.Impl
{
    public class ReviewRepository(ISqlConnectionFactory sqlConnectionFactory) : IReviewRepository
    {
        public int Create(Review item)
        {
            throw new NotImplementedException();
        }

        public ICollection<Review> GetAll()
        {
            throw new NotImplementedException();
        }

        public Review? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Result<IReadOnlyList<Review>> GetByPer(Guid appartID, DateOnly? startDate, DateOnly? endDate, byte? rating)
        {
            DateOnly startD = startDate ?? DateOnly.FromDayNumber(1000);
            DateOnly endD = endDate ?? DateOnly.FromDateTime(DateTime.Today);

            if (startDate >= endDate)
                return new List<Review>();

            using var connection = sqlConnectionFactory.CreateConnection();

            const string sql = """
                           SELECT
                               rev.Id AS Id,
                               rev.Rating AS Rating,
                               rev.Comment AS Comment,
                               rev.ApartmentId AS ApartmentId,
                               rev.CreatedOnUtc AS CreatedOnUtc
                           FROM reviews AS rev
                           WHERE 
                               rev.ApartmentId = @appartID AND
                               rev.Rating = CASE WHEN @rating IS NULL THEN rev.Rating ELSE @rating END AND
                               rev.CreatedOnUtc >= CASE WHEN @startD IS NULL THEN rev.CreatedOnUtc ELSE @startD END AND
                               rev.CreatedOnUtc <= CASE WHEN @endD IS NULL THEN rev.CreatedOnUtc ELSE @endD END
                           """;

            var reviews = connection
                .Query<Review>(
                sql,
                new
                {
                    appartID,
                    startD,
                    endD,
                    rating
                });

            return reviews.ToList();
        }

        public int Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Update(Review item)
        {
            throw new NotImplementedException();
        }
    }
}