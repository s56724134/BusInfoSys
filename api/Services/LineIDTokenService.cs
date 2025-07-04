using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using api.Interfaces;
using api.Dtos.LineLiff;

namespace api.Services
{
    public class LineIDTokenService : ILineIDTokenService
    {
        private readonly HttpClient _httpClient;
        private string _lineIDTokenEndPoint = "https://api.line.me/oauth2/v2.1/verify";
        public LineIDTokenService(HttpClient httpclient)
        {
            _httpClient = httpclient;
        }

        public async Task<AccessLineUserInfo> VerifyIDToken(LineIDTokenDto dto)
        {
            var token = dto.IdToken?.Trim('"');
            var clientId = dto.ClientId?.Trim('"');

            Console.WriteLine(token);
            Console.WriteLine(clientId);

            // Create body parameter
            var parameters = new Dictionary<string, string>()
            {
                {"id_token", token},
                {"client_id", clientId}
            };
            // var idToken  = dto.IdToken;   // string
            // var clientId = dto.ClientId;  // string

            // var parameters = new Dictionary<string, string>();
            // parameters.Add("id_token",  idToken);
            // parameters.Add("client_id", clientId);

            // Use FormUrlEncodedContent make content type to application/x-www-form-urlencoded
            var formData = new FormUrlEncodedContent(parameters);
            formData.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            try
            {
                var response = await _httpClient.PostAsync(_lineIDTokenEndPoint, formData);
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("成功回傳: " + responseContent);
                if (response.IsSuccessStatusCode)
                {
                    var data = JsonSerializer.Deserialize<AccessLineUserInfo>(
                                responseContent,
                                new JsonSerializerOptions()
                                {
                                    PropertyNameCaseInsensitive = true
                                });
                    return data;
                    // string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                    // Console.WriteLine("✅ data 內容如下：\n" + jsonString);
                }
                else
                {
                    Console.WriteLine($"錯誤碼: {(int)response.StatusCode}, 訊息: {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("網路請求失敗：" + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("發生未知錯誤：" + ex.Message);
            }

            return null;
        }
    }
}
