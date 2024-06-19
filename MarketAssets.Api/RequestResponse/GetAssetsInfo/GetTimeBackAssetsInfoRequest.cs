using System.ComponentModel.DataAnnotations;

namespace MarketAssets.Api.RequestResponse.GetAssetsInfo
{
    public record GetTimeBackAssetsInfoRequest : BaseGetAssetsInfoRequest
    {
        [Timestamp]
        public string TimeBack { get; set; }
    }
}
