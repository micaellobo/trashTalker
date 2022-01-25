using System;
using System.ComponentModel.DataAnnotations;
using TrashTalker.Models;
using TrashTalker.Models.Enumerations;

namespace TrashTalker.Dto.User
{
    public class UserDTO
    {
        public Guid id { get; init; }

        public string status { get; init; }

        public string username { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string gender { get; set; }

        public string street { get; set; }

        public string city { get; set; }

        public string zipCode { get; set; }

        public string country { get; set; }

        public string role { get; set; }
    }
}