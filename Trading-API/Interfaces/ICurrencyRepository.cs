using Trading_API.Requests;
using Trading_API.Resposes;

namespace Trading_API.Interfaces
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<Resposes.CurrencyResponse>> GetCurrencies();
        Task<int> MakeTransaction(TransactionRequest request);
        Task<bool> AddUserCurrency(CurrencyRequest currencyRequest);
    }
}