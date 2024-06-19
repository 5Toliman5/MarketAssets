namespace MarketAssets.Fintacharts.Authentication.Helpers
{
    internal class GetInstrumentsUrlBuilder
    {
        private readonly string baseUrl;
        private string provider;
        private string kind;
        private string symbol;
        private int? page;
        private int? size;

        public GetInstrumentsUrlBuilder(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public GetInstrumentsUrlBuilder WithProvider(string provider)
        {
            this.provider = provider;
            return this;
        }

        public GetInstrumentsUrlBuilder WithKind(string kind)
        {
            this.kind = kind;
            return this;
        }

        public GetInstrumentsUrlBuilder WithSymbol(string symbol)
        {
            this.symbol = symbol;
            return this;
        }

        public GetInstrumentsUrlBuilder WithPage(int page)
        {
            this.page = page;
            return this;
        }

        public GetInstrumentsUrlBuilder WithSize(int size)
        {
            this.size = size;
            return this;
        }

        public string Build()
        {
            var url = baseUrl + "/api/instruments/v1/instruments";

            var queryParams = new List<string>();
            if (!string.IsNullOrEmpty(provider))
            {
                queryParams.Add($"provider={provider}");
            }
            if (!string.IsNullOrEmpty(kind))
            {
                queryParams.Add($"kind={kind}");
            }
            if (!string.IsNullOrEmpty(symbol))
            {
                queryParams.Add($"symbol={symbol}");
            }
            if (page.HasValue)
            {
                queryParams.Add($"page={page.Value}");
            }
            if (size.HasValue)
            {
                queryParams.Add($"size={size.Value}");
            }

            if (queryParams.Any())
            {
                url += "?" + string.Join("&", queryParams);
            }

            return url;
        }
    }
}
