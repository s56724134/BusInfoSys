using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using api.Dtos.LineLiff;
using api.Interfaces;


namespace api.Services
{
    public class TDXTokenService : ITDXTokenService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;
        private string TokenEndPoint = $"https://tdx.transportdata.tw/auth/realms/TDXConnect/protocol/openid-connect/token";
        // Dependent Injection on IConfiguration
        private static TDXAccessToken _cachedToken;
        private static DateTime _tokenExpiry;
        private static readonly SemaphoreSlim _semaphore = new(1, 1);
        public TDXTokenService(IConfiguration config, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<TDXAccessToken> GetTDXToken()
        {
            if (_cachedToken != null && _tokenExpiry > DateTime.UtcNow)
            return _cachedToken;

            await _semaphore.WaitAsync();
            // Para have to encoded
            var parameters = new Dictionary<string, string>()
            {
                { "grant_type", "client_credentials"},
                { "client_id", _config["TDX:ClientId"] },
                { "client_secret", _config["TDX:ClientSecret"]}
            };
            try
            {
                if (_cachedToken != null && _tokenExpiry > DateTime.UtcNow)
                    return _cachedToken;
                // FormUrlEncodedContent can convert parameters to "application/x-www-form-urlencoded"
                var formData = new FormUrlEncodedContent(parameters);
                // That PostAsync's para type is HttpContent and FormUrlEncodedContent inherit HttpContent
                var response = await _httpClient.PostAsync(TokenEndPoint, formData);
                // Console.WriteLine("TDXToken Response:" + response);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var token = JsonSerializer.Deserialize<TDXAccessToken>(json);

                _cachedToken = token;
                _tokenExpiry = DateTime.UtcNow.AddSeconds(token.expires_in - 60);

                return token;
            }
            catch (Exception e)
            {
                Console.WriteLine("❗取得 TDX Token 發生錯誤：" + e);
                return null;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
