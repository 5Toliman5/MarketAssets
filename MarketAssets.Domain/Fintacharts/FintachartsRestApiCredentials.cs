namespace MarketAssets.Domain.Fintacharts
{
    public record FintachartsRestApiCredentials
    {
        public string ApiUrl { get; init; }
        public string UserName { get; init; }
        public string Password { get; init; }
        public string ClientId { get; init; }

    }
}
