using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TrashTalker.Helpers;

namespace TrashTalker.Dto.Route
{
    public class CreateAutomaticRoute
    {
        //1 / 24 = 1 hora 
        [Required, DateFromNow(1 / 24.0)] public DateTime dateBegin { get; set; }

        [NonEmptyGuidAttribute(ErrorMessage = "The Employee is Required")]
        public Guid employeeId { get; set; }
    }
}