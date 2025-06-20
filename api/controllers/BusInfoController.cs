using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.Mapper;
using api.Models.TDXApi;
using api.Dtos;


namespace api.controllers
{
    [Route("api/businfo")]
    [ApiController]
    public class BusInfoController : ControllerBase
    {
        // Dependent Injection
        private readonly IBusInfoService _busInfoService;
        public BusInfoController(IBusInfoService busInfoService)
        {
            _busInfoService = busInfoService;
        }

        [HttpGet("BusRoute/{RouteName}")]
        public async Task<IActionResult> GetBusRoute([FromRoute] string RouteName)
        {
            Console.WriteLine("=== Controller GetBusRoute called ===");
            // Get the busRoute data
            BusRoute[] busRouteModel = await _busInfoService.GetBusRoute(RouteName);
            if (busRouteModel != null)
            {
                var busRouteDto = busRouteModel.Select(s => s.ToBusRouteDto());
                return Ok(busRouteDto);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("BusStopOfRoute/{RouteName}")]
        public async Task<IActionResult> GetBusStopOfRoute([FromRoute] string RouteName)
        {
            Console.WriteLine("================GetBusStopOfRoute start================");
            var busStopOfRouteModel = await _busInfoService.GetBusStopOfRoute(RouteName);
            if (busStopOfRouteModel != null)
            {
                var busStopOfRouteDto = busStopOfRouteModel.Select(s => s.ToDisplayBusStopOfRouteDto());
                return Ok(busStopOfRouteDto);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("DailyStopTimeTable/{RouteName}")]
        public async Task<IActionResult> GetDailyStopTimeTable([FromRoute] string RouteName)
        {
            var dailyStopTimeTable = await _busInfoService.GetDailyStopTimeTable(RouteName);
            if (dailyStopTimeTable != null)
            {
                var dailyStopTimeTableDto = dailyStopTimeTable.Select(d => d.ToDailyStopTimeTableDto());
                return Ok(dailyStopTimeTableDto);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet("RealTimeNearStop/{RouteName}")]
        public async Task<IActionResult> GetRealTimeNearStop([FromRoute] string RouteName)
        {
            var realTimeNearStop = await _busInfoService.GetRealTimeNearStop(RouteName);
            if (realTimeNearStop != null)
            {
                var realTimeNearStopDto = realTimeNearStop
                    .Where(r => r.RouteID == RouteName)
                    .Select(r => r.ToRealTimeNearStopDto());

                return Ok(realTimeNearStopDto);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet("EstimatedTimeOfArrival/{RouteName}")]
        public async Task<IActionResult> GetEstimatedTimeOfArrival([FromRoute] string RouteName)
        {
            var estimatedTimeOfArrival = await _busInfoService.GetEstimatedTimeOfArrival(RouteName);
            if (estimatedTimeOfArrival != null)
            {
                var estimatedTimeOfArrivalDto = estimatedTimeOfArrival
                    .Where(e => e.RouteID == RouteName)
                    .OrderBy(e => e.Direction)
                    .ThenBy(e => e.StopSequence)
                    .Select(e => e.ToEstimatedTimeOfArreivalDto());
                // Console.WriteLine(estimatedTimeOfArrivalDto);
                return Ok(estimatedTimeOfArrivalDto);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
