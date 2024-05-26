namespace Trading_API.Requests
{
    public class UpdatePasswordRequest
    {
        public int IdUser { get; set; }

        public string SafeWord { get; set; }
        public string Password { get; set; }
    }
    
}
