using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TrashTalker.Helpers
{
    public class DistanceMatrix
    {
        public IList<string> destination_addresses { get; set; }
        public IList<string> origin_addresses { get; set; }
        public IList<Row> rows { get; set; }
        public string status { get; set; }
    }

    public class Row
    {
        public IList<Element> elements { get; set; }
    }

    public class Element
    {
        public Distance distance { get; set; }
        public Duration duration { get; set; }
        public string status { get; set; }
    }

    public class Distance
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class Duration
    {
        public string text { get; set; }
        public int value { get; set; }
    }
}