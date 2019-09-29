using BusinessLogic.Types;
using ConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp.Helpers
{
    public class FileReader : IFileReader
    {
        public Dictionary<LineOfSightType, IEnumerable<double>> Read(string path)
        {
            var list = new Dictionary<LineOfSightType, IEnumerable<double>>();

            if (!File.Exists(path))
            {
                throw new Exception("File not found!");
            }

            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

            String line;
            var leftReadings = new List<double>();
            var rightReadings = new List<double>();
            //Pass the file path and file name to the StreamReader constructor
            using (var sr = new StreamReader(fileStream, Encoding.UTF8))
            {

                //Read the first line of text
                line = sr.ReadLine();

                var lineNumber = 0;
                //Continue to read until you reach end of file                
                while (line != null)
                {
                    if (lineNumber == 0)
                    {
                        lineNumber++;
                        continue;
                    }
                    
                    //Read the next line
                    line = sr.ReadLine();
                    if(line is null)
                    {
                        lineNumber++;
                        continue;
                    }

                    var values = line.Trim().Split("\t");

                    double leftValue, rightValue;

                    // Left readings
                    double.TryParse(values[0],out leftValue);
                    leftReadings.Add(leftValue);

                    // Right readings
                    double.TryParse(values[0], out rightValue);
                    rightReadings.Add(rightValue);

                    lineNumber++;
                }

                sr.Close();

            }

            list.Add(LineOfSightType.Left,leftReadings);
            list.Add(LineOfSightType.Right, rightReadings);
            return list;
        }
    }
}
