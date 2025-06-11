using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.TDXApi
{
    public class EstimatedTimeOfArrival
    {
        public string PlateNumb { get; set; }
        public string StopUID { get; set; }
        public string StopID { get; set; }
        public EstimatedTimeOfArrivalStopName StopName { get; set; }
        public string RouteUID { get; set; }
        public string RouteID { get; set; }
        public EstimatedTimeOfArrivalRouteName RouteName { get; set; }
        public string SubRouteUID { get; set; }
        public string SubRouteID { get; set; }
        public EstimatedTimeOfArrivalSubRouteName SubRouteName { get; set; }
        public int Direction { get; set; }
        public int EstimateTime { get; set; }
        public int StopSequence { get; set; }
        public int StopStatus { get; set; }
        public DateTimeOffset NextBusTime { get; set; }
        public List<EstimatedTimeOfArrivalEstimate> Estimates { get; set; }
        public DateTimeOffset SrcUpdateTime { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
    }

    public class EstimatedTimeOfArrivalRouteName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class EstimatedTimeOfArrivalStopName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class EstimatedTimeOfArrivalSubRouteName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class EstimatedTimeOfArrivalEstimate
    {
        public string PlateNumb { get; set; }
        public int EstimateTime { get; set; }
        public bool IsLastBus { get; set; }
    }
}
