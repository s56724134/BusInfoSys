using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.Mapper;
using api.Models.TDXApi;


namespace api.controllers
{
    [Route("api/businfo")]
    [ApiController]
    public class LineLiffController : ControllerBase
    {
        // Dependent Injection
        private readonly ILineLiffBusService _lineLiffBusService;
        public LineLiffController(ILineLiffBusService lineLiffBusService)
        {
            _lineLiffBusService = lineLiffBusService;
        }

        [HttpGet("BusRoute/{RouteName}")]
        public async Task<IActionResult> GetBusRoute([FromRoute] string RouteName)
        {
            Console.WriteLine("=== Controller GetBusRoute called ===");
            // Get the busRoute data
            BusRoute[] busRouteModel = await _lineLiffBusService.GetBusRoute(RouteName);
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
            var busStopOfRouteModel = await _lineLiffBusService.GetBusStopOfRoute(RouteName);
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
            var dailyStopTimeTable = await _lineLiffBusService.GetDailyStopTimeTable(RouteName);
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
            var realTimeNearStop = await _lineLiffBusService.GetRealTimeNearStop(RouteName);
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
            var estimatedTimeOfArrival = await _lineLiffBusService.GetEstimatedTimeOfArrival(RouteName);
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
