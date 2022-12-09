using System.ComponentModel.DataAnnotations.Schema;

namespace CabManagementSystem.Models
{
    public enum Locations
    {
        [Display(Name = "Vytilla Hub")]
        VytillaHub,
        [Display(Name = "Ernakulam South Railway station ")]
        ErnakulamSouthRailwaystation,
        [Display(Name = "Ernakulam North Railway station ")]
        ErnakulamNorthRailwaystation,
        [Display(Name = "Palarivattom")]
        Palarivattom,
        [Display(Name = "Edappally")]
        Edappally,
        [Display(Name = "Kakkanad")]
        Kakkanad,
        [Display(Name = "Thripunithura")]
        Thripunithura,
        [Display(Name = "Panampilly Nagar")]
        PanampillyNagar
    }

    public enum CarModel
    {
        [Display(Name = "SUV")]
        SUV,
        [Display(Name = "Sedan")]
        Sedan,
        [Display(Name = "Hatchback")]
        Hatchback,
        [Display(Name = "MUV")]
        MUV

    }
    public enum NumberOfPassengers
    {
       [Display(Name = "1")]
       One,
       [Display(Name = "2")]
       Two,
       [Display(Name = "3")]
       Three,
       [Display(Name = "4")]
       Four,
       [Display(Name = "5")]
       Five
    }
    public class Bookings
    {
        
        public int Id { get; set; }
        public Locations From { get; set; }

        public Locations To { get; set; }

        public DateTime BookingDate { get; set; }
        public NumberOfPassengers NumberOfPassengers { get; set; }

        public CarModel CarModel { get; set; }
        public ApplicationUser Users { get; set; }
        [ForeignKey(nameof(Users))]
        public string UserId { get; set; }

        public Driver? CabDriver { get; set; }
        [ForeignKey(nameof(CabDriver))]
        public int? DriverId { get; set; }

    }
}
