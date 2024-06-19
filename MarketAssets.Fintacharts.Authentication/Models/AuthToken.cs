namespace MarketAssets.Fintacharts.Authentication.Models
{
    public class AuthToken
    {
        public string Value { get; set; }
        public bool IsValid => DateTime.UtcNow < ExpirationTime;
        private DateTime ExpirationTime;
        public AuthToken(string value, int expiresInSeconds)
        {
            Value = value;
            ExpirationTime = DateTime.UtcNow.AddSeconds(expiresInSeconds);
        }
    }
}
