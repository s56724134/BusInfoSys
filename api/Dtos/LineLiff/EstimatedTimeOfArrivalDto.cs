using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.LineLiff
{
    public class EstimatedTimeOfArrivalDto
    {
        public string PlateNumb { get; set; }
        public string StopUID { get; set; }
        public string StopID { get; set; }
        public EstimatedTimeOfArrivalStopNameDto StopName { get; set; }
        public string RouteUID { get; set; }
        public string RouteID { get; set; }
        public EstimatedTimeOfArrivalRouteNameDto RouteName { get; set; }
        public string SubRouteUID { get; set; }
        public string SubRouteID { get; set; }
        public EstimatedTimeOfArrivalSubRouteNameDto SubRouteName { get; set; }
        public int Direction { get; set; }
        public int EstimateTime { get; set; }
        public int StopSequence { get; set; }
        public int StopStatus { get; set; }
        public DateTimeOffset NextBusTime { get; set; }
        public List<EstimatedTimeOfArrivalEstimateDto> Estimates { get; set; }
        public DateTimeOffset SrcUpdateTime { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
    }

    public class EstimatedTimeOfArrivalStopNameDto
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class EstimatedTimeOfArrivalRouteNameDto
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class EstimatedTimeOfArrivalSubRouteNameDto
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class EstimatedTimeOfArrivalEstimateDto
    {
        public string PlateNumb { get; set; }
        public int EstimateTime { get; set; }
        public bool IsLastBus { get; set; }
    }


}
