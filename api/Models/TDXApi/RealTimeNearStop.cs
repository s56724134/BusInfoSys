using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.TDXApi
{
    public class RealTimeNearStop
    {
        public string PlateNumb { get; set; }
        public string OperatorID { get; set; }
        public string OperatorNo { get; set; }
        public string RouteUID { get; set; }
        public string RouteID { get; set; }
        public RealTimeNearStopRouteName RouteName { get; set; }
        public string SubRouteUID { get; set; }
        public string SubRouteID { get; set; }
        public RealTimeNearStopSubRouteName SubRouteName { get; set; }
        public int Direction { get; set; }
        public string StopUID { get; set; }
        public string StopID { get; set; }
        public RealTimeNearStopStopName StopName { get; set; }
        public int StopSequence { get; set; }
        public int DutyStatus { get; set; }
        public int BusStatus { get; set; }
        public int A2EventType { get; set; }
        public DateTimeOffset GPSTime { get; set; }
        public int TripStartTimeType { get; set; }
        public DateTimeOffset TripStartTime { get; set; }
        public DateTimeOffset SrcUpdateTime { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
    }

     public class RealTimeNearStopRouteName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class RealTimeNearStopStopName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class RealTimeNearStopSubRouteName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

}
