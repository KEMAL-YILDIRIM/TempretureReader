using BusinessLogic.Types;
using System;
using System.Collections.Generic;

namespace ConsoleApp.Interfaces
{
    public interface IFileReader
    {
        Dictionary<LineOfSightType, IEnumerable<double>> Read(string path);
    }
}