using System;

namespace EcoScooter.Entities
{
    public partial class TrackPoint
    {

        public TrackPoint()
        {
        }

        public TrackPoint(double batteryLevel, double latitude, double longitude,
            double speed, DateTime timestamp)
        {
            BatteryLevel = batteryLevel;
            Latitude = latitude;
            Longitude = longitude;
            Speed = speed;
            Timestamp = timestamp;
        }
    }
}
