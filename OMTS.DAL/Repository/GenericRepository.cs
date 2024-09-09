using Microsoft.EntityFrameworkCore;
using OMTS.DAL.Data;
using OMTS.DAL.Models;
using OMTS.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMTS.DAL.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly OMTSDbContext _dbContext;
		private readonly DbSet<T> _dbSet;
		public GenericRepository(OMTSDbContext dbContext)
		{
			_dbContext = dbContext;
			_dbSet = _dbContext.Set<T>();
		}
		public async Task<T> Add(T entity)
		{
			await _dbSet.AddAsync(entity);
			return entity;
		}

		public T Delete(int id)
		{
			var entity = _dbSet.Find(id);
			_dbSet.Remove(entity);
			return entity;
		}

		public async Task<T> Get(int? id)
		{
			return await _dbSet.FindAsync(id);
		}


		public async Task<IEnumerable<T>> GetAll(params Func<IQueryable<T>, IQueryable<T>>[] includeProperties)
		{
			IQueryable<T> query = _dbSet;

			foreach (var includeProperty in includeProperties)
			{
				query = includeProperty(query);
			}

			return  await query.ToListAsync();

		}

		public T Update(T entity)
		{
			_dbSet.Update(entity);
			return entity;
		}
		public async Task SaveAsync()
		{
			await _dbContext.SaveChangesAsync();
		}
	}
}
