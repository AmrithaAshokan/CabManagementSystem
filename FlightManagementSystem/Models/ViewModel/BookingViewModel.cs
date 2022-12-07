namespace CabManagementSystem.Models.ViewModel
{
    public class BookingViewModel
    {
     
        [Required]
      
        [Display(Name = "Pick-Up Location")]
        public Locations From { get; set; }

        [Required]
       
        [Display(Name = "Drop-Off Location")]
        public Locations To { get; set; }
        
        [Required]
        [Display(Name = "Booking Date and Time")]
        public DateTime? BookingDate { get; set; }
        [Required]
        [Display(Name = "Number of Passengers")]
        public NumberOfPassengers NumberOfPassengers { get; set; }
        public CarModel CarModel { get; set; }
    }
}
