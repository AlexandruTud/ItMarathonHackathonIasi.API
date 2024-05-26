namespace Trading_API.Resposes
{
    public class CurrencyResponse
    {
        public int IdCripto { get; set; }   
        public string CriptoName { get; set; }  
        public string UserName { get; set; }
        public int IdUser { get; set; }
        public float Price { get; set; }
        public string PictureLink { get; set; }
    }
}