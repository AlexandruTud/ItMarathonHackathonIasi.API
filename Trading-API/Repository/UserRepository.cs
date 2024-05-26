using Dapper;
using System.Data;
using System.Text;
using Trading_API.Interfaces;
using Trading_API.Requests;
using System.Security.Cryptography;
using Trading_API.DTOs;

namespace Trading_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public UserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<int> LoginUser(LoginRequest loginRequest)
        {
            var hashedPassword = HashPassword(loginRequest.Password);        
            var parameters = new DynamicParameters();
            parameters.Add(@"Email", loginRequest.Email);
            parameters.Add(@"Password", hashedPassword);
            parameters.Add(@"IdUser", dbType: DbType.Int32, direction: ParameterDirection.Output);
            using (var connection = _dbConnectionFactory.ConnectToDataBase())
            {               
                await connection.ExecuteAsync("UserLogin", parameters, commandType: CommandType.StoredProcedure);
                var result =  parameters.Get<int>("IdUser");
                return result;
                
            }
            
        }
        public async Task<int> RegisterUser(RegisterRequest registerRequest)
        {
            var hashedPassword = HashPassword(registerRequest.Password);
            var parameters = new DynamicParameters();
            parameters.Add(@"Email", registerRequest.Email);
            parameters.Add(@"Password", hashedPassword);
            parameters.Add(@"FirstName", registerRequest.FirstName);
            parameters.Add(@"LastName", registerRequest.LastName);
            parameters.Add(@"SafeWord", registerRequest.SafeWord);
            parameters.Add(@"IdUser", dbType: DbType.Int32, direction: ParameterDirection.Output);
            using (var connection = _dbConnectionFactory.ConnectToDataBase())
            {
                await connection.ExecuteAsync("UserRegister", parameters, commandType: CommandType.StoredProcedure);

                var result = parameters.Get<int>("IdUser");
                return result;
            }
        }
        public async Task<int> GetUserRoleById(int IdUser)
        {
            var parameters = new DynamicParameters();
            parameters.Add(@"IdUser", IdUser);
            parameters.Add(@"Role", dbType: DbType.Int32, direction: ParameterDirection.Output);
            using (var connection = _dbConnectionFactory.ConnectToDataBase())
            {
                await connection.ExecuteAsync("GetRoleByUserId", parameters, commandType: CommandType.StoredProcedure);
                var result = parameters.Get<int>("Role");
                return result;
            }
        }
        public async Task<int> UpdatePassword(UpdatePasswordRequest updatePasswordRequest)
        {
            var hashedPassword = HashPassword(updatePasswordRequest.Password);
            var parameters = new DynamicParameters();
            parameters.Add(@"IdUser", updatePasswordRequest.IdUser);
            parameters.Add(@"SafeWord", updatePasswordRequest.SafeWord);
            parameters.Add(@"NewPassword", hashedPassword);
            parameters.Add(@"StatusCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            using (var connection = _dbConnectionFactory.ConnectToDataBase())
            {
                await connection.ExecuteAsync("UpdatePassword", parameters, commandType: CommandType.StoredProcedure);
                var result = parameters.Get<int>("StatusCode");
                return result;
            }
        }
        public async Task<IEnumerable<UserDetailsDTO>> GetUserDetailsAsync()
        {
            
            using (var connection = _dbConnectionFactory.ConnectToDataBase())
            {
                var result = await connection.QueryAsync<UserDetailsDTO>("GetUserDetails", commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        private string HashPassword(string pasword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pasword));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public async Task<IEnumerable<UserTransactionResponse>> GetUserTransactions(int idUser)
        {
            var parameters = new DynamicParameters();
            parameters.Add(@"IdUser", idUser);
            using (var connection = _dbConnectionFactory.ConnectToDataBase())
            {
                var result = await connection.QueryAsync<UserTransactionResponse>("GetUserTransactionsById", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
