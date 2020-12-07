using System;
using System.ComponentModel.DataAnnotations;

namespace SwaggerTest.Models
{
    public class ValuesModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int MicrocontrollerID { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float Dust { get; set; }
        public bool DoorOpen { get; set; }
        public float Power { get; set; }
    }
}
