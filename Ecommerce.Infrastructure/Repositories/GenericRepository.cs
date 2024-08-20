using Ecommerce.Core.Entities;
using Ecommerce.Core.IRepositories;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task Create(T model)
        {
            await dbContext.Set<T>().AddAsync(model);
        }

        public void Delete(int id)
        {
            //dbContext.Remove(id);
            var entity = dbContext.Set<T>().Find(id);
            if (entity != null)
            {
                dbContext.Remove(entity);
            }
            else
            {
                throw new Exception("Entity not found.");
            }
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, int pageSize = 2, int pageNumber = 1, string? includeProperty = null)
        {
            //if(typeof(T) == typeof(Products))
            //{
            //    var model = await dbContext.Products.Include(x => x.Category).ToListAsync();
            //    return (IEnumerable<T>)model;
            //}

            //return await dbContext.Set<T>().ToListAsync();


            // Pagination:
            IQueryable<T> query = dbContext.Set<T>();

            if(filter != null)
            {
                query = query.Where(filter);
            }
            if(includeProperty != null)
            {
                query = query.Include(includeProperty);
            }
            /*
            if(includeProperty != null)
            {
                // includeProperty = "Category, Order"      ==>  Here when I have more than one (related data)
                foreach(var property in includeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property.Trim());
                }
            }*/


            if (pageSize > 0)
            {
                if(pageSize > 4)
                {
                    pageSize = 4;
                }
                query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public void Update(T model)
        {
            dbContext.Set<T>().Update(model);
        }
    }
}
