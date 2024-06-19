using MarketAssets.Fintacharts.Authentication.Models;
using MarketAssets.Domain.Fintacharts;
namespace MarketAssets.Fintacharts.Authentication.Abstract
{
    public interface IFintachartsAuthenticator
    {
        HttpClient HttpClient { get; }
        FintachartsRestApiCredentials Credentials { get; }
        AuthToken AccessToken { get; }
        Task AuthenticateAsync();
    }
}
