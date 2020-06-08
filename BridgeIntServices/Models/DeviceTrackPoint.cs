using System;

namespace BridgeIntServices.Models
{
    public class DeviceTrackPoint
    {
        public int AcademyId { get; set; }
        public double BatteryLevel { get; set; }
        public string EmployeeId { get; set; }
        public string SerialNumber { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
