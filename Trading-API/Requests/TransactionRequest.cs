namespace Trading_API.Requests
{
    public class TransactionRequest
    {
        public int IdCripto { get; set; }
        public int IdUser { get; set; }
        public string Date { get; set; }
        public decimal SumaLei { get; set; }
        public string TransactionType { get; set; }
    }
}

