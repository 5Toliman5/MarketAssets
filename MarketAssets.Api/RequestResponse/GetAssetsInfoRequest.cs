using MarketAssets.Api.RequestResponse.GetAssetsInfo;

namespace MarketAssets.Api.RequestResponse
{
    public record GetAssetsInfoRequest<T> where T : BaseGetAssetsInfoRequest
    {
        public IEnumerable<T> Items { get; set; }
    }

}
