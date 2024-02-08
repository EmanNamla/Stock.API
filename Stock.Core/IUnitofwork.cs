using Stock.Core.Entites;
using Stock.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Core
{
    public interface IUnitofwork:IAsyncDisposable
    {
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        public Task<int> CompleteAsync();

    }
}
