using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using spektro_api.Data;
using spektro_api.Model;

namespace spektro_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BurgersController : ControllerBase
    {
        // GET: api/Burgers
        [HttpGet]
        public IEnumerable<List<Burger>> Get()
        {
            yield return StaticDatas.burgers;
        }

        // GET: api/Burgers/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Burgers
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Burgers/5
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
