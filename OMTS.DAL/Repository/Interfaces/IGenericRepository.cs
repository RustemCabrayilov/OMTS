using OMTS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMTS.DAL.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public Task<T> Add(T entity);
        public T Update(T entity);
        public T Delete(T entity);
        public Task<T> Get(int? id);
        public Task<IEnumerable<T>> GetAll(params Func<IQueryable<T>, IQueryable<T>>[] includeProperties);
        public Task SaveAsync();
    }
}
