using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TrashTalker.Models.Enumerations;

namespace TrashTalker.Models
{
    /// <summary>
    /// The Route class stores information about the Route
    /// </summary>
    public class Route
    {
        /// <summary>
        /// unique identifier of the Route
        /// </summary>
        public Guid id { get; init; }

        /// <summary>
        /// contain the name of the Route
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// <see cref="StatusRoute"/> contains the status of the Route
        /// </summary>
        public StatusRoute status { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> contain the date criation of the Route
        /// </summary>
        public DateTime dateCriation { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> contain the start date of the route
        /// </summary>
        public DateTime dateBegin { get; set; }

        /// <summary>
        /// contain the end date of the route
        /// </summary>
        public DateTime? dateEnd { get; set; }

        /// <summary>
        /// contains the estimated duration of the route
        /// </summary>
        public TimeSpan estimatedDuration { get; set; }

        /// <summary>
        /// contains the estimated distance in km of the route
        /// </summary>
        public int distanceEstimatedKm { get; set; }

        /// <summary>
        /// contains the distance traveled in km of a route
        /// </summary>
        public float? distanceTravelledKm { get; set; }

        /// <summary>
        /// <see cref="List{CollectPoint}"/> of <see cref="CollectPoint"/> Contains the list of RecycleBin from a Route
        /// </summary>
        public virtual List<CollectPoint> collectPoints { get; set; }

        /// <summary>
        /// The Employee associated to the <see cref="Route"/>  
        /// </summary>
        [Required]
        public virtual User employee { get; set; }

        /// <summary>
        /// The type of <see cref="typeCreation"/> of the <see cref="Route"/>
        /// </summary>
        public TypeCreation typeCreation { get; set; }

        /// <summary>
        /// Gets the duration of a route
        /// </summary>
        /// <returns>duration of a route, (end date - start date)</returns>
        public TimeSpan? getDuration()
        {
            return dateEnd - dateBegin;
        }
    }
}