using System;
using System.Drawing;

namespace MarketAssets.Fintacharts.Historical.Helpers
{
    internal class CountBackUrlBuilder
    {
        private readonly string baseUrl;
        private string instrumentId;
        private string provider;
        private int? interval;
        private string periodicity;
        private int? barsCount;
        public CountBackUrlBuilder(string baseUrl)
        {
            this.baseUrl = baseUrl; 
        }
        public CountBackUrlBuilder SetInstrumentId(string instrumentId)
        {
            this.instrumentId = instrumentId;
            return this;
        }

        public CountBackUrlBuilder SetProvider(string provider)
        {
            this.provider = provider;
            return this;
        }

        public CountBackUrlBuilder SetInterval(int interval)
        {
            this.interval = interval;
            return this;
        }

        public CountBackUrlBuilder SetPeriodicity(string periodicity)
        {
            this.periodicity = periodicity;
            return this;
        }

        public CountBackUrlBuilder SetBarsCount(int barsCount)
        {
            this.barsCount = barsCount;
            return this;
        }

        public string Build()
        {
            var url = baseUrl + "/api/bars/v1/bars/count-back";

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
            if (barsCount.HasValue)
            {
                queryParams.Add($"barsCount={barsCount}");
            }
            if (queryParams.Any())
            {
                url += "?" + string.Join("&", queryParams);
            }
            return url;
        }
    }
}
