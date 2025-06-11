using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.LineLiff;
using api.Models.TDXApi;

namespace api.Mapper
{
    public static class BusMapper
    {
        public static BusRouteDto ToBusRouteDto(this BusRoute busRoute)
        {
            return new BusRouteDto
            {
                RouteID = busRoute.RouteID,
                SubRoutes = busRoute.SubRoutes.Select(s =>
                    new SubRouteDtoOfBusRoute
                    {
                        SubRouteUID = s.SubRouteUID,
                        SubRouteID = s.SubRouteID,
                        SubRouteName = new SubRouteNameDtoOfBusRoute
                        {
                            Zh_tw = s.SubRouteName.Zh_tw,
                            En = s.SubRouteName.En
                        }
                    }).ToList(),
                DepartureStopNameZh = busRoute.DepartureStopNameZh,
                DepartureStopNameEn = busRoute.DepartureStopNameEn,
                DestinationStopNameZh = busRoute.DestinationStopNameZh,
                DestinationStopNameEn = busRoute.DestinationStopNameEn
            };
        }

        public static DisplayBusStopOfRouteDto ToDisplayBusStopOfRouteDto(this Models.TDXApi.DisplayStopOfRoute source)
        {
            return new DisplayBusStopOfRouteDto
            {
                RouteID = source.RouteID,
                Direction = source.Direction,
                Stops = source.Stops.Select(s => new Dtos.LineLiff.DisplayStopOfRoute
                {
                    StopUID = s.StopUID,
                    StopID = s.StopID,
                    StopName = new Dtos.LineLiff.DisplayStopRouteName
                    {
                        Zh_tw = s.StopName?.Zh_tw,
                        En = s.StopName?.En
                    },
                    StopSequence = s.StopSequence,
                    StopPosition = s.StopPosition != null ? new Dtos.LineLiff.DisplayStopPosition
                    {
                        PositionLon = s.StopPosition.PositionLon,
                        PositionLat = s.StopPosition.PositionLat,
                        GeoHash = s.StopPosition.GeoHash
                    } : null,
                    StationID = s.StationID
                }).ToList()
            };
        }
        public static DailyStopTimeTableDto ToDailyStopTimeTableDto(this DailyStopTimeTable source)
        {
            return new DailyStopTimeTableDto
            {
                BusDate = source.BusDate,
                RouteUID = source.RouteUID,
                RouteID = source.RouteID,
                RouteName = new DailyStopTimeTableRouteNameDto
                {
                    Zh_tw = source.RouteName.Zh_tw,
                    En = source.RouteName.En
                },
                DestinationStopID = source.DestinationStopID,
                DestinationStopName = new DailyStopTimeTableDestinationStopNameDto
                {
                    Zh_tw = source.DestinationStopName.Zh_tw,
                    En = source.DestinationStopName.En
                },
                Stops = source.Stops.Select(s => new DailyStopTimeTableStopDto
                {
                    StopUID = s.StopUID,
                    StopID = s.StopID,
                    StopName = new DailyStopTimeTableStopNameDto
                    {
                        Zh_tw = s.StopName.Zh_tw,
                        En = s.StopName.En
                    },
                    TimeTables = s.TimeTables.Select(t => new TimeTableDto
                    {
                        Sequence = t.Sequence,
                        ArrivalTime = t.ArrivalTime,
                        DepartureTime = t.DepartureTime,
                        TimeType = t.TimeType
                    }).ToList()
                }).ToList(),
                UpdateTime = source.UpdateTime
            };
        }
        public static RealTimeNearStopDto ToRealTimeNearStopDto(this RealTimeNearStop source)
        {
            return new RealTimeNearStopDto
            {
                PlateNumb = source.PlateNumb,
                RouteUID = source.OperatorID,
                RouteID = source.RouteID,
                RouteName = new RealTimeNearStopRouteNameDto
                {
                    Zh_tw = source.RouteName.Zh_tw,
                    En = source.RouteName.En
                },
                SubRouteUID = source.SubRouteUID,
                SubRouteID = source.SubRouteID,
                SubRouteName = new RealTimeNearStopSubRouteNameDto
                {
                    Zh_tw = source.SubRouteName.Zh_tw,
                    En = source.SubRouteName.En
                },
                Direction = source.Direction,
                StopUID = source.StopUID,
                StopID = source.StopID,
                StopName = new RealTimeNearStopStopNameDto
                {
                    Zh_tw = source.StopName.Zh_tw,
                    En = source.StopName.En
                },
                StopSequence = source.StopSequence
            };
        }

        public static EstimatedTimeOfArrivalDto ToEstimatedTimeOfArreivalDto(this EstimatedTimeOfArrival source)
        {

            return new EstimatedTimeOfArrivalDto
            {
                PlateNumb = source.PlateNumb,
                StopUID = source.StopUID,
                StopID = source.StopID,
                StopName = source.StopName == null ? null : new EstimatedTimeOfArrivalStopNameDto
                {
                    Zh_tw = source.StopName?.Zh_tw,
                    En = source.StopName?.En
                },
                RouteUID = source.RouteUID,
                RouteID = source.RouteID,
                RouteName = source.RouteName == null ? null : new EstimatedTimeOfArrivalRouteNameDto
                {
                    Zh_tw = source.RouteName?.Zh_tw,
                    En = source.RouteName?.En
                },
                SubRouteUID = source.SubRouteUID,
                SubRouteID = source.SubRouteID,
                SubRouteName = source.SubRouteName == null ? null : new EstimatedTimeOfArrivalSubRouteNameDto
                {
                    Zh_tw = source.SubRouteName?.Zh_tw,
                    En = source.SubRouteName?.En
                },
                Direction = source.Direction,
                EstimateTime = source.EstimateTime,
                StopSequence = source.StopSequence,
                StopStatus = source.StopStatus,
                NextBusTime = source.NextBusTime,
                Estimates = source.Estimates?.Select(e => new EstimatedTimeOfArrivalEstimateDto
                {
                    PlateNumb = e.PlateNumb,
                    EstimateTime = e.EstimateTime,
                    IsLastBus = e.IsLastBus
                }).ToList() ?? new List<EstimatedTimeOfArrivalEstimateDto>(),
                SrcUpdateTime = source.SrcUpdateTime,
                UpdateTime = source.UpdateTime
            };
        }
    }
}
