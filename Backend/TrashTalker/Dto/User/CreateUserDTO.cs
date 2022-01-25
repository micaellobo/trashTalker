using System;
using System.ComponentModel.DataAnnotations;

namespace TrashTalker.Dto.User
{
    public class CreateUserDTO 
    {
        [Required]
        public string username { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "The field password must be a string type with a minimum length of 5.")]
        public string password { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        
        [Required]
        [RegularExpression(@"^(?i)(MALE|FEMALE|OTHER)$", ErrorMessage = "Only are alowed MALE, FEMALE and OTHER")]
        public string gender { get; set; }

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

        [Required]
        [RegularExpression(@"^(?i)(EMPLOYEE|MANAGER|ADMIN)$", ErrorMessage = "Only are alowed this roles: EMPLOYEE, MANAGER")]
        public string role { get; set; }
    }

}
