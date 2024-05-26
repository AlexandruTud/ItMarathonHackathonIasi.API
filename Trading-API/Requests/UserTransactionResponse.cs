namespace Trading_API.Requests
{
    public class UserTransactionResponse
    {
        public int IdTransaction { get; set; }
        public string CryptocurrencyName { get; set; }  
        public int IdCripto { get; set; }
        public int IdUser { get; set; }
        public string Date { get; set; }
        public decimal SumaLei { get; set; }
        public decimal Impozit { get; set; }
        public string TransactionType { get; set; }
        public decimal Quantity { get; set; }

    }
}
