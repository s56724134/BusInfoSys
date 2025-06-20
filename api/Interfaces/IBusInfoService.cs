using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.TDXApi;

namespace api.Interfaces
{
    public interface IBusInfoService
    {
        Task<BusRoute[]> GetBusRoute(string routeName);

        Task<DisplayStopOfRoute[]> GetBusStopOfRoute(string routeName);

        Task<DailyStopTimeTable[]> GetDailyStopTimeTable(string routeName);

        Task<RealTimeNearStop[]> GetRealTimeNearStop(string routName);

        Task<EstimatedTimeOfArrival[]> GetEstimatedTimeOfArrival(string routeName);
    }
}
