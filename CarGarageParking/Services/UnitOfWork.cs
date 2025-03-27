namespace CarGarageParking.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarGarageParkingDbContext _dbContext;

        public UnitOfWork(CarGarageParkingDbContext context)
        {
            _dbContext = context;
            OwnerService = new OwnerService(context);
            VehicleService = new VehicleService(context);
            GarageService = new GarageService(context);
            VehicleInGarageService = new VehicleInGarageService(context);
        }
        public IOwnerService OwnerService
        {
            get; private set;
        }

        public IVehicleService VehicleService
        {
            get; private set;
        }

        public IGarageService GarageService
        {
            get; private set;
        }

        public IVehicleInGarageService VehicleInGarageService
        {
            get; private set;
        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();   
        }
    }
}
