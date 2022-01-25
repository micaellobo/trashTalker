using System;
using System.ComponentModel.DataAnnotations;

namespace TrashTalker.Dto.Container
{
    public class CreateContainerDTO
    {
        [Required]
        [RegularExpression(@"^(?i)(PAPER|GLASS|PLASTIC|UNDIFFERENTIATED)$",
            ErrorMessage = "The type of waste must be paper,glass,plastic or undifferentiated")]
        public string typeOfWaste { get; init; }

        [Required]
        [Range(3, 10000, ErrorMessage = "Please enter a value bigger than 0")]
        public float height { get; set; }

        [Required]
        [Range(3, 10000, ErrorMessage = "Please enter a value bigger than 0")]
        public float width { get; set; }

        [Required]
        [Range(3, float.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        public float depth { get; set; }

        [Required] public Guid idRecBin { get; set; }
    }
}