using System;
using System.Collections.Generic;
using TrashTalker.Models.Enumerations;

namespace TrashTalker.Models
{
    /// <summary>
    /// The RecycleBin class stores information about the recycle bin
    /// </summary>
    public class RecycleBin
    {
        /// <summary>
        /// unique identifier of the RecycleBin
        /// </summary>
        public Guid id { get; init; }

        /// <summary>
        /// <see cref="Status"/> contains the status of the RecycleBin
        /// </summary>
        public Status status { get; set; }

        /// <summary>
        /// contain the latit of the RecycleBin
        /// </summary>
        public string latit { get; set; }

        /// <summary>
        /// contain the longit of the RecycleBin
        /// </summary>
        public string longit { get; set; }

        /// <summary>
        /// contain the street of the RecycleBin
        /// </summary>
        public string street { get; set; }

        /// <summary>
        /// contain the city of the RecycleBin
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// contain the zipCode of the RecycleBin
        /// </summary>
        public string zipCode { get; set; }

        /// <summary>
        /// contain the country of the RecycleBin
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// <see cref="List{Container}"/> of <see cref="Container"/> contains the list of Container from a RecycleBin
        /// </summary>
        public virtual List<Container> containers { get; set; }

        /// <summary>
        /// <see cref="IList{CollectPoint}"/> of <see cref="CollectPoint"/> contains the list of Routes from a RecycleBin
        /// </summary>
        public virtual List<CollectPoint> routes { get; set; }
    }
}