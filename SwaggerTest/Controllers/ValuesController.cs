using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
                .Reverse().Take(20)
                .OrderBy(d => d.DateTime);
            return result;
        }

        [HttpPost("notification")]
        public async Task<IActionResult> PostNotificationMessage([FromBody] NotificationModel notification)
        {
            _context.NotificationsModel.Add(notification);
            await _context.SaveChangesAsync();
            return Ok(true);
        }
    }
}
