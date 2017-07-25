using Microsoft.EntityFrameworkCore;
using seedMS.Core.Data.Repositories;
using seedMS.Core.DomainModels.Repositories;
using seedMS.Core.Interfaces.Repositories;

namespace seedMS.Core.Extensions.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        { }

        private CoreRepositoriesDbContext appContext
        {
            get { return (CoreRepositoriesDbContext)_context; }
        }
    }
}