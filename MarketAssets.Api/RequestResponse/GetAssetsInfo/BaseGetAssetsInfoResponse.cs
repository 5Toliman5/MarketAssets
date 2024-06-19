using MarketAssets.Fintacharts.Domain.Models;

namespace MarketAssets.Api.RequestResponse.GetAssetsInfo
{
    public abstract class BaseGetAssetsInfoResponse
    {
        public IEnumerable<RealTimeAssetInfo>? RealTimeInfo { get; set; }
    }
}
