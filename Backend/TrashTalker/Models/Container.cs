using System;
using System.Collections.Generic;
using TrashTalker.Models.Enumerations;

namespace TrashTalker.Models
{
    /// <summary>
    /// The Container class stores information about the Container
    /// </summary>
    public class Container
    {
        /// <summary>
        /// unique identifier of the container
        /// </summary>
        public Guid id { get; set; }

        /// <summary>
        /// <see cref="Status"/> contains the status of the container
        /// </summary>
        public Status status { get; set; }

        /// <summary>
        /// <see cref="RecycleBin"/> Ecopoint to which a particular container is associated
        /// </summary>
        public virtual RecycleBin RecBin { get; set; }

        /// <summary>
        /// <see cref="List{Picking}"/> of <see cref="Picking"/> contains the list of picks from a container
        /// </summary>
        public virtual List<Picking> listPickings { get; set; }

        /// <summary>
        /// <see cref="TypeOfWaste"/> contains the type of waste in the container
        /// </summary>
        public TypeOfWaste typeOfWaste { get; set; }

        /// <summary>
        /// contain the height of the container
        /// </summary>
        public float height { get; set; }

        /// <summary>
        /// contain the width of the container
        /// </summary>
        public float width { get; set; }

        /// <summary>
        /// contain the depth of the container
        /// </summary>
        public float depth { get; set; }

        /// <summary>
        /// Contain the average growth rate of occupied volume per day  
        /// </summary>
        public double avgGrowthOccupiedVolumePerDay { get; set; }

        /// <summary>
        /// Last date where the container full   
        /// </summary>
        public DateTime? dateFull { get; set; }

        /// <summary>
        /// Contain the percentage of occupied volume of this container.
        /// </summary>
        public float currentPercOccupied { get; set; }

        public TimeSpan? setDuration()
        {
            if (avgGrowthOccupiedVolumePerDay == 0)
                return TimeSpan.FromDays(0);
            if (dateFull is null)
                return TimeSpan.FromDays((100 - currentPercOccupied) / avgGrowthOccupiedVolumePerDay);
            return dateFull - DateTime.Now;
        }
    }
}