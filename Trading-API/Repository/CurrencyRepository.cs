using Dapper;
using Org.BouncyCastle.Asn1.IsisMtt.X509;
using System.Data;
using Trading_API.Interfaces;
using Trading_API.Requests;
using Trading_API.Resposes;

namespace Trading_API.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly ILogger<CurrencyRepository> _logger;
        private readonly IDbConnectionFactory   _dbConnectionFactory;
        public CurrencyRepository(IDbConnectionFactory dbConnectionFactory, ILogger<CurrencyRepository> logger)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _logger = logger;
        }
        public async Task<IEnumerable<Resposes.CurrencyResponse>> GetCurrencies()
        {
            _logger.LogInformation("Lista Monede");
            using (var connection = _dbConnectionFactory.ConnectToDataBase())
            {
                var result = await connection.QueryAsync<Resposes.CurrencyResponse>("GetCurrency", commandType: CommandType.StoredProcedure);
                IEnumerable<CurrencyResponse> loggerCurency = result;
                foreach (var item in loggerCurency)
                {
                    _logger.LogInformation(" IdUSer: {IdUser}, CriptoName: {CriptoName}, IdCripto: {IdCripto}", item.IdUser.ToString(), item.CriptoName.ToString(),item.IdCripto.ToString());
                }
                return result;
            }
        }
        public async Task<int> MakeTransaction (TransactionRequest request)
        {
            var parameters = new DynamicParameters();   
            parameters.Add("@IdCripto", request.IdCripto);
            parameters.Add("@IdUser", request.IdUser);
            parameters.Add("@Date", request.Date);
            parameters.Add("@SumaLei", request.SumaLei);
            parameters.Add("@TransactionType", request.TransactionType);
            _logger.LogInformation("MakeTransaction , parameters : " +parameters);
            using (var connection = _dbConnectionFactory.ConnectToDataBase())
            {
                var result = await connection.ExecuteAsync("InsertTransaction", parameters, commandType: CommandType.StoredProcedure);
                _logger.LogInformation("MakeTransaction result: {result}", result);
                return result;
            }
        }
        public async Task<bool> AddUserCurrency (CurrencyRequest currencyRequest)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@IdUser", currencyRequest.IdUser);
            parameters.Add(@"CriptoName", currencyRequest.CriptoName);
            parameters.Add("@Ratio", currencyRequest.Ratio);
            parameters.Add(@"PictureLink", currencyRequest.PictureLink);
            parameters.Add(@"Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            using (var connection = _dbConnectionFactory.ConnectToDataBase())
            {
                await connection.ExecuteAsync("AddCurrency", parameters, commandType: CommandType.StoredProcedure);
                var result = parameters.Get<bool>("Success");
                return result;
            }
        }
    }
}
