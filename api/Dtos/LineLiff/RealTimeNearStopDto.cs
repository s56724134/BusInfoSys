using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.LineLiff
{
    public class RealTimeNearStopDto
    {
        public string PlateNumb { get; set; }
        public string RouteUID { get; set; }
        public string RouteID { get; set; }
        public RealTimeNearStopRouteNameDto RouteName { get; set; }
        public string SubRouteUID { get; set; }
        public string SubRouteID { get; set; }
        public RealTimeNearStopSubRouteNameDto SubRouteName { get; set; }
        public int Direction { get; set; }
        public string StopUID { get; set; }
        public string StopID { get; set; }
        public RealTimeNearStopStopNameDto StopName { get; set; }
        public int StopSequence { get; set; }
    }

    public class RealTimeNearStopRouteNameDto
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class RealTimeNearStopSubRouteNameDto
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class RealTimeNearStopStopNameDto
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }
}
