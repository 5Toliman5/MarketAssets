using Newtonsoft.Json;

namespace MarketAssets.Fintacharts.Domain.Models
{
    public class RealTimeAssetInfo
    {
        [JsonProperty("timestamp")]
        public DateTime DateTime { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("volume")]
        public int Volume { get; set; }
    }
}
