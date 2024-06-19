namespace MarketAssets.Fintacharts.Historical.Helpers
{
    internal static class UrlBuilder
    {
        public static string BuildProvidersUrl(string baseUrl) => $"{baseUrl}/api/instruments/v1/providers";
    }
}
