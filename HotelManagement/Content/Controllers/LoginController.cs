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
    public class LoginController : ApiController
    {
        finalprojectdbEntities dalobj = new finalprojectdbEntities();
        T_Users user = new T_Users();
        public LoginController()
        {
            dalobj.Configuration.ProxyCreationEnabled = false;
        }


        // GET: api/PassHistory
        [Route("api/PassHistory")]
        public IEnumerable<T_PasswordHistoryLog> Get()
        {
            IEnumerable<T_PasswordHistoryLog> PList = dalobj.T_PasswordHistoryLog.ToList();
            return PList;
        }


        // POST: api/Login
        [HttpPost]
        [Route("api/Login")]
        public Response Post([FromBody]T_Users login)
        {
            Response response = new Response();
            if (login.EmailId!=null && login.Password!=null)
            {
                var listuser = dalobj.T_Users.ToList();
                T_Users validUser = (from user in listuser
                                     where user.EmailId == login.EmailId &&
                                     user.Password == login.Password
                                     select user).SingleOrDefault();

                if (validUser != null)
                {
                    response.Data = validUser;
                    response.Error = null;
                    response.Status = "Success";
                    return response;
                }
                else
                {
                    response.Error = null;
                    response.Status = "Incorrect Credentials";
                    return response;
                }
            }
            else
            {
                response.Error = null;
                response.Status = "Fields are empty";
                return response;
            } 
        }
        
    }
}
