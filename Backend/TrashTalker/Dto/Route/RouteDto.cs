using System;
using System.Collections.Generic;
using TrashTalker.Dto.RecycleBin;
using TrashTalker.Dto.User;

namespace TrashTalker.Dto.Route
{
    public class RouteDto
    {
        public Guid id { get; set; }

        public string name { get; set; }

        public string status { get; set; }

        public string typeCreation { get; set; }

        public DateTime dateCriation { get; set; }

        public string? duration { get; set; }
        public DateTime dateBegin { get; set; }

        public DateTime? dateEnd { get; set; }

        public string estimatedDuration { get; set; }

        public int distanceEstimatedKm { get; set; }

        public float? distanceTravelledKm { get; set; }


       public IDictionary<string, string> startingPoint { get; set; }

        public IList<RecycleBinDTO> recycleBins { get; set; }

        public UserDTO employee { get; set; }
    }
}