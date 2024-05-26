namespace Trading_API.Interfaces
{
    public interface IIpozitRepository
    {
        Task<decimal> GetTotalImpozit();
    }
}