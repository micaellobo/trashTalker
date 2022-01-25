using System;
using System.ComponentModel.DataAnnotations;
using TrashTalker.Models.Enumerations;

namespace TrashTalker.Dto.Container
{
    public class DeactivateContainerDTO
    {
        [Required]
        public Status status { get; init; }
    }
}
