using Bookify.Infrastructure;
using Bookify.Infrastructure.Services;
using Bookify.Models.Results;
using Bookify.Services;
using Dapper;

namespace Bookify.Models.Services.Impl
{
    public class ApartmentRepository(
        ApartmentSettingService settingService,
        ISqlConnectionFactory sqlConnectionFactory,
        ApplicationDbContext dbContext) : IApartmentRepository
    {
        public Result<Guid> CreateAppartment(string name, string address, decimal price)
        {
            var addressIsRegistered = GetByAddress(address);
            if (addressIsRegistered)
            {
                return Result.Failure<Guid>(ApartmentError.Exists);
            }

            var apartment = Apartment.Create(
                settingService,
                name,
                address,
                price);

            dbContext.Add(apartment);
            dbContext.SaveChanges();

            return apartment.Id;
        }

        private bool GetByAddress(string address)
        {
            return dbContext.Apartments.Any(apartment => apartment.Address == address);
        }

        public Apartment? GetById(Guid id)
        {
            return dbContext.Apartments.FirstOrDefault(apartment => apartment.Id == id);
        }

        private static readonly int[] ActiveBookingStatuses =
        {
            (int)BookingStatus.Reserved,
            (int)BookingStatus.Confirmed
        };

        public Result<IReadOnlyList<Apartment>> GetByPer(DateOnly startDate, DateOnly endDate, decimal? maxPrice)
        {
            var searchedMaxPrice = decimal.MaxValue;

            if (startDate >= endDate)
                return new List<Apartment>();

            if(maxPrice != null)
                searchedMaxPrice = (decimal)maxPrice;

            using var connection = sqlConnectionFactory.CreateConnection();

            const string sql = """
                           SELECT
                               a.Id AS Id,
                               a.Name AS Name,
                               a.Description AS Description,
                               a.Price AS Price,
                               a.Address AS Address
                           FROM apartments AS a
                           WHERE 
                               a.Price <= @searchedMaxPrice
                           AND
                           NOT EXISTS
                           (
                               SELECT 1
                               FROM bookings AS b
                               WHERE
                                   b.apartmentId = a.Id AND
                                   b.dateFrom <= @EndDate AND
                                   b.dateto >= @StartDate AND
                                   b.Status IN @ActiveBookingStatuses 
                           )
                           """;

            var apartments = connection
              .Query<Apartment>(
                sql,
                new
                {
                    startDate,
                    endDate,
                    searchedMaxPrice,
                    ActiveBookingStatuses
                });

            return apartments.ToList();
        }

        public int Create(Apartment item)
        {
            return 0;
        }

        public ICollection<Apartment> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Update(Apartment item)
        {
            throw new NotImplementedException();
        }
    }
}
