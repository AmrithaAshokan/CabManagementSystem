﻿namespace CabManagementSystem.Models
{
    public class ApplicationUser:IdentityUser
    {
        [StringLength(15)]
        public string FirstName { get; set; }

        [StringLength(15)]
        public string LastName { get; set; }

        public IEnumerable<Bookings> BookingCabs { get; set; }
    }
}
