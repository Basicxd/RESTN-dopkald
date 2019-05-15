using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RESTNødopkald.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NødopkaldController : ControllerBase
    {

        private static  List<Sensor> sensorslList = new List<Sensor>()
        {
            new Sensor("2","2","none")
        };

        // GET: api/Nødopkald
        [HttpGet]
        public List<Sensor> Get()
        {
            return sensorslList;
        }

        // GET: api/Nødopkald/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Nødopkald
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Sensor value)
        {
            if (sensorslList.Contains(value))
            {
                return new HttpResponseMessage(HttpStatusCode.NotModified);
            }
            else
            {
                Sensor addingCustomer = new Sensor(value.Dato, value.Tid, value.Motion);
                sensorslList.Add(addingCustomer);
                return new HttpResponseMessage(HttpStatusCode.OK);
                
            }
        }


        // PUT: api/Nødopkald/5
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
