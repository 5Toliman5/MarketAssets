using MarketAssets.Api.RequestResponse.GetAssetsInfo;

namespace MarketAssets.Api.RequestResponse
{
    public class GetAssetsInfoResponse<T> where T : BaseGetAssetsInfoResponse
    {
        public List<T> Items { get; set; }
        public GetAssetsInfoResponse()
        {
            this.Items = new();
        }
    }
}
