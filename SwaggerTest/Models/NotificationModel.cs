using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerTest.Models
{
    public class NotificationModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int MicrocontrollerID { get; set; }
        public string NotificationDetails { get; set; }
    }
}
