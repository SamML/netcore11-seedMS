using seedMS.Core.DomainModels.Repositories;
using System.Collections.Generic;

namespace seedMS.Core.Interfaces.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Customer> GetTopActiveCustomers(int count);

        IEnumerable<Customer> GetAllCustomersData();
    }
}