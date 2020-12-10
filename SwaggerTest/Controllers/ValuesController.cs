using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SwaggerTest.DAL;
using SwaggerTest.Models;

namespace SwaggerTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly DataAccessContext _context;

        public ValuesController(ILogger<ValuesController> logger, DataAccessContext context)
        {
            _logger = logger;
            _context = context;
        }

        //values
        [HttpGet]
        public ValuesModel Get()
        {
            var id = 1;
            var result = _context.ValuesModels
                .Where(p => p.MicrocontrollerID == id)
                .OrderBy(d => d.DateTime)
                .LastOrDefault();
            return result;
        }

        //values/1
        [HttpGet("{id}")]
        public ValuesModel Get(int id)
        {
            var result = _context.ValuesModels
                .Where(p => p.MicrocontrollerID == id)
                .OrderBy(d => d.DateTime)
                .LastOrDefault();
            return result;
        }

        //values/1
        [HttpGet("data/{id}")]
        public IEnumerable<ValuesModel> GetLast(int id)
        {
            var result = _context.ValuesModels
                .Where(p => p.MicrocontrollerID == id)
                .OrderBy(d => d.DateTime)
                .Reverse().Take(15)
                .OrderBy(d => d.DateTime);
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
