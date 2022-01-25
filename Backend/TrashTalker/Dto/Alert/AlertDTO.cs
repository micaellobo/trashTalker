using System;
using TrashTalker.Models.Enumerations;

namespace TrashTalker.Dto.Alert
{
    public class AlertDTO
    {
        public Guid id { get; init; }
        public string alertStatus { get; init; }
        public string alertType { get; init; }
        public DateTime date { get; set; }
        public string issue { get; set; }
        public Guid employeeId { get; set; }
    }
}