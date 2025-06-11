using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.LineLiff
{
    public class DailyStopTimeTableDto
    {
        public DateTimeOffset BusDate { get; set; }
        public string RouteUID { get; set; }
        public string RouteID { get; set; }
        public DailyStopTimeTableRouteNameDto RouteName { get; set; }
        public string DestinationStopID { get; set; }
        public DailyStopTimeTableDestinationStopNameDto DestinationStopName { get; set; }
        public List<DailyStopTimeTableStopDto> Stops { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
    }

    public class DailyStopTimeTableRouteNameDto
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class DailyStopTimeTableDestinationStopNameDto
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class DailyStopTimeTableStopDto
    {
        public string StopUID { get; set; }
        public string StopID { get; set; }
        public DailyStopTimeTableStopNameDto StopName { get; set; }
        public List<TimeTableDto> TimeTables { get; set; }
    }

    public class DailyStopTimeTableStopNameDto
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class TimeTableDto
    {
        public int Sequence { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public int TimeType { get; set; }
    }

}
