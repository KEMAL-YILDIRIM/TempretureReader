using BusinessLogic.Types;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Entities
{
    /// <summary>
    /// The temprature reader sensor.
    /// </summary>
    public class Sensor
    {
        // Initialize the empty readings.
        public Sensor()
        {
            Id = Guid.NewGuid();
            TempreratureReadings = TempreratureReadings ?? new List<double>();
        }

        // In case of having multiple sensors every sensor has to have a unique Id.
        public Guid Id { get; }
        public IEnumerable<double> TempreratureReadings { get; set; }
        public LineOfSightType LineOfSight { get; set; }
        public double AmbientTemperature { get; set; }
        public DateTime ReadDate { get; set; }
    }
}
