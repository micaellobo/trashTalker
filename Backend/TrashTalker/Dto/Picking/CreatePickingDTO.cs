using System;
using System.ComponentModel.DataAnnotations;

namespace TrashTalker.Dto.Picking
{
    public class CreatePickingDTO
    {
        [Required]
        public float volumeRecolhido { get; init; }

        [Required]
        public Guid containerId { get; set; }
        
        [Required]
        public DateTime date { get; set; }
    }
}
