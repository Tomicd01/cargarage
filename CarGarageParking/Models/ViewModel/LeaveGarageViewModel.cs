namespace CarGarageParking.Models.ViewModel
{
    public class LeaveGarageViewModel
    {
        public Vehicle Vehicle { get; set; }
        public VehicleInGarage VehicleInGarage { get; set; }
        public Owner? Owner { get; set; }
        public Application? Application { get; set; }
        public Garage Garage { get; set; }
        public Payment Payment { get; set; }
        public decimal Discount { get; set; }   

    }
}
