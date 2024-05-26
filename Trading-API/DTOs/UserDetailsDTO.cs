namespace Trading_API.DTOs
{
    public class UserDetailsDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
        public decimal Sold { get; set; }
    }
}
