namespace CarGarageParking.Models.ViewModel
{
    public interface IPaginationViewModel<T> where T : class
    {
        public int TotalPages { get; }
        public int CurrentPage { get; set; }
        public bool HasPrevious { get;  }
        public bool HasNext { get; } 
        public IEnumerable<T> Colection { get; set; }
        public IEnumerable<ApplicationRegistrationViewModel>? arvm { get; set; }
    }
}
