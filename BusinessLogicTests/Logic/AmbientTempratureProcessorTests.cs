using BusinessLogic.Logic;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BusinessLogicTests.Logic
{
    [TestFixture]
    public class AmbientTempratureProcessorTests
    {
        AmbientTemperatureProcessor GetNewInstance()
        {
            return new AmbientTemperatureProcessor();
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
            Assert.Throws<Exception>(() => GetNewInstance().Process(readings));
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
            Assert.Throws<Exception>(() => GetNewInstance().Process(readings));
        }

        [Test]
        public void FailNoGivenNumberOfReadings()
        {
            // Arrange
            var readings = new List<double>();

            // Assert
            Assert.Throws<Exception>(() => GetNewInstance().Process(readings));
        }

        [Test]
        public void SuccessValidNumberOfReadings()
        {
            // Arrange
            var readings = new List<double>
            {
                12,
                12,
                12,
                12,
                12,
                12,
                12,
                12,
                13,
                14,
                14,
                14,
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
            var result = GetNewInstance().Process(readings);

            // Assert
            Assert.AreEqual(result, 12.0d);
        }

    }
}
