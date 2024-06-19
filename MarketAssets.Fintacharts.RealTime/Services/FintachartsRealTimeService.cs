using MarketAssets.Fintacharts.Authentication.Abstract;
using MarketAssets.Fintacharts.Domain.Models;
using MarketAssets.Fintacharts.RealTime.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;
using MarketAssets.Domain.Exceptions;
using MarketAssets.Domain.Fintacharts.Models.RealTime;

namespace MarketAssets.Fintacharts.RealTime.Services
{
    public class FintachartsRealTimeService : IFintachartsRealTimeService
    {
        private readonly ILogger<FintachartsRealTimeService> Logger;
        private readonly IFintachartsAuthenticator Authenticator;
        private readonly FintachartsWSApiCredentials Credentials;

        public FintachartsRealTimeService(ILogger<FintachartsRealTimeService> logger, IOptions<FintachartsWSApiCredentials> credentials, IFintachartsAuthenticator authenticator)
        {
            this.Credentials = credentials.Value;
            this.Logger = logger;
            this.Authenticator = authenticator;
        }
        public async Task<IEnumerable<RealTimeAssetInfo>> GetRealTimeData(SubscriptionRequest request)
        {
            using (var ws = new ClientWebSocket())
            {
                await this.AuthenticateAsync(ws);
                await this.ListenForSession(ws);
                var json = JsonConvert.SerializeObject(request);
                var subscriptionBytes = Encoding.UTF8.GetBytes(json);
                await ws.SendAsync(new ArraySegment<byte>(subscriptionBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                var response = await this.ListenForData(ws);
                if (string.IsNullOrEmpty(response))
                    throw new NotFoundException("The requested data has not been found.");
                return PesponseDataParser.Parse(response, request.Kinds);
            }

        }
        private async Task AuthenticateAsync(ClientWebSocket ws)
        {
            await this.Authenticator.AuthenticateAsync();
            var url = UrlBuilder.BuildUrl(this.Credentials.BaseApiUrl, this.Authenticator.AccessToken.Value);
            await ws.ConnectAsync(new Uri(url), CancellationToken.None);
        }
        private async Task ListenForSession(ClientWebSocket ws)
        {
            var buffer = new byte[this.Credentials.SessionResponseBufferSize];
            var iterator = 0;
            bool success = false;
            while (ws.State == WebSocketState.Open && iterator < this.Credentials.ListenForSessionIterationsLimit)
            {
                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                if (message.Contains("\"session\""))
                {
                    success = true;
                    break;
                }
            }
            if (!success)
            {
                if (ws.State == WebSocketState.Open)
                {
                    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "No session message received within the iteration limit.", CancellationToken.None);
                }
                throw new TimeoutException("Session has not been established within the iteration limit");
            }

        }
        private async Task<string> ListenForData(ClientWebSocket ws)
        {
            var buffer = new byte[this.Credentials.DataResponseBufferSize];
            var iterator = 0;
            while (ws.State == WebSocketState.Open && iterator < this.Credentials.ListenForDataIterationsLimit)
            {
                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    if (message.Contains("\"instrumentId\""))
                    {
                        await ws.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Received necessary data", CancellationToken.None);
                        return message;
                    }
                }
                iterator++;
            }
            throw new TimeoutException("No valid data has been received within the iteration limit");
        }
    }
}
