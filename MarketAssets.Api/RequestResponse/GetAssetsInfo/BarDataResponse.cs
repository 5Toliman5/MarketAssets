using MarketAssets.Domain.Models;

namespace MarketAssets.Api.RequestResponse.GetAssetsInfo
{
    public class BarDataResponse : BaseGetAssetsInfoResponse
    {
        public IEnumerable<BarData> Bars { get; set; }
    }
}
