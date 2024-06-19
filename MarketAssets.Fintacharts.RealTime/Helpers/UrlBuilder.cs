namespace MarketAssets.Fintacharts.RealTime.Helpers
{
    internal static class UrlBuilder
    {
        public static string BuildUrl(string baseUrl, string token) => $"{baseUrl}/api/streaming/ws/v1/realtime?token={token}";
    }
}
