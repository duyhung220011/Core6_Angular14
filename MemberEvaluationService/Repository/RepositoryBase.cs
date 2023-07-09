using MemberEvaluationService.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MemberEvaluationService.Repository
{
	public interface IRepositoryBase<T>
	{
		IQueryable<T> FindAll();
		IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
		void Create(T entity);
		void Update(T entity);
		void Delete(T entity);
	}

	public abstract class RepositoryBase<T> where T : class
    {
        protected DataContext dataContext { get; set; }

        public RepositoryBase(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable<T> FindAll()
        {
            return dataContext.Set<T>()
                .AsNoTracking();
        }

		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
		{
			return dataContext.Set<T>()
				.Where(expression)
				.AsNoTracking();
		}

		public void Create(T entity)
		{
			dataContext.Set<T>().Add(entity);
		}

		public void Update(T entity)
		{
			dataContext.Set<T>().Update(entity);
		}

		public void Delete(T entity)
		{
			dataContext.Set<T>().Remove(entity);
		}

	}
}
