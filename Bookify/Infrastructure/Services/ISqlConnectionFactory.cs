using System.Data;

namespace Bookify.Infrastructure.Services
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
