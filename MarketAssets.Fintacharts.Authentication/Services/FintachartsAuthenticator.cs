using MarketAssets.Fintacharts.Authentication.Abstract;
using MarketAssets.Fintacharts.Authentication.Helpers;
using MarketAssets.Fintacharts.Authentication.Models;
using MarketAssets.Domain.Fintacharts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;


namespace MarketAssets.Fintacharts.Authentication.Services
{
    public class FintachartsAuthenticator : IFintachartsAuthenticator
    {
        private readonly ILogger<FintachartsAuthenticator> Logger;
        public HttpClient HttpClient { get; private set; }
        public FintachartsRestApiCredentials Credentials { get; private set; }
        public AuthToken AccessToken { get; private set; }
        private AuthToken RefreshToken;
        public FintachartsAuthenticator(ILogger<FintachartsAuthenticator> logger, HttpClient httpClient, IOptions<FintachartsRestApiCredentials> credentials)
        {
            this.Logger = logger;
            this.HttpClient = httpClient;
            this.Credentials = credentials.Value;
        }
        public async Task AuthenticateAsync()
        {
            this.Logger.LogInformation("Started authentication");
            if (AccessToken is not null && AccessToken.IsValid)
                return;
            TokenResponse tokens = null;
            if (AccessToken is null || !RefreshToken.IsValid)
                tokens = await GetAuthTokens(ApiCallType.GetNewAccessToken);
            else
                tokens = await GetAuthTokens(ApiCallType.RefreshAccessToken);
            if (tokens is null)
                throw new InvalidOperationException("Failed to retrieve authentication tokens.");
            this.AccessToken = new(tokens.AccessToken, tokens.ExpiresIn);
            this.RefreshToken = new(tokens.RefreshToken, tokens.RefreshExpiresIn);
            this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokens.TokenType, AccessToken.Value);
        }

        private async Task<TokenResponse> GetAuthTokens(ApiCallType callType)
        {
            var url = AuthUrlBuilder.BuildAuthenticationUrl(Credentials.ApiUrl);
            var content = CreateRequestContent(callType);
            var httpResponse = await CallHttpPost(url, content);
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TokenResponse>(responseContent);
        }
        private async Task<HttpResponseMessage> CallHttpPost(string url, FormUrlEncodedContent content)
        {
            var response = await HttpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return response;
        }
        private FormUrlEncodedContent CreateRequestContent(ApiCallType callType)
        {
            return callType switch
            {
                ApiCallType.GetNewAccessToken => new FormUrlEncodedContent(new[]
                        {
                          new KeyValuePair<string, string>("grant_type", "password"),
                          new KeyValuePair<string, string>("client_id", Credentials.ClientId),
                          new KeyValuePair<string, string>("username", Credentials.UserName),
                          new KeyValuePair<string, string>("password", Credentials.Password)
                        }),
                ApiCallType.RefreshAccessToken => new FormUrlEncodedContent(new[]
                      {
                          new KeyValuePair<string, string>("grant_type", "refresh_token"),
                          new KeyValuePair<string, string>("client_id", Credentials.ClientId),
                          new KeyValuePair<string, string>("refresh_token", RefreshToken.Value)
                      }),
                _ => throw new ArgumentException(nameof(callType))
            };
        }
    }
}
