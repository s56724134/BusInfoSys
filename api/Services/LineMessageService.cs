using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using api.Interfaces;


namespace api.Services
{
    public class LineMessageService : ILineMessageService
    {
        private readonly HttpClient _httpClient;
        private readonly string _channelAccessToken;
        public LineMessageService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClient = httpClientFactory.CreateClient();
            _channelAccessToken = config["LineBot:ChannelAccessToken"];
        }

        public async Task PushTextMessageAsync(string userId, string message)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _channelAccessToken);

            var payload = new
            {
                to = userId,
                messages = new[]
                {
                    new { type = "text", text = message }
                }
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.line.me/v2/bot/message/push", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"LINE 推播失敗: {response.StatusCode} - {error}");
            }
        }
    }
}
