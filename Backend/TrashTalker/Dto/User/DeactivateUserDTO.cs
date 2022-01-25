using System.ComponentModel.DataAnnotations;
using TrashTalker.Models.Enumerations;

namespace TrashTalker.Dto.User
{
    public class DeactivateUserDTO
    {
        [Required]
        public Status status { get; init; }
    }
}
