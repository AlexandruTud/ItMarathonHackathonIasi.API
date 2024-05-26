using System.Data;

namespace Trading_API.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection ConnectToDataBase();
    }
}