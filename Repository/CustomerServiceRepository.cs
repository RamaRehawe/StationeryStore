using StationeryStore.Data;
using StationeryStore.Interfaces;
using StationeryStore.Models;

namespace StationeryStore.Repository
{
    public class CustomerServiceRepository : BaseRepository, ICustomerServiceRepository
    {
        public CustomerServiceRepository(DataContext context) : base(context)
        {
        }

        public void AddComplain(CustomerService customerService)
        {
            var complain = _context.CustomerServices.Add(customerService);
           
            _context.SaveChanges();
        }

        public ICollection<CustomerService> GetComplains()
        {
            return _context.CustomerServices.OrderBy(cs => cs.Id).ToList();
        }

        public ICollection<CustomerService> GetMyComplains(int userId)
        {
            return _context.CustomerServices.Where(cs => cs.UserId == userId).ToList();
        }

        public void SetResponse(int complaineId)
        {
            var complain = _context.CustomerServices.Where(cs => cs.Id == complaineId).FirstOrDefault();
            if (complain != null)
            {
                complain.AdminResponse = true;
                _context.SaveChanges();
            }
        }
    }
}
