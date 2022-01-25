using System;
using TrashTalker.Dto.Container;

namespace TrashTalker.Dto.Picking
{
    public class PickingDTO
    {
        public Guid id { get; init; }

        public float volumeRecolhido { get; init; }

        public ContainerDTO container { get; set; }
        
        public DateTime date { get; set; }
    }
}
