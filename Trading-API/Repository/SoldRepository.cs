using Dapper;
using System.Data;
using Trading_API.Interfaces;
using Trading_API.Requests;

namespace Trading_API.Repository
{
    public class SoldRepository : ISoldRepository

    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public SoldRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<int> UpdateSold(UpdateSoldRequest updateSoldRequest)
        {
            var parameters = new DynamicParameters(); 
            parameters.Add("@IdUser", updateSoldRequest.IdUser);
            parameters.Add("@Amount", updateSoldRequest.Amount);
            using (var connection = _dbConnectionFactory.ConnectToDataBase())
            {
                var result = await connection.ExecuteAsync("UpdateSold", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        } 
        public async Task<decimal> GetSold(int IdUser)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@IdUser", IdUser);
            parameters.Add("@Sold", dbType: DbType.Decimal, direction: ParameterDirection.Output);
            using (var connection = _dbConnectionFactory.ConnectToDataBase())
            {
                await connection.ExecuteAsync("GetSold", parameters, commandType: CommandType.StoredProcedure);
                var result = parameters.Get<decimal>("Sold");
                return result;
            }
        }

    }
}
