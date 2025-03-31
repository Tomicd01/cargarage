namespace CarGarageParking.Models.ViewModel
{
    public class EnterVehicleModel
    {
        public int GarageId { get; set; }

        public Garage Garage { get; set; }

        public string? LicensePlate { get; set; }
    }
}
