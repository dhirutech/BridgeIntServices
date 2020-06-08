using BridgeIntServices.Core.Interfaces;
using BridgeIntServices.Core.Models;
using BridgeIntServices.Interfaces;
using BridgeIntServices.Models;
using BridgeIntServices.Repositories.Interfaces;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BridgeIntServices.Logics
{
    public class BatteryUsageLogics : IBatteryUsageLogics
    {
        private readonly IResponseHelper _helper;
        private readonly IBatteryUsageRepository _batteryUsageRepo;
        public BatteryUsageLogics(IResponseHelper helper, IBatteryUsageRepository batteryUsageRepo)
        {
            _helper = helper;
            _batteryUsageRepo = batteryUsageRepo;
        }

        public async Task<ResponseObject> CalculateBatteryDailyAverage(List<DeviceTrackPoint> trackPoints)
        {
            var deviceUsageList =
                (from device in trackPoints
                 group device by device.SerialNumber into devices
                 select new BatteryUsage
                 {
                     SerialNumber = devices.Key,
                     AverageDailyBattery = GetAvarage(devices.OrderBy(d => d.Timestamp))
                 }).ToList();

            return _helper.SuccessResponse("Operation successful", deviceUsageList);
        }

        private string GetAvarage(IEnumerable<DeviceTrackPoint> trackPointsByDevice)
        {
            if (trackPointsByDevice.Count() == 1)
                return "unknown";

            var firstTrackPoint = trackPointsByDevice.First();
            var lastTrackPoint = trackPointsByDevice.Last();
            var pointsList = trackPointsByDevice.ToList();
            for (int i = 1; i < pointsList.Count(); i++)
            {
                if (pointsList[i - 1].BatteryLevel < pointsList[i].BatteryLevel)
                {
                    lastTrackPoint = pointsList[i - 1];
                    break;
                }
            }
            var batteryLeveldownby = firstTrackPoint.BatteryLevel - lastTrackPoint.BatteryLevel;
            if (batteryLeveldownby == 0)
                return "unknown";

            var timediff = lastTrackPoint.Timestamp - firstTrackPoint.Timestamp;
            var tt = batteryLeveldownby / timediff.TotalHours * 24;

            return tt.ToString("P", CultureInfo.InvariantCulture);
        }
    }
}
