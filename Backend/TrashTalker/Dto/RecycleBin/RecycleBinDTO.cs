using System;
using System.Collections.Generic;
using TrashTalker.Dto.Container;

namespace TrashTalker.Dto.RecycleBin
{
    public class RecycleBinDTO
    {
        public Guid id { get; set; }
        public string Status { get; set; }
        public string longit { get; set; }
        public string latit { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string zipCode { get; set; }
        public string country { get; set; }

        public IList<ContainerDTO> containerDtos { get; set; }
    }
}