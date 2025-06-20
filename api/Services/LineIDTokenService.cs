using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            // Create body parameter
            var parameters = new Dictionary<string, string>()
            {
                {"id_token", dto.IdToken},
                {"client_id", dto.ClientId}
            };
            // Use FormUrlEncodedContent make content type to application/x-www-form-urlencoded
            var formData = new FormUrlEncodedContent(parameters);
            // get response
            Console.WriteLine(formData);

            try
            {
                var response = await _httpClient.PostAsync(_lineIDTokenEndPoint, formData);
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("成功回傳: " + responseContent);
                if (response.IsSuccessStatusCode)
                {
                    // var responseContent = await response.Content.ReadAsStringAsync();
                    // Console.WriteLine("成功回傳: " + responseContent);
                    // ✅ 可進一步做 JSON 解析
                    var data = JsonSerializer.Deserialize<AccessLineUserInfo>(responseContent);
                    return data;
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
