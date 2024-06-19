using MarketAssets.Domain.Models;
using MarketAssets.Fintacharts.Historical.Models;

namespace MarketAssets.Fintacharts.Historical.Services
{
    public interface IFintachartsHistoricalService
    {
        Task<IEnumerable<BarData>> GetCountBackDataAsync(string instrumentId, string provider, int interval, string periodicity, int barsCount);
        Task<IEnumerable<BarData>> GetDateRangeDataAsync(string instrumentId, string provider, int interval, string periodicity, string startDate, string? endDate = null);
        Task<IEnumerable<FintachartsInstrument>> GetInstrumentsAsync(string? symbol = null, int? page = null, int? size = null, string? provider = null, string? kind = null);
        Task<IEnumerable<string>> GetProvidersAsync();
        Task<string> GetTimeBackDataAsync(string instrumentId, string provider, int interval, string periodicity, string timeBack);
    }
}