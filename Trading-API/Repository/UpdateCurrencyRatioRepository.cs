using Dapper;
using System.Data;
using Trading_API.Interfaces;

namespace Trading_API.Repository
{
    public class UpdateCurrencyRatioRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public UpdateCurrencyRatioRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public void UpdateCurrencyRatio()
        {
            using (var connection = _dbConnectionFactory.ConnectToDataBase())
            {
                connection.Execute("UpdateCurrencyRatio", commandType: CommandType.StoredProcedure);
            }
        }
    }
}
