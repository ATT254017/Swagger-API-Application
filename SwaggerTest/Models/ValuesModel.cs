using System;

namespace SwaggerTest.Models
{
    public class ValuesModel
    {
        public DateTime DateTime { get; set; }
        public int MicrocontrollerID { get; set; }
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public int Dust { get; set; }
        public bool DoorOpen { get; set; }
        public int Power { get; set; }
    }
}
