using MarketAssets.Api.Validation;
using System.Security.Principal;

namespace MarketAssets.Api.RequestResponse.GetAssetsInfo
{
    public record GetDateRangeAssetsInfoRequest : BaseGetAssetsInfoRequest
    {
        [DateFormat]
        public string StartDate { get; set; }
        [DateFormat]
        public string? EndDate { get; set; }
    }
}
