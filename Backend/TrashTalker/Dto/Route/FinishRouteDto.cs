using System;
using System.ComponentModel.DataAnnotations;

namespace TrashTalker.Dto.Route
{
    public class FinishRouteDto
    {
        [Required] [Range(1, Int32.MaxValue)] public int distanceTravelledKm { get; set; }
    }
}