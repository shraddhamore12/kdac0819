//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotelManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_HotelRoomDetails
    {
        public T_HotelRoomDetails()
        {
            this.T_CustomerBooking = new HashSet<T_CustomerBooking>();
        }
    
        public int RDetId { get; set; }
        public int HId { get; set; }
        public string Roomtype { get; set; }
        public double Rate { get; set; }
        public string Status { get; set; }
    
        public virtual ICollection<T_CustomerBooking> T_CustomerBooking { get; set; }
        public virtual T_HotelDetails T_HotelDetails { get; set; }
    }
}
