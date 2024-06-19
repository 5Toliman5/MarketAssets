namespace MarketAssets.Domain.Models
{
    public class MarketAsset
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; }
        public string? Description { get; set; }
        public string? Kind { get; set; }
        public string? Currency { get; set; }
        public string? BaseCurrency { get; set; }
    }
}
