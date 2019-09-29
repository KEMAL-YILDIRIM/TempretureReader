using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Logic
{
    /// <summary>
    /// The ambient temperature processor computes the given temperatures for a sensor.
    /// </summary>
    public class AmbientTemperatureProcessor : IAmbientTemperatureProcessor
    {
        /// <summary>
        /// Computes the number of values to find out the ambient temprature.
        /// </summary>
        /// <param name="values"></param>
        /// <returns>Temprature</returns>
        public double Process(IEnumerable<double> values)
        {
            if (!values.Any())
                // todo: exception types could be implemented for a better understanding
                throw new Exception("Given file doesn't contain valid data.");

            // The temperature has to be stable for a number of readings
            var referenceValue = values.First();

            var groupOfValues = values.GroupBy(x => x);
            var mostRepeatedValue = groupOfValues
                .OrderByDescending(x => x.Count())
                .First();

            if (mostRepeatedValue.Key > values.Average()
                || mostRepeatedValue.Count() < 10)
                throw new Exception("Given file contains inconsistent data.");

            return mostRepeatedValue.Key;
        }
    }
}
