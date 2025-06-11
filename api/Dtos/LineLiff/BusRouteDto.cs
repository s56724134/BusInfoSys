using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.LineLiff
{
    public class BusRouteDto
    {
        public string RouteID { get; set; }
        public List<SubRouteDtoOfBusRoute> SubRoutes { get; set; }
        public string DepartureStopNameZh { get; set; }
        public string DepartureStopNameEn { get; set; }
        public string DestinationStopNameZh { get; set; }
        public string DestinationStopNameEn { get; set; }
    }

    public class SubRouteDtoOfBusRoute
    {
        public string SubRouteUID { get; set; }
        public string SubRouteID { get; set; }
        public SubRouteNameDtoOfBusRoute SubRouteName { get; set; }
        public int Direction { get; set; }

    }

    public class SubRouteNameDtoOfBusRoute
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }
}
