using System;

namespace TrashTalker.Models
{
    /// <summary>
    /// The Measurement class stores information about the Measurement
    /// </summary>
    public class Measurement
    {
        /// <summary>
        /// unique identifier of the measurement
        /// </summary>
        public Guid id { get; set; }

        /// <summary>
        /// distance between the sensor and the obstacle
        /// </summary>
        public int distance { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> contain the date and time of the measurement
        /// </summary>
        public DateTime date { get; set; }

        /// <summary>
        /// <see cref="Container"/> container to which a particular measurement is associated
        /// </summary>
        public virtual Container container { get; set; }
    }
}