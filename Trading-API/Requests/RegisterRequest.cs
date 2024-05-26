namespace Trading_API.Requests
{
    public class RegisterRequest
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SafeWord { get; set; }
    }
}
