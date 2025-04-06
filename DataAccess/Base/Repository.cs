
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DataAccess.Context;


namespace DataAccess.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _db;
        private readonly DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public IQueryable<T> FindAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, string? includeProperties = null)
        {

            IQueryable<T> query = dbSet;
            query = query.Where(expression);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query;
        }

        public void Create(T entity)
        {
              dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DetachChildren(Expression<Func<T, bool>> expression, string parentPropertyName)
        {
            var children = dbSet.Where(expression).ToList();
            foreach (var child in children)
            {
                typeof(T).GetProperty(parentPropertyName)?.SetValue(child, null);
            }
            _db.SaveChanges();
        }



    }
}
