using BusinessLogic.Entities;
using BusinessLogic.Types;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    /// <summary>
    /// The tyre temperature processor interface.
    /// </summary>
    public interface ITyreTemperatureProcessor
    {
        IEnumerable<Tyre> Process(IEnumerable<double> values, TyrePlacementType tyrePlacementType);
    }
}
