namespace MarketAssets.Fintacharts.Authentication.Helpers
{
    internal static class AuthUrlBuilder
    {
        public static string BuildAuthenticationUrl(string baseUrl) => $"{baseUrl}/identity/realms/fintatech/protocol/openid-connect/token";
    }
}
