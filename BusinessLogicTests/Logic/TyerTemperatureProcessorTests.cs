using BusinessLogic.Logic;
using BusinessLogic.Types;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicTests.Logic
{
    [TestFixture]
    public class TyreTemperatureProcessorTests
    {
        TyreTemperatureProcessor GetNewInstance()
        {
            return new TyreTemperatureProcessor();
        }

        [Test]
        public void FailInconsistentNumberOfReadings()
        {
            // Arrange
            var readings = new List<double>
            {
                12,
                13,
                14
            };

            // Assert
            Assert.Throws<Exception>(() => GetNewInstance().Process(readings, TyrePlacementType.Left));
        }

        [Test]
        public void FailInsufficantNumberOfReadings()
        {
            // Arrange
            var readings = new List<double>
            {
                12,
                13,
                14,
                11,
                11,
                11,
                11
            };

            // Assert
            Assert.Throws<Exception>(() => GetNewInstance().Process(readings, TyrePlacementType.Left));
        }

        [Test]
        public void FailBrokenSensorReadings()
        {
            // Arrange
            var readings = new List<double>
            {
                13,
                12,
                12,
                12,
                12,
                12,
                12,
                12,
                12,
                12,
                12,
                12,
                12,
                12,
                12,
                12,
                13,
                13,
                14,
                14,
                15
            };

            // Assert
            Assert.Throws<Exception>(() => GetNewInstance().Process(readings, TyrePlacementType.Left));
        }

        [Test]
        public void FailNoGivenNumberOfReadings()
        {
            // Arrange
            var readings = new List<double>();

            // Assert
            Assert.Throws<Exception>(() => GetNewInstance().Process(readings, TyrePlacementType.Left));
        }

        [Test]
        public void SuccessValidNumberOfReadings()
        {
            // Arrange
            var readings = new List<double>
            {
                13,
                12,
                12,
                12,
                12,
                12,
                12,
                12,
                13,
                13,
                14,
                14,
                14,
                14,
                15,
                15,
                15,
                15,
                14,
                14,
                14,
                14,
                13,
                13,
                13,
                13,
                12,
                12,
                12,
                12,
                12,
                12,
                12,
                12
            };

            // Act
            var result = GetNewInstance().Process(readings, TyrePlacementType.Left);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(result.Count(),1);
            Assert.AreEqual(result.First().MaximumTyreTemperature, 15);
        }

        [Test]
        public void SuccessValidNumberOfReadingsForTwoTyres()
        {
            // Arrange
            var readings = new List<double>
            {
                13,
                12,
                12,
                12,
                12,
                12,
                12,
                12,
                13,
                13,
                14,
                14,
                14,
                14,
                16,
                16,
                16,
                16,
                25,
                25,
                25,
                25,
                25,
                25,
                14,
                14,
                14,
                14,
                13,
                13,
                13,
                12,
                12,
                12,
                12,
                12,
                13,
                12,
                12,
                12,
                12,
                12,
                12,
                12,
                13,
                13,
                14,
                14,
                14,
                14,
                17,
                17,
                17,
                17,
                21,
                21,
                21,
                21,
                21,
                21,
                16,
                16,
                16,
                16,
                15,
                15,
                15,
                15,
                12,
                12,
                12,
                12,
                12,
                12,
                12,
                12
            };

            // Act
            var result = GetNewInstance().Process(readings, TyrePlacementType.Left);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(result.Count(), 2);
            Assert.AreEqual(result.First(tyre=>tyre.TyreNumber == 2).MaximumTyreTemperature, 21);
        }

    }
}
