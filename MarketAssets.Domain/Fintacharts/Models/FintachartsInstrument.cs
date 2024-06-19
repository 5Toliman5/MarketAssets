namespace MarketAssets.Fintacharts.Historical.Models
{
    public class FintachartsInstrument
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Kind { get; set; }
        public string Exchange { get; set; }
        public string Description { get; set; }
        public decimal TickSize { get; set; }
        public string Currency { get; set; }
        public string BaseCurrency { get; set; }
        public Dictionary<string, FintachartsMapping> Mappings { get; set; }
    }
    public class FintachartsMapping
    {
        public string Symbol { get; set; }
        public string Exchange { get; set; }
        public int DefaultOrderSize { get; set; }
    }
}
