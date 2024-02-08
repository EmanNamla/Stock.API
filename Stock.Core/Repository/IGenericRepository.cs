using Stock.Core.Entites;
using Stock.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Core.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        #region WithoutSpecification
        Task<IReadOnlyList<T>> GatAllAsync();

        Task<T> GetByIdAsync(int id);

        Task AddAsync(T item);

        void Delete(T item);

        void Update(T item);

         void Detach(T entity);
        #endregion

        #region WithSpecification
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> Spec);

        Task<T> GetByIdEntitySpecAsync(ISpecifications<T> Spec);


        #endregion

    }
}
