using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface ICustomerServiceRepository : IBaseRepository
    {
        void AddComplain(CustomerService customerService);
        ICollection<CustomerService> GetComplains();
        ICollection<CustomerService> GetMyComplains(int userId);
        void SetResponse(int complaineId);
    }
}
