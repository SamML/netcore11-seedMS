using Microsoft.EntityFrameworkCore;
using seedMS.Core.Data.Repositories;
using seedMS.Core.DomainModels.Repositories;
using seedMS.Core.Interfaces.Repositories;

namespace seedMS.Core.Extensions.Repositories
{
    public class OrdersRepository : Repository<Order>, IOrdersRepository
    {
        public OrdersRepository(DbContext context) : base(context)
        { }

        private CoreRepositoriesDbContext appContext
        {
            get { return (CoreRepositoriesDbContext)_context; }
        }
    }
}