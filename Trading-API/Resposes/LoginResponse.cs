namespace Trading_API.Resposes
{
    public class LoginResponse
    {
        public int IdUser { get; set; }
        public int Role { get; set; }
        public bool isAuth()
        {
            if (IdUser != 0 && Role != 0)
            {
                return true;
            }
            return false;
        }

    }
}
