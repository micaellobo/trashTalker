using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrashTalker.Models
{
    public class CollectPoint
    {
        /// <summary>
        /// <see cref="Guid"/>, the identifier of the <see cref="CollectPoint"/>
        /// </summary>
        [Key]
        public Guid id { get; set; }

        /// <summary>
        /// Order of the <see cref="RecycleBin"/> in the <see cref="Route"/>
        /// </summary>
        public int order { get; set; }

        /// <summary>
        /// <see cref="RecycleBin"/> of the <see cref="CollectPoint"/>
        /// </summary>
        public virtual RecycleBin recycleBin { get; set; }

        /// <summary>
        /// <see cref="Route"/> of the <see cref="CollectPoint"/>
        /// </summary>
        public virtual Route route { get; set; }
    }
}