using Newtonsoft.Json;
namespace MarketAssets.Domain.Fintacharts.Models.RealTime
{
    public class SubscriptionRequest
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("instrumentId")]
        public string InstrumentId { get; set; }

        [JsonProperty("provider")]
        public string Provider { get; set; }

        [JsonProperty("subscribe")]
        public bool Subscribe { get; set; } = true;

        [JsonProperty("kinds")]
        public string[] Kinds { get; set; }
        public SubscriptionRequest(string type, string id, string instrumentId, string provider, string[] kinds)
        {
            Type = type;
            Id = id;
            InstrumentId = instrumentId;
            Provider = provider;
            Kinds = kinds;
        }
    }
}
