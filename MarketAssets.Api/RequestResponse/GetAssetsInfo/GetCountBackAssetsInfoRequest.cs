using System.ComponentModel.DataAnnotations;

namespace MarketAssets.Api.RequestResponse.GetAssetsInfo
{
    public record GetCountBackAssetsInfoRequest : BaseGetAssetsInfoRequest
    {
        [Range(1, 50, ErrorMessage = "BarsCount must be between 1 and 50")]
        public int BarsCount { get; set; }
    }
}
