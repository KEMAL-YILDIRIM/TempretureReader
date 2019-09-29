using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    /// <summary>
    /// The ambient temperature processor interface.
    /// </summary>
    public interface IAmbientTemperatureProcessor
    {
        double Process(IEnumerable<double> values);
    }
}
