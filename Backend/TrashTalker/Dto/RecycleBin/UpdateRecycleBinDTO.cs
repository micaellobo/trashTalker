using System;
using System.ComponentModel.DataAnnotations;

namespace TrashTalker.Dto.RecycleBin
{
    public class UpdateRecycleBinDTO
    {
        [Required] public Guid id { get; set; }

        [Required] public string street { get; set; }
        [Required] public string city { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}-\d{3}$",
            ErrorMessage = "ZipCode must be properly formatted.(Ex:0000-000)")]
        public string zipCode { get; set; }

        [Required] public string country { get; set; }
    }
}