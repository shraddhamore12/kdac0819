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
    // Allow CORS for all origins. (Caution!)
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class HotelsController : ApiController
    {
        finalprojectdbEntities dalobj = new finalprojectdbEntities();

        public HotelsController()
        {
            dalobj.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Hotels
        public IEnumerable<T_HotelDetails> Get()
        {
            IEnumerable<T_HotelDetails> HList = dalobj.T_HotelDetails.ToList();
            return HList;
        }

        // POST: api/Hotels

        [HttpPost]
        [Route("api/HotelsList")]
        public Response Post([FromBody]T_HotelDetails hotels)
        {
            Response response = new Response();
            if (hotels != null)
            {
                dalobj.T_HotelDetails.Add(hotels);
                dalobj.SaveChanges();
                response.Error = null;
                response.Status = "Successfully Added !!!";
                return response;
            }
            else
            {
                response.Error = null;
                response.Status = "Enter correct data!!!";
                return response;
            }
        }

        // GET: api/Hotels/5
        public Response Get(int id)
        {
            Response response = new Response();
            T_HotelDetails hotel = dalobj.T_HotelDetails.Find(id);
            response.Data = hotel;
            return response;
        }

        //Getting hotels based on search
        [HttpPost]
        [Route("api/Hotel/FindHotel")]
        public Response FindHotel([FromBody]T_HotelDetails value)
        {
            Response response = new Response();

            var hotellist = dalobj.T_HotelDetails.ToList();
            /* IEnumerable<T_TrainInfo> trains*/
            var hotels = (from t in hotellist
                          where t.City == value.City
                          select t).ToList();
            /*&& t.Date == value.Date*/

            if (hotels != null)
            {
                response.Status = "Success";
                response.Data = hotels;
                response.Error = null;
            }
            else
            {
                response.Status = "Something went wrong";
                response.Data = null;
                response.Error = null;
            }
            return response;
        }

        //Getting hotels based on search
        [HttpPost]
        [Route("api/Hotel/FindRoom")]
        public Response FindRoom([FromBody]T_HotelRoomDetails value)
        {
            Response response = new Response();

            List< T_HotelRoomDetails> roomlist = dalobj.T_HotelRoomDetails.ToList();
            List<T_HotelDetails> hotelList = dalobj.T_HotelDetails.ToList();
            /* IEnumerable<T_TrainInfo> trains*/
            var rooms = from hotel in hotelList
                         join room in roomlist on hotel.HId equals room.HId
                         where hotel.HId == value.HId
                         select new { hotel.HId, hotel.HName, room.RDetId, room.Roomtype, room.Rate,room.Status};
            /*&& t.Date == value.Date*/

            if (rooms != null)
            {
                response.Status = "Success";
                response.Data = rooms;
                response.Error = null;
            }
            else
            {
                response.Status = "Something went wrong";
                response.Data = null;
                response.Error = null;
            }
            return response;
        }

        [HttpPut]
        [Route("api/Hotels/UpdateStatus/{id}")]
        public Response UpdateStatus(int id,[FromBody] T_HotelRoomDetails roomobj)
        {
            Response response = new Response();

            T_HotelRoomDetails roomd = dalobj.T_HotelRoomDetails.Find(id);
            roomd.Status = roomobj.Status;
             dalobj.SaveChanges();
            response.Status = "Success";       
            return response;
        }

        // PUT: api/Hotels/5
        public Response Put(int id, [FromBody]T_HotelDetails hotel)
        {
            Response response = new Response();
            T_HotelDetails hotelToBeUpdated = dalobj.T_HotelDetails.Find(id);
            hotelToBeUpdated.HName = hotel.HName;
            hotelToBeUpdated.HAddress = hotel.HAddress;
            hotelToBeUpdated.City = hotel.City;
            hotelToBeUpdated.NRooms = hotel.NRooms;
            hotelToBeUpdated.ACRooms = hotel.ACRooms;
            hotelToBeUpdated.NonACRooms = hotel.NonACRooms;
            dalobj.SaveChanges();
            response.Status = "Hotel Details Updated successfully";
            return response;
        }

        // DELETE: api/Hotels/5
        [Route("api/hotels/delete")]
        public Response Delete(int id)
        {
            Response rs = new Response();

            T_HotelDetails hd = dalobj.T_HotelDetails.Find(id);

            if(hd != null)
            {
                dalobj.T_HotelDetails.Remove(hd);
                dalobj.SaveChanges();
                rs.Status = "Hotel deleted successfully";
                rs.Error = null;
            }
            else
            {
                rs.Error = null;
                rs.Status = "There is no Hotel with this Id";
            }
            return rs;

            

        }
    }
}
