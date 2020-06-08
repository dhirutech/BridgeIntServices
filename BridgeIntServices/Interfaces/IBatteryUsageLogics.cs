using BridgeIntServices.Core.Models;
using BridgeIntServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BridgeIntServices.Interfaces
{
    public interface IBatteryUsageLogics
    {
        Task<ResponseObject> CalculateBatteryDailyAverage(List<DeviceTrackPoint> trackPoints);
    }
}
