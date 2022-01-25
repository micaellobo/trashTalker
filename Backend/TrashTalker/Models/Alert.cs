using System;
using TrashTalker.Models.Enumerations;

namespace TrashTalker.Models
{
    /// <summary>
    /// This class represents a Alert Entity
    /// </summary>
    public class Alert
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public Guid id { get; set; }
        
        /// <summary>
        /// Issue or problem found
        /// </summary>
        public string issue { get; set; }
        
        /// <summary>
        /// Status of the alert
        /// </summary>
        public AlertStatus alertStatus { get; set; }
        
        /// <summary>
        /// Type of the alert
        /// </summary>
        public AlertType alertType { get; set; }
        
        /// <summary>
        /// Date the alert was created
        /// </summary>
        public DateTime date { get; set; }
        
        /// <summary>
        /// Responsible employee
        /// </summary>
        public virtual User employee { get; set; }
    }
}