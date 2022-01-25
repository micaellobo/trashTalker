using System;
using System.ComponentModel.DataAnnotations;

namespace TrashTalker.Dto.Container
{
    public class UpdateContainerDTO
    {
        [Required]
        [Range(3, 10000, ErrorMessage = "Please enter a value bigger than 0")]
        public float height { get; set; }
        [Required]
        [Range(3, 10000, ErrorMessage = "Please enter a value bigger than 0")]
        public float width { get; set; }
        [Required]
        [Range(3, 10000, ErrorMessage = "Please enter a value bigger than 0")]
        public float depth { get; set; }
    }
}
