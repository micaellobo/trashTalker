using System;

namespace TrashTalker.Models
{
    /// <summary>
    /// The Picking class stores information about the Picking
    /// </summary>
    public class Picking
    {
        /// <summary>
        /// unique identifier of the Picking
        /// </summary>
        public Guid id { get; set; }

        /// <summary>
        /// contain the volumeRecolhido of the picking
        /// </summary>
        public float volumeRecolhido { get; set; }

        /// <summary>
        /// <see cref="Container"/> collection associated with a particular ecopoint
        /// </summary>
        public virtual Container container { get; set; }

        /// <summary>
        /// contain the date and time of the picking
        /// </summary>
        public DateTime data { get; set; }
        
        /// <summary>
        /// Contain the average growth rate per day of container occupied volume.
        /// </summary>
        public Double avgGrowthOccupiedPerDay { get; set; }
    }
}