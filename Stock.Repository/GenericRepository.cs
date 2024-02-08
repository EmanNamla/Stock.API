using Microsoft.EntityFrameworkCore;
using Stock.Core.Entites;
using Stock.Core.Repository;
using Stock.Core.Specifications;
using Stock.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StockDbContext dbContext;

        public GenericRepository(StockDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        #region WithoutSpecification
        public async Task<IReadOnlyList<T>> GatAllAsync()
        
          =>await  dbContext.Set<T>().ToListAsync();
        
        public async Task<T> GetByIdAsync(int id)
        
         =>  await dbContext.Set<T>().FindAsync(id);
        
        public async Task AddAsync(T item)
        {
           await dbContext.Set<T>().AddAsync(item);
        }

        public void Update(T item)
        {
             dbContext.Set<T>().Update(item);
        }
        public void Delete(T item)
        {
            dbContext.Set<T>().Remove(item);
        }


        public void Detach(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entry = dbContext.Entry(entity);
            if (entry != null)
            {
                entry.State = EntityState.Detached;
            }
        }

        #endregion

        #region WithSpecification

        private IQueryable<T> ApplySpecification(ISpecifications<T> Spec)
        {
            return SpecificationEvalutor<T>.GetQuery(dbContext.Set<T>(), Spec);
        }
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> Spec)
        {
            return await  ApplySpecification(Spec).ToListAsync();
        }


        public async Task<T> GetByIdEntitySpecAsync(ISpecifications<T> Spec)
        {
            return await ApplySpecification(Spec).FirstOrDefaultAsync();
        } 
        #endregion



    }
}
