using MarketAssets.Domain.Fintacharts.Models.RealTime;
using MarketAssets.Fintacharts.Domain.Models;


namespace MarketAssets.Fintacharts.RealTime.Services
{
    public interface IFintachartsRealTimeService
    {
        Task<IEnumerable<RealTimeAssetInfo>> GetRealTimeData(SubscriptionRequest request);
    }
}