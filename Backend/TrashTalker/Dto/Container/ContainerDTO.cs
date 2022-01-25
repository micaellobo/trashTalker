using System;
using System.ComponentModel.DataAnnotations;
using TrashTalker.Models.Enumerations;

namespace TrashTalker.Dto.Container
{
    public class ContainerDTO
    {
        public Guid id { get; init; }
        public string status { get; init; }
        public string typeOfWaste { get; init; }
        public float height { get; set; }
        public float width { get; set; }
        public float depth { get; set; }
        public float percentageOccupied { get; set; }
        public double avgGrowthOccupiedVolumePerDay { get; set; }
        public DateTime? dateFull { get; set; }
        public TimeSpan? previsionOrDaysFull { get; set; } 
        public Guid recyclerBinId { get; set; }

    }
}