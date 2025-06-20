using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using api.Interfaces;
using api.Models.TDXApi;
using api.Mapper;


namespace api.Services
{
    public class BusInfoService : IBusInfoService
    {
        private readonly HttpClient _httpClient;
        private readonly ITDXTokenService _tokenService;
        public BusInfoService(HttpClient httpClient, ITDXTokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }

        // That Frontend could fetch api get busroute data that para is string
        public async Task<BusRoute[]> GetBusRoute(string routeName)
        {
            try
            {
                // Console.WriteLine("Start GetBusRoute: " + routeName);
                //Fetch Route api and get the data
                // Every time Put Token in request
                // Get token from TDXTokenService
                var tokenObj = await _tokenService.GetTDXToken();
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://tdx.transportdata.tw/api/basic/v2/Bus/Route/City/Taichung/{routeName}?%24top=30&%24format=JSON");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenObj.access_token);
                var result = await _httpClient.SendAsync(request);
                // Console.WriteLine("GetBusRoute Response:" + result.StatusCode);
                if (result.IsSuccessStatusCode)
                {
                    //if the response is true
                    string content = await result.Content.ReadAsStringAsync();
                    // Console.WriteLine("TDX Response: " + content);
                    //Handle the data and desealrize to object
                    var busRoutes = JsonSerializer.Deserialize<BusRoute[]>(content);
                    if (busRoutes != null)
                    {
                        return busRoutes;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Deserialize Exception: " + ex);
                return null;
            }
        }
        public async Task<DisplayStopOfRoute[]> GetBusStopOfRoute(string routeName)
        {
            try
            {
                var tokenObj = await _tokenService.GetTDXToken();
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://tdx.transportdata.tw/api/basic/v2/Bus/DisplayStopOfRoute/City/Taichung/{routeName}?%24top=30&%24format=JSON");
                // Set access_token in request header
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenObj.access_token);
                // Get httpResponse
                var response = await _httpClient.SendAsync(request);
                // Console.WriteLine("GetBusStopOfRoute" + response);
                // check the statuscode
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    DisplayStopOfRoute[] busStopOfRoute = JsonSerializer.Deserialize<DisplayStopOfRoute[]>(content);
                    if (busStopOfRoute != null)
                    {
                        return busStopOfRoute;
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<DailyStopTimeTable[]> GetDailyStopTimeTable(string routeName)
        {
            try
            {
                var tokenObj = await _tokenService.GetTDXToken();
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://tdx.transportdata.tw/api/basic/v2/Bus/DailyStopTimeTable/City/Taichung/{routeName}?%24top=30&%24format=JSON");
                // Set access_token in request header
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenObj.access_token);
                // Get httpResponse
                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    DailyStopTimeTable[] dailyBusStopTable = JsonSerializer.Deserialize<DailyStopTimeTable[]>(content);
                    if (dailyBusStopTable != null)
                    {
                        return dailyBusStopTable;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetDailyStopTimeTavle" + ex);
                return null;
            }
        }

        public async Task<RealTimeNearStop[]> GetRealTimeNearStop(string routeName)
        {
            try
            {
                var tokenObj = await _tokenService.GetTDXToken();
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://tdx.transportdata.tw/api/basic/v2/Bus/RealTimeNearStop/City/Taichung/{routeName}?%24top=30&%24format=JSON");
                // Set access_token in request header
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenObj.access_token);
                // Get httpResponse
                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    // Console.WriteLine(content);
                    RealTimeNearStop[] realTimeNearStop = JsonSerializer.Deserialize<RealTimeNearStop[]>(content);
                    if (realTimeNearStop != null)
                    {
                        return realTimeNearStop;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("RealTimeNearStop:" + ex);
                return null;
            }
        }

        public async Task<EstimatedTimeOfArrival[]> GetEstimatedTimeOfArrival(string routeName)
        {
            try
            {
                var tokenObj = await _tokenService.GetTDXToken();
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://tdx.transportdata.tw/api/basic/v2/Bus/EstimatedTimeOfArrival/City/Taichung/{routeName}?%24format=JSON");
                // Set access_token in request header
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenObj.access_token);
                // Get httpResponse
                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    // Console.WriteLine(content);
                    EstimatedTimeOfArrival[] estimatedTimeOfArrival = JsonSerializer.Deserialize<EstimatedTimeOfArrival[]>(content);
                    if (estimatedTimeOfArrival != null)
                    {
                        return estimatedTimeOfArrival;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EstimatedTimeOfArrival" + ex);
                return null;
            }
        }
    }
}
