namespace CarGarageParking.Models
{
    public class EnterVehicleModel
    {
        public int GarageId { get; set; }

        public string GarageName { get; set; }

        public string GarageLocation { get; set; }

        public string? LicensePlate { get; set; }
    }
}
