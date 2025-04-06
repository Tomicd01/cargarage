using CarGarageParking.Models;

namespace CarGarageParking.Services
{
    public interface IPaymentService
    {
        IEnumerable<Payment> GetAllPayments();
        Payment GetPaymentById(int id);
        void Create(Payment payment);
        void Update(Payment payment);
        void Delete(int id);
        Payment GetLastPaymentByVehicleInGarageId(int vehicleInGarageId);

    }
}
