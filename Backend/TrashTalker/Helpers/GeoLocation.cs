using System;
namespace TrashTalker.Helpers
{
    public class GeoLocation
    {
        public int ID { get; set; }
        public string CodigoPostal { get; set; }
        public string Morada { get; set; }
        public string Localidade { get; set; }
        public string NumeroPorta { get; set; }
        public string Freguesia { get; set; }
        public string Concelho { get; set; }
        public int CodigoDistrito { get; set; }
        public string Distrito { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
