using Ecommerce.Core.IRepositories;
using Ecommerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly AppDbContext dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            productRepository = new ProductRepository(dbContext);
        }

        public IProductRepository productRepository { get; set; }

        // Task<int> ==> Asynchronously commits all changes to the database
        //               (such as insert, update, or delete)
        //               and returns the number of rows affected by those changes.
        public async Task<int> save()
           => await dbContext.SaveChangesAsync();
    }
}
