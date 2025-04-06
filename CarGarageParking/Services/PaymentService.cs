using CarGarageParking.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGarageParking.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly CarGarageParkingDbContext _context;
        public PaymentService(CarGarageParkingDbContext context)
        {
            _context = context;
        }

        public void Create(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Payment payment = _context.Payments.Find(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Payment> GetAllPayments()
        {
            return _context.Payments.Include(p => p.VehicleInGarage).ToList();
        }

        public Payment GetLastPaymentByVehicleInGarageId(int vehicleInGarageId)
        {
            return _context.Payments
                .Where(p => p.VehicleInGarageId == vehicleInGarageId)
                .OrderByDescending(p => p.PaymentTime)
                .FirstOrDefault();
        }

        public Payment GetPaymentById(int id)
        {
            return _context.Payments.Find(id);
        }

        public void Update(Payment payment)
        {
            _context.Payments.Update(payment);
            _context.SaveChanges();
        }
    }
}
