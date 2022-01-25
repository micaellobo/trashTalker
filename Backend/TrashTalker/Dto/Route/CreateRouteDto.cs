using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using TrashTalker.Helpers;

namespace TrashTalker.Dto.Route
{
    public class CreateRouteDto
    {
        [Required, RegularExpression(@"^[a-zA-Z \d]{5,}$",
             ErrorMessage = "Invalide name, only letters and numbers are allowed, with minimum 5 characters")]
        public string name { get; set; }

        [Required(AllowEmptyStrings = false), DateFromNow(1)]
        public DateTime dateBegin { get; set; }

        [Required, MinLength(2, ErrorMessage = "The Route most be most have more than 2 RecycleBins")]
        public IList<Guid> recycleBinIds { get; set; }

        [NonEmptyGuidAttribute(ErrorMessage = "The Employee is Required")] public Guid employeeId { get; set; }
    }
}