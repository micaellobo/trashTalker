using System;
using System.Collections.Generic;
using TrashTalker.Models.Enumerations;

namespace TrashTalker.Models
{
    /// <summary>
    /// The User class stores information about the User
    /// </summary>
    public class User
    {
        /// <summary>
        /// unique identifier of the User
        /// </summary>
        public Guid id { get; set; }

        /// <summary>
        /// contain the username of the User
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// contain the password of the User
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// <see cref="Role"/> contain the role of the User
        /// </summary>
        public Role role { get; init; }

        /// <summary>
        /// contain the firstName of the User
        /// </summary>
        public string firstName { get; set; }

        /// <summary>
        /// contain the lastName of the User
        /// </summary>
        public string lastName { get; set; }

        /// <summary>
        /// contain the email of the User
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// <see cref="Gender"/> contain the gender of the User
        /// </summary>
        public Gender gender { get; set; }

        /// <summary>
        /// <see cref="Status"/> contains the status of the container
        /// </summary>
        public Status status { get; set; }

        /// <summary>
        /// contain the street of the User
        /// </summary>
        public string street { get; set; }

        /// <summary>
        /// contain the city of the User
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// contain the zipCode of the User
        /// </summary>
        public string zipCode { get; set; }

        /// <summary>
        /// contain the country of the User
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// List of routes that the employee is responsible for
        /// </summary>
        public virtual IList<Route> routes { get; set; }
    }
}