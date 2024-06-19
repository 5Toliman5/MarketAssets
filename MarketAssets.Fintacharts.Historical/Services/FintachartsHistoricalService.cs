using MarketAssets.Domain.Exceptions;
using MarketAssets.Domain.Fintacharts.Models.Historical;
using MarketAssets.Domain.Models;
using MarketAssets.Fintacharts.Authentication.Abstract;
using MarketAssets.Fintacharts.Historical.Helpers;
using MarketAssets.Fintacharts.Historical.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MarketAssets.Fintacharts.Historical.Services
{
    public class FintachartsHistoricalService : IFintachartsHistoricalService
    {
        private readonly ILogger<FintachartsHistoricalService> Logger;
        private readonly IFintachartsAuthenticator Authenticator;

        public FintachartsHistoricalService(ILogger<FintachartsHistoricalService> logger, IFintachartsAuthenticator authenticator)
        {
            this.Logger = logger;
            this.Authenticator = authenticator;
        }
        private async Task<string> CallHttpGet(string url)
        {
            var response = await this.Authenticator.HttpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync(); ;
        }
        public async Task<IEnumerable<string>> GetProvidersAsync()
        {
            this.Logger.LogInformation("Start GetProvidersAsync");
            await this.Authenticator.AuthenticateAsync();
            var url = UrlBuilder.BuildProvidersUrl(this.Authenticator.Credentials.ApiUrl);
            var response = await this.CallHttpGet(url);
            var data = JsonConvert.DeserializeObject<ProvidersResponse>(response);
            if (data is null)
            {
                this.Logger.LogError("No data has been received");
                throw new NotFoundException("The requested data has not been found.");
            }
            return data.Providers;
        }
        public async Task<IEnumerable<FintachartsInstrument>> GetInstrumentsAsync(string? symbol = null, int? page = null, int? size = null, string? provider = null, string? kind = null)
        {
            this.Logger.LogInformation("Start GetInstrumentsAsync");
            await this.Authenticator.AuthenticateAsync();
            var urlBuilder = new GetInstrumentsUrlBuilder(this.Authenticator.Credentials.ApiUrl);
            var url = urlBuilder
                .SetSymbol(symbol)
                .SetProvider(provider)
                .SetPage(page)
                .SetSize(size)
                .SetKind(kind)
                .Build();
            var response = await this.CallHttpGet(url);
            var data = JsonConvert.DeserializeObject<InstrumentsResponse>(response);
            if (data is null)
            {
                this.Logger.LogError("No data has been received");
                throw new NotFoundException("The requested data has not been found.");
            }
            return data.Instruments;

        }
        public async Task<IEnumerable<BarData>> GetCountBackDataAsync(string instrumentId, string provider, int interval, string periodicity, int barsCount)
        {
            this.Logger.LogInformation("Start GetCountBackDataAsync");
            await this.Authenticator.AuthenticateAsync();
            var urlBuilder = new CountBackUrlBuilder(this.Authenticator.Credentials.ApiUrl);
            var url = urlBuilder
                .SetInstrumentId(instrumentId)
                .SetProvider(provider)
                .SetInterval(interval)
                .SetPeriodicity(periodicity)
                .SetBarsCount(barsCount)
                .Build();
            var response = await this.CallHttpGet(url);
            var data = JsonConvert.DeserializeObject<BarsResponse>(response);
            if (data is null)
            {
                this.Logger.LogError("No data has been received");
                throw new NotFoundException("The requested data has not been found.");
            }
            return data.Bars;
        }
        public async Task<IEnumerable<BarData>> GetDateRangeDataAsync(string instrumentId, string provider, int interval, string periodicity, string startDate, string? endDate = null)
        {
            this.Logger.LogInformation("Start GetDateRangeDataAsync");
            await this.Authenticator.AuthenticateAsync();
            var urlBuilder = new DateRangeUrlBuilder(this.Authenticator.Credentials.ApiUrl);
            var url = urlBuilder
                .SetInstrumentId(instrumentId)
                .SetProvider(provider)
                .SetInterval(interval)
                .SetPeriodicity(periodicity)
                .SetStartDate(startDate)
                .SetEndDate(endDate)
                .Build();
            var response = await this.CallHttpGet(url);
            var data = JsonConvert.DeserializeObject<BarsResponse>(response);
            if (data is null)
            {
                this.Logger.LogError("No data has been received");
                throw new NotFoundException("The requested data has not been found.");
            }
            return data.Bars;
        }
        public async Task<string> GetTimeBackDataAsync(string instrumentId, string provider, int interval, string periodicity, string timeBack)
        {
            this.Logger.LogInformation("Start GetTimeBackDataAsync");
            await this.Authenticator.AuthenticateAsync();
            var urlBuilder = new TimeBackUrlBuilder(this.Authenticator.Credentials.ApiUrl);
            var url = urlBuilder
                .SetInstrumentId(instrumentId)
                .SetProvider(provider)
                .SetInterval(interval)
                .SetPeriodicity(periodicity)
                .SetTimeBack(timeBack)
                .Build();
            var response = await this.CallHttpGet(url);
            if (response is null)
            {
                this.Logger.LogError("No data has been received");
                throw new NotFoundException("The requested data has not been found.");
            }
            return response;
        }
    }
}
