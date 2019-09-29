using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using BusinessLogic.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Logic
{
    /// <summary>
    /// The average tyre temperature processor computes the temperature for the given sensor.
    /// </summary>
    public class TyreTemperatureProcessor : ITyreTemperatureProcessor
    {

        private const double Tolerance = 0.1;

        public IEnumerable<Tyre> Process(IEnumerable<double> temperatureReadings, TyrePlacementType tyrePlacementType)
        {
            var temperatureReadingsList = temperatureReadings.ToList();
            if (!temperatureReadingsList.Any())
                // todo: exception types could be implemented for a better understanding
                throw new Exception("Given file doesn't contain valid data.");

            var numberOfTemperatureReads = temperatureReadingsList.GroupBy(x => x);

            // Most repeated temperature should be the ambient temperature due to default readings of the sensor.
            var ambientTemperature = numberOfTemperatureReads
                .OrderByDescending(x => x.Count())
                .First();

            // If readings below 10 the readings should be false or not enough.
            if (ambientTemperature.Key > temperatureReadingsList.Average()
                || ambientTemperature.Count() < 10)
                throw new Exception("Given file contains inconsistent data.");

            var listOfValues = temperatureReadingsList.ToList();
            List<(double, int, int)> flattenList = IndexedNumberOfTemperatureReadings(listOfValues);

            // Should have at least 3 consecutive decreasing value to define the maximum temperature
            var listOfTyre = new List<Tyre>();
            var tyreNumber = 1;
            for (var i = 0; i < flattenList.Count - 2; i++)
            {
                if (!(flattenList[i].Item1 > flattenList[i + 1].Item1) ||
                    !(flattenList[i + 1].Item1 > flattenList[i + 2].Item1) ||
                    !(flattenList[i].Item1 - ambientTemperature.Key > 2) || flattenList[i].Item2 <= 2) continue;


                var maximumTyreTemperature = flattenList[i].Item1;
                var valuesUpToMaximumTyreTemperature = flattenList.Take(i + 1);

                // Set next index to process the nex tyre.
                i = flattenList.FindIndex(item => item.Item3 > flattenList[i].Item3 && Math.Abs(item.Item1 - ambientTemperature.Key) < Tolerance);

                // Take the increasing temperature values from ambient to max to determine average
                var upToMaximumTyreTemperature = valuesUpToMaximumTyreTemperature.ToList();
                var reverseList = upToMaximumTyreTemperature.Select(item => item.Item1).Reverse()
                    .TakeWhile(predicate: item =>
                    {
                        if (Math.Abs(item - ambientTemperature.Key) > Tolerance) return true;
                        return false;
                    });

                var averageTyreTemperature = reverseList.Concat(
                        upToMaximumTyreTemperature.Select(item => item.Item1)
                            .TakeWhile(item => Math.Abs(item - ambientTemperature.Key) < Tolerance))
                    .Average();


                listOfTyre.Add(new Tyre
                {
                    MaximumTyreTemperature = maximumTyreTemperature,
                    AverageTyreTemperature = averageTyreTemperature,
                    ReadDate = DateTime.Now,
                    TyrePlacement = tyrePlacementType,
                    TyreNumber = tyreNumber
                });

                tyreNumber++;
            }

            if (listOfTyre.Any())
                throw new Exception("Given file contains inconsistent data.");

            return listOfTyre;
        }

        /// <summary>
        /// Indexed list of number of temperature readings.
        /// </summary>
        /// <param name="listOfValues"></param>
        /// <returns></returns>
        private static List<(double, int, int)> IndexedNumberOfTemperatureReadings(List<double> listOfValues)
        {
            var flattenList = new List<ValueTuple<double, int, int>>();
            int repeat = 1, flattenIndex = 0;
            for (var i = 0; i < listOfValues.Count - 1; i++)
            {
                var current = listOfValues[i];
                var next = listOfValues[i + 1];

                if (i == listOfValues.Count - 2)
                {
                    if (Math.Abs(current - listOfValues.Last()) < Tolerance) repeat++;
                    else repeat = 1;
                    flattenList.Add((listOfValues[i], repeat, flattenIndex));
                }
                else
                {
                    if (Math.Abs(current - next) < Tolerance)
                    {
                        repeat++;
                    }
                    else
                    {
                        flattenList.Add((listOfValues[i], repeat, flattenIndex));
                        repeat = 1;
                        flattenIndex++;
                    }
                }
            }

            return flattenList;
        }
    }
}
