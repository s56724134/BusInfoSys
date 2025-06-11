using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.TDXApi
{
    public class BusRoute
    {
        public string RouteUID { get; set; }
        public string RouteID { get; set; }
        public bool HasSubRoutes { get; set; }
        public List<Operator> Operators { get; set; }
        public string AuthorityID { get; set; }
        public string ProviderID { get; set; }
        public List<SubRoute> SubRoutes { get; set; }
        public int BusRouteType { get; set; }
        public RouteName RouteName { get; set; }
        public string DepartureStopNameZh { get; set; }
        public string DepartureStopNameEn { get; set; }
        public string DestinationStopNameZh { get; set; }
        public string DestinationStopNameEn { get; set; }
        public string RouteMapImageUrl { get; set; }
        public string City { get; set; }
        public string CityCode { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
        public int VersionID { get; set; }
    }

     public class Operator
    {
        public string OperatorID { get; set; }
        public OperatorName OperatorName { get; set; }
        public string OperatorCode { get; set; }
        public string OperatorNo { get; set; }
    }

    public class OperatorName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class RouteName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }

    public class SubRoute
    {
        public string SubRouteUID { get; set; }
        public string SubRouteID { get; set; }
        public List<string> OperatorIDs { get; set; }
        public SubRouteName SubRouteName { get; set; }
        public string Headsign { get; set; }
        public int Direction { get; set; }
    }

    public class SubRouteName
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }
}
