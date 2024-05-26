namespace Trading_API.Requests
{
    public class CurrencyRequest
    {
        public string CriptoName { get; set; }

        public int IdUser { get; set; }
        public string PictureLink { get; set; }
        public decimal Ratio { get; set; }
    }
}