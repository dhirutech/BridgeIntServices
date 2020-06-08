using BridgeIntServices.Core.Interfaces;
using BridgeIntServices.Interfaces;
using BridgeIntServices.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BridgeIntServices.Controllers
{
    [Route("api/v1/battery")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BatteryUsageController : ControllerBase
    {
        private readonly IBatteryUsageLogics _batteryUsageLogics;
        private readonly IResponseHelper _helper;
        public BatteryUsageController(IBatteryUsageLogics batteryUsageLogics, IResponseHelper helper)
        {
            _batteryUsageLogics = batteryUsageLogics;
            _helper = helper;
        }

        [HttpPost("dailyaverage")]
        public async Task<ActionResult> CalculateBatteryDailyAverage(List<DeviceTrackPoint> trackPoints)
        {
            try
            {
                var result = await _batteryUsageLogics.CalculateBatteryDailyAverage(trackPoints);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return _helper.CreateApiError(ex);
            }
        }
    }
}