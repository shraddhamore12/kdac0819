using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HotelManagement.Content.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoomController : ApiController
    {
        finalprojectdbEntities dalobj = new finalprojectdbEntities();
        Response res = new Response();

        public RoomController()
        {
            dalobj.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Room
        public Response Get()
        {
            T_HotelRoomDetails obj;
            List<T_HotelRoomDetails> allRooms = dalobj.T_HotelRoomDetails.Include("T_HotelDetails").ToList();
            res.Data = allRooms;
            return res;

        }

        // GET: api/Room/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Room
       
        [Route("api/RoomStatus")]
        public Response Post(int rid)
        {
            T_HotelRoomDetails getroom = dalobj.T_HotelRoomDetails.Find(rid);
            getroom.Status = "Booked";
            dalobj.SaveChanges();
            res.Status = "Success";
            return res;
        }


        // PUT: api/Room/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Room/5
        public void Delete(int id)
        {
        }
    }
}
