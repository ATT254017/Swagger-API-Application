using System;
using System.Collections.Generic;
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

        public int GetTemperature(string url)
        {
            var response = Messenger.Get(VersionCode.V1,
                           new IPEndPoint(IPAddress.Parse(url), 161),
                           new OctetString("public"),
                           new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.3.2016.5.1.1")) },
                           3000)[0].Data?.ToString();
            var result = !String.IsNullOrEmpty(response) ? Int32.Parse(response) : 0;
            return result;
        }

        public int GetDust(string url)
        {
            var response = Messenger.Get(VersionCode.V1,
                           new IPEndPoint(IPAddress.Parse(url), 161),
                           new OctetString("public"),
                           new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.3.2016.5.1.3")) },
                           3000)[0].Data?.ToString();
            var result = !String.IsNullOrEmpty(response) ? Int32.Parse(response) : 0;
            return result;
        }

        public int GetHumidity(string url)
        {
            var response = Messenger.Get(VersionCode.V1,
                           new IPEndPoint(IPAddress.Parse(url), 161),
                           new OctetString("public"),
                           new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.3.2016.5.1.2")) },
                           3000)[0].Data?.ToString();
            var result = !String.IsNullOrEmpty(response) ? Int32.Parse(response) : 0;
            return result;
        }

        public bool GetDoorOpen(string url)
        {
            var response = Messenger.Get(VersionCode.V1,
                           new IPEndPoint(IPAddress.Parse(url), 161),
                           new OctetString("public"),
                           new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.3.2016.5.1.4")) },
                           3000)[0].Data?.ToString();
            var temp = !String.IsNullOrEmpty(response) ? Int32.Parse(response) : 0;
            var result = true;
            if (temp == 0) result = false;
            return result;
        }

        public int GetPower(string url)
        {
            var response = Messenger.Get(VersionCode.V1,
                           new IPEndPoint(IPAddress.Parse(url), 161),
                           new OctetString("public"),
                           new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.3.2016.5.1.5")) },
                           3000)[0].Data?.ToString();
            var result = !String.IsNullOrEmpty(response) ? Int32.Parse(response) : 0;
            return result;
        }
    }
}
