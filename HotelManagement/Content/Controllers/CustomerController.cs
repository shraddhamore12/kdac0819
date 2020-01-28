using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HotelManagement.Models;
using System.Web.Http.Cors;

namespace HotelManagement.Content.Controllers
{
    // Allow CORS for all origins. (Caution!)
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class CustomerController : ApiController
    {

        finalprojectdbEntities dalobj = new finalprojectdbEntities();

        public CustomerController()
        {
            dalobj.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Customer
        public IEnumerable<String> Get()
        {
            IEnumerable<T_HotelDetails> CList = dalobj.T_HotelDetails.ToList();
            var cities = (from c in CList
                          select c.City).Distinct().ToList();
            return cities;
        }


        // GET: api/Customer/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Customer
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Customer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Customer/5
        public void Delete(int id)
        {
        }
    }
}
