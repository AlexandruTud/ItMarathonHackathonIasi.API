using Trading_API.Requests;

namespace Trading_API.Interfaces
{
    public interface ISoldRepository
    {
        Task<int> UpdateSold(UpdateSoldRequest updateSoldRequest);

        Task<decimal> GetSold(int IdUser);
    }
}