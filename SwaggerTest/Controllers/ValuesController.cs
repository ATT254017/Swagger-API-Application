using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SwaggerTest.Models;

namespace SwaggerTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }

        //values
        [HttpGet]
        public ValuesModel Get()
        {
            var id = 1;
            var result = new ValuesModel();
            var url = MicrocontrollerMap.IpMapId.GetValueOrDefault(id);
            if (!String.IsNullOrEmpty(url))
            {
                result.MicrocontrollerID = id;
                result.Temperature = GetTemperature(url);
                result.DoorOpen = GetDoorOpen(url);
                result.Dust = GetDust(url);
                result.DateTime = DateTime.Now;
                result.Humidity = GetHumidity(url);
                result.Power = GetPower(url);
            }
            return result;
        }

        //values/1
        [HttpGet("{id}")]
        public ValuesModel Get(int id)
        {
            var result = new ValuesModel();
            var url = MicrocontrollerMap.IpMapId.GetValueOrDefault(id);
            if (!String.IsNullOrEmpty(url))
            {
                result.MicrocontrollerID = id;
                result.Temperature = GetTemperature(url);
                result.DoorOpen = GetDoorOpen(url);
                result.Dust = GetDust(url);
                result.DateTime = DateTime.Now;
                result.Humidity = GetHumidity(url);
                result.Power = GetPower(url);
            }
            return result;
        }

        public float GetTemperature(string url)
        {
            var response = Messenger.Get(VersionCode.V1,
                           new IPEndPoint(IPAddress.Parse(url), 161),
                           new OctetString("public"),
                           new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.8.0")) },
                           3000)[0].Data?.ToString();
            var result = !String.IsNullOrEmpty(response) ? float.Parse(response, CultureInfo.InvariantCulture.NumberFormat) : 0;
            return result;
        }

        public float GetDust(string url)
        {
            var response = Messenger.Get(VersionCode.V1,
                           new IPEndPoint(IPAddress.Parse(url), 161),
                           new OctetString("public"),
                           new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.10.0")) },
                           3000)[0].Data?.ToString();
            var result = !String.IsNullOrEmpty(response) ? float.Parse(response, CultureInfo.InvariantCulture.NumberFormat) : 0;
            return result;
        }

        public float GetHumidity(string url)
        {
            var response = Messenger.Get(VersionCode.V1,
                           new IPEndPoint(IPAddress.Parse(url), 161),
                           new OctetString("public"),
                           new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.11.0")) },
                           3000)[0].Data?.ToString();
            var result = !String.IsNullOrEmpty(response) ? float.Parse(response, CultureInfo.InvariantCulture.NumberFormat) : 0;
            return result;
        }

        public bool GetDoorOpen(string url)
        {
            var response = Messenger.Get(VersionCode.V1,
                           new IPEndPoint(IPAddress.Parse(url), 161),
                           new OctetString("public"),
                           new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.4.1.318.1.1.1.2.2.1.0")) },
                           3000)[0].Data?.ToString();
            var temp = !String.IsNullOrEmpty(response) ? Int32.Parse(response) : 0;
            var result = true;
            if (temp == 0) result = false;
            return result;
        }

        public float GetPower(string url)
        {
            var response = Messenger.Get(VersionCode.V1,
                           new IPEndPoint(IPAddress.Parse(url), 161),
                           new OctetString("public"),
                           new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.9.0")) },
                           3000)[0].Data?.ToString();
            var result = !String.IsNullOrEmpty(response) ? float.Parse(response, CultureInfo.InvariantCulture.NumberFormat) : 0;
            return result;
        }
    }
}
