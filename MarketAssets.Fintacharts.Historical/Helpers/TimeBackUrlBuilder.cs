using System;
using System.Drawing;

namespace MarketAssets.Fintacharts.Historical.Helpers
{
    internal class TimeBackUrlBuilder
    {
        private readonly string baseUrl;
        private string instrumentId;
        private string provider;
        private int? interval;
        private string periodicity;
        private string timeBack;
        public TimeBackUrlBuilder(string baseUrl)
        {
            this.baseUrl = baseUrl; 
        }
        public TimeBackUrlBuilder SetInstrumentId(string instrumentId)
        {
            this.instrumentId = instrumentId;
            return this;
        }

        public TimeBackUrlBuilder SetProvider(string provider)
        {
            this.provider = provider;
            return this;
        }

        public TimeBackUrlBuilder SetInterval(int interval)
        {
            this.interval = interval;
            return this;
        }

        public TimeBackUrlBuilder SetPeriodicity(string periodicity)
        {
            this.periodicity = periodicity;
            return this;
        }

        public TimeBackUrlBuilder SetTimeBack(string timeBack)
        {
            this.timeBack = timeBack;
            return this;
        }

        public string Build()
        {
            var url = baseUrl + "/api/data-consolidators/bars/v1/bars/time-back";

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
            if (!string.IsNullOrEmpty(timeBack))
            {
                queryParams.Add($"timeBack={timeBack}");
            }
            if (queryParams.Any())
            {
                url += "?" + string.Join("&", queryParams);
            }
            return url;
        }
    }
}
