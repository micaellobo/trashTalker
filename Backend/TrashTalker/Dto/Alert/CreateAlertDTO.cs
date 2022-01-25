using System;
using System.ComponentModel.DataAnnotations;
using TrashTalker.Models.Enumerations;

namespace TrashTalker.Dto.Alert
{
    public class CreateAlertDTO
    {
        //public Guid id { get; set; }
        [Required]
        public string issue { get; set; }
        
        [Required]
        [RegularExpression(@"^(?i)(CONTAINER|SENSOR|SYSTEM_FAILURE|OTHER)$", ErrorMessage = "Only are allowed CONTAINER, SENSOR, SYSTEM_FAILURE and OTHER")]
        public string alertType { get; set; }
        
        //public Guid employeeId { get; set; }
    }
}