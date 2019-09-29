using BusinessLogic.Types;
using System;

namespace BusinessLogic.Entities
{
    /// <summary>
    /// The tyre.
    /// </summary>
    public class Tyre
    {
        public Tyre()
        {
            Id = Guid.NewGuid();
        }

        // Every tier has to have a unique id.
        public Guid Id { get; }
        public TyrePlacementType TyrePlacement { get; set; }
        public int TyreNumber { get; set; }
        public double MaximumTyreTemperature { get; set; }
        public double AverageTyreTemperature { get; set; }
        public DateTime ReadDate { get; set; }
    }
}
