using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using BusinessLogic.Types;
using ConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    public class CalculateTempratures : ICalculateTempratures
    {
        private IAmbientTemperatureProcessor _ambientTemperatureProcessor;
        private ITyreTemperatureProcessor _tyreTemperatureProcessor;
        private IFileReader _fileReader;

        public CalculateTempratures(IAmbientTemperatureProcessor ambientTemperatureProcessor,
            ITyreTemperatureProcessor tyreTemperatureProcessor,
            IFileReader fileReader)
        {
            _ambientTemperatureProcessor = ambientTemperatureProcessor;
            _tyreTemperatureProcessor = tyreTemperatureProcessor;
            _fileReader = fileReader;
        }


        public bool Execute()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"Temprerature Calculator V1.0" + Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Please enter the path of the data file.");

            var path = Console.ReadLine();
            var dataList = _fileReader.Read(path);

            IEnumerable<double> leftDataList,rightDataList;
            dataList.TryGetValue(LineOfSightType.Left,out leftDataList);
            dataList.TryGetValue(LineOfSightType.Right, out rightDataList);

            var leftTyreList = _tyreTemperatureProcessor.Process(leftDataList, TyrePlacementType.Left);
            var rightTyreList = _tyreTemperatureProcessor.Process(rightDataList, TyrePlacementType.Right);

            var leftSensor = _ambientTemperatureProcessor.Process(leftDataList);
            var rightSensor = _ambientTemperatureProcessor.Process(rightDataList);

            // Left tyers
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("RESULTS FOR LEFT TYERS");
            Console.WriteLine();
            OutputTyreResult(leftTyreList);

            // Right tyres
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("RESULTS FOR RIGHT TYERS");
            Console.WriteLine();
            OutputTyreResult(rightTyreList);

            // Sensors
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("RESULTS FOR SENSORS");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sernsor Left  => Ambient Temperature : {0}",leftSensor);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sernsor Right => Ambient Temperature : {0}",rightSensor);
            return true;
        }

        public void OutputTyreResult(IEnumerable<Tyre> tyreList)
        {
            foreach (var tyre in tyreList)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Tyer {0} => Average Temperature : {1} Maximum Temperature : {2}",
                    tyre.TyreNumber,
                    tyre.AverageTyreTemperature,
                    tyre.MaximumTyreTemperature);
            }
        }
    }
}
