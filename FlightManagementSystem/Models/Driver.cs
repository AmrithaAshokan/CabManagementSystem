using System.ComponentModel.DataAnnotations.Schema;

namespace CabManagementSystem.Models
{
    [Index("LicenseNumber",IsUnique =true)]
    [Index("CarNumber", IsUnique = true)]
    public class Driver
    {
        public int Id { get; set; }
        public string CarName { get; set; }
        public string CarNumber { get; set; }
        public CarModel CarModel { get; set; }
        public string LicenseNumber { get; set; }

        public ApplicationUser CabDriver { get; set; }
        [ForeignKey(nameof(CabDriver))]
        public string DriverId { get; set; }


    }
}
