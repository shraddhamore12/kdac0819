using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using HotelManagement.Models;
using System.Web.Http.Cors;
using System.Net.Mail;

namespace HotelManagement.Controllers
{
    // Allow CORS for all origins. (Caution!)
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        //  finalprojectdbEntities dalobj = new finalprojectdbEntities();
    finalprojectdbEntities dalobj = new finalprojectdbEntities();
        public UserController()
        {
            dalobj.Configuration.ProxyCreationEnabled = false;
        }
       // finalprojectdbEntities dalobj = new finalprojectdbEntities();
        // GET: api/User
        public IEnumerable<T_Users> Get()
        {
            IEnumerable<T_Users> UList = dalobj.T_Users.ToList();
            return UList;
            // return new string[] { "value1", "value2" };
        }


        // GET: api/User/5
        public T_Users Get(int id)
        {
            return dalobj.T_Users.Find(id);
        }

        // POST: api/User
        [HttpPost]
        [Route("api/Register")]
        public Response Register([FromBody]T_Users value)
        {
            Response response = new Response();
            value.RoleId = 2;
            if (value!=null)
            {
                dalobj.T_Users.Add(value);
                dalobj.SaveChanges();
                response.Error = null;
                response.Status = "Successfull register";
                return response;
            }
            else
            {
                response.Error = null;
                response.Status = "plz enter valid data";
                return response;
            }
            
         
        }



        [HttpPost]
        [Route("api/User/OTP")]
        public Response OTP([FromBody]dynamic otpDetails)
        {
            string email = otpDetails.EmailId.ToString();


            var userlist = dalobj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == email
                        select u).SingleOrDefault();
            string o = otpDetails.OTP.ToString();

            var otpd = dalobj.T_OTP_Details.ToList();
            var vOTP = (from v in otpd
                        where v.UserId == User.UserId && v.OTP == o
                        select v).SingleOrDefault();

            if (vOTP != null)
            {
                Response RC = new Response();
                RC.Status = "success";
                RC.Error = null;
                RC.Data = vOTP;
                return RC;
            }
            else
            {
                Response RC = new Response();
                RC.Status = "fail";
                RC.Error = null;
                RC.Data = false;
                return RC;
            }
        }


        [HttpPost]
        [Route("api/User/IsExist")]
        public Response IsExist([FromBody]T_Users value)
        {

            var userlist = dalobj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == value.EmailId
                        select u).SingleOrDefault();
            if (User != null)
            {
                Response RC = new Response();
                string mails = GetOTP();

                T_OTP_Details otp = new T_OTP_Details();
                otp.UserId = User.UserId;
                otp.ValidTill = DateTime.Now;
                otp.GeneratedOn = DateTime.Now;
                otp.OTP = mails;
                dalobj.T_OTP_Details.Add(otp);
                

                Email email = new Email();
                email.to = User.EmailId;
                email.subject = "otp";
                email.body = "Ur OTP is" + mails;
                dalobj.SaveChanges();
                var result = SendEmail(email);

                RC.Status = "success";
                RC.Error = null;
                RC.Data = mails;
                return RC;
            }
            else
            {
                Response RC = new Response();
                RC.Status = "fail";
                RC.Error = null;
                RC.Data = false;
                return RC;
            }

        }


        [HttpPut]
        [Route("api/User/UpdatePassword")]
        public Response updatepassword([FromBody]T_Users value)
        {

            var userlist = dalobj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == value.EmailId
                        select u).SingleOrDefault();

            if (User != null)
            {
                User.Password = value.Password;
                dalobj.SaveChanges();
                Response rc = new Response();
                rc.Status = "success";
                rc.Error = null;
                rc.Data = User;
                return rc;
            }
            else
            {
                Response rc = new Response();
                rc.Status = "fail";
                rc.Error = null;
                rc.Data = null;
                return rc;
            }
        }

        private string GetOTP(string otpType = "1", int len = 5)
        {
            //otptype 1 = alpha numeric
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
            if (otpType == "1")
            {
                characters += alphabets + small_alphabets + numbers;
            }
            int length = 5;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }


        [HttpPost]
        [Route("api/User/SendEmail")]
        public Response SendEmail([FromBody]Email e)
        {
            string to = e.to;
            string subject = e.subject;
            string body = e.body;
            Response res = new Response();
            string result = "Message Sent Successfully..!!";
            string senderID = "moreshraddhasudhakar@gmail.com";// use sender’s email id here..
            const string senderPassword = "shraddha"; // sender password here…
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com", // smtp server address here…
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                    Timeout = 30000,
                };
                MailMessage message = new MailMessage(senderID, to, subject, body);
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                result = "Error sending email.!!!";
                res.Error = ex;
            }
            return res;

        }

    }




}


