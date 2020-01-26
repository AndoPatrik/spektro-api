using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spektro_API_Azure.Service;

namespace Spektro_API_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTestController : ControllerBase
    {
        // GET: api/EmailTest
        [HttpGet]
        public void Get()
        {
            DynamicEmailSender.SendCustomEmail("fname","lname","screadern@gmail.com",DateTime.Now,"COUPON","Welcome","text");
        }

        // GET: api/EmailTest/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/EmailTest
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/EmailTest/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
