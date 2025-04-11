namespace CarGarageParking.Models.ViewModel
{
    public class PaginationViewModel<T> : IPaginationViewModel<T> where T : class
    {
        //public IEnumerable<Garage> Garages { get; set; }

        public IEnumerable<T> Colection { get; set; }

        public int TotalCount { get; set; }

        public int PageSize { get; set; }

        public IEnumerable<ApplicationRegistrationViewModel>? arvm { get; set; }

        public int TotalPages
        {
            get
            {
                int totalP = TotalCount / PageSize;
                if (TotalCount % PageSize != 0)
                {
                    totalP += 1;
                }
                return totalP;
            }
        }

        public int CurrentPage { get; set; }

        public bool HasPrevious
        {
            get
            {
                return CurrentPage > 1;
            }
        }

        public bool HasNext
        {
            get
            {
                return CurrentPage < TotalPages;
            }
        }

       
    }
}
