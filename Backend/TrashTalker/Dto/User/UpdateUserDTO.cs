using System;
using System.ComponentModel.DataAnnotations;

namespace TrashTalker.Dto.User
{
    public class UpdateUserDTO
    {
        public string password { get; set; }
        
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string street { get; set; }

        [Required]
        public string city { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}-\d{3}$",
           ErrorMessage = "ZipCode must be properly formatted.(Ex:0000-000)")]
        public string zipCode { get; set; }

        [Required]
        public string country { get; set; }

    }
}
