using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HotelManagement.Models;
using System.Web.Http.Cors;

namespace HotelManagement.Controllers
{
    // Allow CORS for all origins. (Caution!)
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoleController : ApiController
    {
        public RoleController()
        {
            dalobj.Configuration.ProxyCreationEnabled = false;
        }
        finalprojectdbEntities dalobj = new finalprojectdbEntities();
        // GET: api/Role
        public IEnumerable<T_Roles> Get()
        {
            IEnumerable<T_Roles> RList = dalobj.T_Roles.ToList();
            return RList;
            //return new string[] { "value1", "value2" };
        }

        // GET: api/Role/5
        public T_Roles Get(int id)
        {
            return dalobj.T_Roles.Find(id);
        }

        // POST: api/Role
        public void Post([FromBody]T_Roles value)
        {
            dalobj.T_Roles.Add(value);
            dalobj.SaveChanges();
        }

        // PUT: api/Role/5
        public void Put(int id, [FromBody]T_Roles value)
        {
            T_Roles r = dalobj.T_Roles.Find(id);
            r.RoleName = value.RoleName;
            dalobj.SaveChanges();
        }

        // DELETE: api/Role/5
        public void Delete(int id)
        {
            T_Roles r = dalobj.T_Roles.Find(id);
            dalobj.T_Roles.Remove(r);
            dalobj.SaveChanges();
        }
    }
}
