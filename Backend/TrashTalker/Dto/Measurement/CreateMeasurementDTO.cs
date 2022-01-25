using System;

namespace TrashTalker.Dto.Measurement
{
    public class CreateMeasurementDTO
    {
        //public Guid idRecycleBin { get; set; }
        public DateTime date { get; set; }
        public Guid containerId { get; set; }
        public int distance { get; set; }
    }
}