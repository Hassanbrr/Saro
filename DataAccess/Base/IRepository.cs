
using System.Linq.Expressions;


namespace DataAccess.Base
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> FindAll(string? includeProperties = null);

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, string? includeProperties = null);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        public void DetachChildren(Expression<Func<T, bool>> expression, string parentPropertyName);

    }
}
