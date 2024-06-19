
namespace MarketAssets.Fintacharts.Historical.Helpers
{
    internal class DateRangeUrlBuilder
    {
        private readonly string baseUrl;
        private string instrumentId;
        private string provider;
        private int? interval;
        private string periodicity;
        private string? startDate;
        private string? endDate;
        public DateRangeUrlBuilder(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }
        public DateRangeUrlBuilder SetInstrumentId(string? instrumentId)
        {
            this.instrumentId = instrumentId;
            return this;
        }

        public DateRangeUrlBuilder SetProvider(string? provider)
        {
            this.provider = provider;
            return this;
        }

        public DateRangeUrlBuilder SetInterval(int? interval)
        {
            this.interval = interval;
            return this;
        }

        public DateRangeUrlBuilder SetPeriodicity(string? periodicity)
        {
            this.periodicity = periodicity;
            return this;
        }

        public DateRangeUrlBuilder SetStartDate(string? startDate)
        {
            this.startDate = startDate;
            return this;
        }
        public DateRangeUrlBuilder SetEndDate(string? endDate)
        {
            this.endDate = endDate;
            return this;
        }
        public string Build()
        {
            var url = baseUrl + "/api/bars/v1/bars/date-range";

            var queryParams = new List<string>();
            if (!string.IsNullOrEmpty(instrumentId))
            {
                queryParams.Add($"instrumentId={instrumentId}");
            }
            if (!string.IsNullOrEmpty(periodicity))
            {
                queryParams.Add($"periodicity={periodicity}");
            }
            if (!string.IsNullOrEmpty(provider))
            {
                queryParams.Add($"provider={provider}");
            }
            if (interval.HasValue)
            {
                queryParams.Add($"interval={interval}");
            }
            if (!string.IsNullOrEmpty(startDate))
            {
                queryParams.Add($"startDate={startDate}");
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                queryParams.Add($"endDate={endDate}");
            }
            if (queryParams.Any())
            {
                url += "?" + string.Join("&", queryParams);
            }
            return url;
        }
    }
}
