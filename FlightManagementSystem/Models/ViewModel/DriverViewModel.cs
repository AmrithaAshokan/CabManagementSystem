namespace CabManagementSystem.Models.ViewModel
{
    public class DriverViewModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Car Name")]
        public string CarName { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Car Number")]
        public string CarNumber { get; set; }

        [Required]
        public CarModel CarModel { get; set; }
        [Required]
        [StringLength(25)]
        [Display(Name = "License Number")]
        public string LicenseNumber { get; set; }
    }
}
