using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.TDXApi
{
    public class DisplayStopOfRoute
    {
        public string RouteUID { get; set; }
        public string RouteID { get; set; }
        public DisplayStopRouteName RouteName { get; set; }
        public int Direction { get; set; }
        public List<DisplayStop> Stops { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
        public int VersionID { get; set; }
    }
    public class DisplayStopRouteName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class DisplayStop
    {
        public string StopUID { get; set; }
        public string StopID { get; set; }
        public DisplayStopName StopName { get; set; }
        public int StopBoarding { get; set; }
        public int StopSequence { get; set; }
        public DisplayStopPosition StopPosition { get; set; }
        public string StationID { get; set; }
    }

    public class DisplayStopName
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
