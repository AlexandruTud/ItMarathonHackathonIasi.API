using Dapper;
using System.Data;
using Trading_API.Interfaces;

namespace Trading_API.Repository
{
    public class IpozitRepository : IIpozitRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public IpozitRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<decimal> GetTotalImpozit()
        {
            var parameters = new DynamicParameters();
            parameters.Add(@"TotalImpozit", dbType: DbType.Decimal, direction: ParameterDirection.Output);
            using (var connection = _dbConnectionFactory.ConnectToDataBase())
            {
                 await connection.ExecuteAsync("CalculateImpozitTotal", parameters,commandType: CommandType.StoredProcedure);
                var result = parameters.Get<decimal>("TotalImpozit");
                return result;
            }
        }
    }
}
