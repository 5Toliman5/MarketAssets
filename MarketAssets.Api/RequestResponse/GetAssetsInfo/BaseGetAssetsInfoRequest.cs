using MarketAssets.Api.Validation;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace MarketAssets.Api.RequestResponse.GetAssetsInfo
{
    public abstract record BaseGetAssetsInfoRequest
    {
        [Required(ErrorMessage = "RequestId is required")]
        public string RequestId { get; set; }
        [Required(ErrorMessage = "InstrumentId is required")]
        public string InstrumentId { get; set; }
        [Required(ErrorMessage = "Provider is required")]
        public string Provider { get; set; }
        [Range(1, 10, ErrorMessage = "Interval must be between 1 and 10")]
        public int Interval { get; set; }
        [Required(ErrorMessage = "Periodicity is required")]
        public string Periodicity { get; set; }
        public bool IncludeRealTime { get; set; }
        [IncludeIfAnotherPropertyIsTrue(nameof(IncludeRealTime))]
        public string[]? Kinds { get; set; }
    }
}
