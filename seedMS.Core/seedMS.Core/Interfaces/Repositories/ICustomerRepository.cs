using seedMS.Core.DomainModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace seedMS.Core.Interfaces.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Customer> GetTopActiveCustomers(int count);
        IEnumerable<Customer> GetAllCustomersData();
    }
}
