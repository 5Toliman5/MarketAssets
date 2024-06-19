namespace MarketAssets.Domain.Fintacharts.Models.RealTime
{
    public record FintachartsWSApiCredentials
    {
        public string BaseApiUrl { get; init; }
        public int SessionResponseBufferSize { get; init; }
        public int DataResponseBufferSize { get; init; }
        public int ListenForSessionIterationsLimit { get; init; }
        public int ListenForDataIterationsLimit { get; init; }
    }
}
