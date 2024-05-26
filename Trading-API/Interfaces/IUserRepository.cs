using Trading_API.DTOs;
using Trading_API.Requests;
using Trading_API.Resposes;

namespace Trading_API.Interfaces
{
    public interface IUserRepository
    {
        Task<int> LoginUser(LoginRequest loginRequest);
        Task<int> RegisterUser(RegisterRequest registerRequest);
        Task<int> GetUserRoleById(int IdUser);
        Task<int> UpdatePassword(UpdatePasswordRequest updatePasswordRequest);
        Task<IEnumerable<UserDetailsDTO>> GetUserDetailsAsync();
        Task<IEnumerable<UserTransactionResponse>> GetUserTransactions(int idUser);

    }
}