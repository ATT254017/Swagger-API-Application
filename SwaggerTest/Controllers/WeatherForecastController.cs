using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SwaggerTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("variables")]
        public IEnumerable<Variable> Get()
        {
            var result = Messenger.Get(VersionCode.V1,
                           new IPEndPoint(IPAddress.Parse("192.168.20.6"), 161),
                           new OctetString("public"),
                           new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.3.2016.5.1.0")) },
                           30000);
            return result;
        }
    }
}
