using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MarketAssets.Fintacharts.Domain.Models;

namespace MarketAssets.Fintacharts.RealTime.Helpers
{
    internal static class PesponseDataParser
    {
        public static IEnumerable<RealTimeAssetInfo> Parse(string responseData, string[] keys)
        {
            List<RealTimeAssetInfo> data = new();
            var jsonObject = JsonConvert.DeserializeObject<JObject>(responseData);
            foreach (var kind in keys)
            {
                var kindObject = jsonObject[kind];
                if (kindObject != null)
                {
                    var assetInfo = kindObject.ToObject<RealTimeAssetInfo>();
                    data.Add(assetInfo);
                }
            }
            return data;
        }
    }
}
