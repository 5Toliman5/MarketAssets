using AutoMapper;
using MarketAssets.Api.RequestResponse;
using MarketAssets.Api.RequestResponse.GetAssetsInfo;
using MarketAssets.Domain.Fintacharts.Models.RealTime;
using MarketAssets.Domain.Models;
using MarketAssets.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using MarketAssets.Fintacharts.Historical.Services;
using MarketAssets.Fintacharts.RealTime.Services;
namespace MarketAssets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsInfoController : ControllerBase
    {
        private readonly ILogger<AssetsInfoController> Logger;
        private readonly IConfiguration Configuration;
        private readonly IFintachartsRealTimeService RealTimeService;
        private readonly IFintachartsHistoricalService HistoricalService;
        private readonly IDataAccessRepository<MarketAsset> AssetsRepository;
        private readonly IDataAccessRepository<AssetProvider> ProvidersRepository;
        private readonly IMapper Mapper;

        public AssetsInfoController(ILogger<AssetsInfoController> logger, 
            IConfiguration configuration,
            IFintachartsRealTimeService realTimePricesService, 
            IFintachartsHistoricalService historicalPricesService, 
            IDataAccessRepository<MarketAsset> assetsRepository,
            IDataAccessRepository<AssetProvider> providersRepository,
            IMapper mapper)
        {
            this.Logger = logger;
            this.Configuration = configuration;
            this.RealTimeService = realTimePricesService;
            this.HistoricalService = historicalPricesService;
            this.AssetsRepository = assetsRepository;
            this.ProvidersRepository = providersRepository;
            this.Mapper = mapper;
        }
        [HttpGet("providers")]
        public async Task<IEnumerable<string>> GetAvailableProviders()
        {
            this.Logger.LogInformation("Executing GetAvailableProviders.");
            var providers = await this.ProvidersRepository.GetAllAsync();
            if (providers is null || !providers.Any())
            {
                this.Logger.LogInformation("No providers data in the DB, fetching from the remote service.");
                var serviceProviders = await this.HistoricalService.GetProvidersAsync();
                var domainProviders = serviceProviders.Select(x => new AssetProvider(x));
                await this.ProvidersRepository.InsertAsync(domainProviders);
                return serviceProviders;
            }
            else
                return providers.Select(x => x.Name);
        }
        [HttpGet("assets")]
        public async Task<IEnumerable<string>> GetAvailableAssets()
        {
            this.Logger.LogInformation("Executing GetAvailableAssets.");
            var dbAssets = await this.AssetsRepository.GetAllAsync();
            if (dbAssets is null || !dbAssets.Any()) 
            {
                this.Logger.LogInformation("No assets data in the DB, fetching from the remote service.");
                var serviceAssets = await this.HistoricalService.GetInstrumentsAsync();
                var domainAssets = this.Mapper.Map<IEnumerable<MarketAsset>>(serviceAssets);
                await this.AssetsRepository.InsertAsync(domainAssets);
                return serviceAssets.Select(x => x.Symbol);
            }
            else
                return dbAssets.Select(x => x.Symbol);
        }
        [HttpPost("assetsTimeBack")]
        public async Task<GetAssetsInfoResponse<HtmlResponse>> GetAssetsTimeBackInfo(GetAssetsInfoRequest<GetTimeBackAssetsInfoRequest> request)
        {
            this.Logger.LogInformation("Executing GetAssetsTimeBackInfo.");
            GetAssetsInfoResponse<HtmlResponse> response = new();
            foreach (var requestItem in request.Items) 
            {
                var responseItem = new HtmlResponse();
                var historicalData = await this.HistoricalService.GetTimeBackDataAsync(requestItem.InstrumentId, requestItem.Provider, requestItem.Interval, requestItem.Periodicity, requestItem.TimeBack);
                responseItem.Html = historicalData;
                if (requestItem.IncludeRealTime)
                {
                    responseItem.RealTimeInfo = await this.GetRealTimeInfo(requestItem);
                }
                response.Items.Add(responseItem);
            }
            return response;

        }
        [HttpPost("assetsCountBack")]
        public async Task<GetAssetsInfoResponse<BarDataResponse>> GetAssetsCountBackInfo(GetAssetsInfoRequest<GetCountBackAssetsInfoRequest> request)
        {
            this.Logger.LogInformation("Executing GetAssetsCountBackInfo.");
            GetAssetsInfoResponse<BarDataResponse> response = new();
            foreach (var requestItem in request.Items)
            {
                var responseItem = new BarDataResponse();
                var historicalData = await this.HistoricalService.GetCountBackDataAsync(requestItem.InstrumentId, requestItem.Provider, requestItem.Interval, requestItem.Periodicity, requestItem.BarsCount);
                responseItem.Bars = historicalData;
                if (requestItem.IncludeRealTime)
                {
                    responseItem.RealTimeInfo = await this.GetRealTimeInfo(requestItem);
                }
                response.Items.Add(responseItem);
            }
            return response;

        }
        [HttpPost("assetsDateRange")]
        public async Task<GetAssetsInfoResponse<BarDataResponse>> GetAssetsDateRangeInfo(GetAssetsInfoRequest<GetDateRangeAssetsInfoRequest> request)
        {
            this.Logger.LogInformation("Executing GetAssetsDateRangeInfo.");
            GetAssetsInfoResponse<BarDataResponse> response = new();
            foreach (var requestItem in request.Items)
            {
                var responseItem = new BarDataResponse();
                var historicalData = await this.HistoricalService.GetDateRangeDataAsync(requestItem.InstrumentId, requestItem.Provider, requestItem.Interval, requestItem.Periodicity, requestItem.StartDate, requestItem.EndDate);
                responseItem.Bars = historicalData;
                if (requestItem.IncludeRealTime)
                {
                    responseItem.RealTimeInfo = await this.GetRealTimeInfo(requestItem);
                }
                response.Items.Add(responseItem);
            }
            return response;

        }
        private async Task<IEnumerable<Fintacharts.Domain.Models.RealTimeAssetInfo>> GetRealTimeInfo(BaseGetAssetsInfoRequest request)
        {
            var type = this.Configuration.GetValue<string>("Fintacharts:SubscribeRequestType");
            var getRealTimeInfoRequest = new SubscriptionRequest(type, request.RequestId, request.InstrumentId, request.Provider, request.Kinds);
            return await this.RealTimeService.GetRealTimeData(getRealTimeInfoRequest);
        }
    }
}
