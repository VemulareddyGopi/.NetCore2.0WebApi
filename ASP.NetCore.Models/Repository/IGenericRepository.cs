using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ASP.NetCore.Models.Repository
{
    public interface IGenericRepository<T> where T : class
    {

        IEnumerable<T> Get();

        T FindById(int id);

        void Insert(T entity);

        void Delete(object id);

        void Delete(T entityToDelete);

        void Update(T entityToUpdate);

        IEnumerable<object> GetMany(Func<T, bool> where, Func<T, object> selection = null);

        IQueryable<T> GetManyQueryable(Func<T, bool> where);

        T Get(Func<T, Boolean> where);

        void Delete(Func<T, Boolean> where);

        IQueryable<T> GetWithInclude(Expression<Func<T, bool>> predicate, params string[] include);

        bool Exists(object primaryKey);

        T GetSingle(Func<T, bool> predicate);

        T GetFirst(Func<T, bool> predicate);

        IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters);

        int ExecuteSqlCommand(string query, params object[] parameters);

    }
}
