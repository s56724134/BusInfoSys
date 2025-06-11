using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.LineLiff
{
    public class DisplayBusStopOfRouteDto
    {
        public string RouteID { get; set; }
        public int Direction { get; set; }
        public List<DisplayStopOfRoute> Stops { get; set; }
    }

    public class DisplayStopOfRoute
    {
        public string StopUID { get; set; }
        public string StopID { get; set; }
        public DisplayStopRouteName StopName { get; set; }
        public int StopSequence { get; set; }
        public DisplayStopPosition StopPosition { get; set; }
        public string StationID { get; set; }
    }

    public class DisplayStopRouteName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class DisplayStopPosition
    {
        public double PositionLon { get; set; }
        public double PositionLat { get; set; }
        public string GeoHash { get; set; }
    }
}
