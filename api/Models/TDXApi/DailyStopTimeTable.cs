using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.TDXApi
{
    public class DailyStopTimeTable
    {
        public DateTimeOffset BusDate { get; set; }
        public string RouteUID { get; set; }
        public string RouteID { get; set; }
        public DailyStopTimeTableRouteName RouteName { get; set; }
        public string OperatorID { get; set; }
        public string OperatorCode { get; set; }
        public string SubRouteUID { get; set; }
        public string SubRouteID { get; set; }
        public DailyStopTimeTableSubRouteName SubRouteName { get; set; }
        public string DestinationStopID { get; set; }
        public DailyStopTimeTableDestinationStopName DestinationStopName { get; set; }
        public List<DailyStopTimeTableStop> Stops { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
    }

    public class DailyStopTimeTableDestinationStopName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class DailyStopTimeTableRouteName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class DailyStopTimeTableStop
    {
        public string StopUID { get; set; }
        public string StopID { get; set; }
        public DailyStopTimeTableStopName StopName { get; set; }
        public List<TimeTable> TimeTables { get; set; }
    }

    public class DailyStopTimeTableStopName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class DailyStopTimeTableSubRouteName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class TimeTable
    {
        public int Sequence { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public int TimeType { get; set; }
    }


}
