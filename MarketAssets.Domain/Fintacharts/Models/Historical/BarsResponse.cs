using MarketAssets.Domain.Models;
using MarketAssets.Fintacharts.Historical.Models;
using Newtonsoft.Json;
namespace MarketAssets.Domain.Fintacharts.Models.Historical
{
    public class BarsResponse
    {
        [JsonProperty("data")]
        public List<BarData> Bars { get; set; }
    }
    public class InstrumentsResponse
    {
        [JsonProperty("data")]
        public List<FintachartsInstrument> Instruments { get; set; }
    }
    public class ProvidersResponse
    {
        [JsonProperty("data")]
        public List<string> Providers { get; set; }
    }
}
