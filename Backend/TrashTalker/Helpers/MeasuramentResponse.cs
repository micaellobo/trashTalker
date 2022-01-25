using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrashTalker.Helpers
{
    public class MeasuramentResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public DateTime updated_at { get; set; }
        public IList<Widget> widgets { get; set; }
    }

    public class Widget
    {
        public bool has_permission_incompatibility { get; set; }
        public bool has_type_incompatibility { get; set; }
        public int height { get; set; }
        public int height_mobile { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public Options options { get; set; }
        public string type { get; set; }
        public IList<Variable> variables { get; set; }
        public int width { get; set; }
        public int width_mobile { get; set; }
        public int x { get; set; }
        public int x_mobile { get; set; }
        public int y { get; set; }
        public int y_mobile { get; set; }
    }


    public class Options
    {
        public string icon { get; set; }
        public bool readOnly { get; set; }
        public string section { get; set; }
        public string thingId { get; set; }
    }


    public class Variable
    {
        public string id { get; set; }
        public int last_value { get; set; }
        public DateTime last_value_updated_at { get; set; }
        public string name { get; set; }
        public string permission { get; set; }
        public string thing_id { get; set; }
        public string thing_name { get; set; }
        public Thing_Timezone thing_timezone { get; set; }
        public string type { get; set; }
        public string variable_name { get; set; }
    }


    public class Thing_Timezone
    {
        public string name { get; set; }
        public int offset { get; set; }
        public DateTime until { get; set; }
    }
}