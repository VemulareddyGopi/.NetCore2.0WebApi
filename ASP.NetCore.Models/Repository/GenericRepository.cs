using ASP.NetCore.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASP.NetCore.Models.Repository
{
    public class GenericRepository<T> :
      IGenericRepository<T>
           where T : class
    {

        public GenericRepository(ASPNetCoreContext context)
        {
            _entities = context;
        }
        private ASPNetCoreContext _entities;
        public ASPNetCoreContext db
        {

            get { return _entities; }
            set { _entities = value; }
        }

        /// <summary>  
        /// generic Get method for Entities  
        /// </summary>  
        /// <returns></returns>  
        public virtual IEnumerable<T> Get()
        {
            IQueryable<T> query = _entities.Set<T>();

            return query.ToList();
        }

        /// <summary>  
        /// Generic get method on the basis of id for Entities.  
        /// </summary>  
        /// <param name="id"></param>  
        /// <returns></returns> 
        public virtual T FindById(int id)
        {
            return _entities.Set<T>().Find(id);
        }

        /// <summary>  
        /// generic Insert method for the entities  
        /// </summary>  
        /// <param name="entity"></param>  
        public virtual void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            this._entities.Set<T>().Add(entity);
            _entities.SaveChanges();
        }

        /// <summary>  
        /// Generic Delete method for the entities  
        /// </summary>  
        /// <param name="id"></param>
        public virtual void Delete(object id)
        {
            T entityToDelete = _entities.Set<T>().Find(id);
            Delete(entityToDelete);
        }

        /// <summary>  
        /// Generic Delete method for the entities  
        /// </summary>  
        /// <param name="entityToDelete"></param>  
        public virtual void Delete(T entityToDelete)
        {
            if (_entities.Entry(entityToDelete).State == EntityState.Detached)
            {
                _entities.Set<T>().Attach(entityToDelete);
            }
            _entities.Set<T>().Remove(entityToDelete);
            _entities.SaveChanges();
        }

        /// <summary>  
        /// Generic update method for the entities  
        /// </summary>  
        /// <param name="entityToUpdate"></param>  
        public virtual void Update(T entityToUpdate)
        {
            _entities.Set<T>().Attach(entityToUpdate);
            _entities.Entry(entityToUpdate).State = EntityState.Modified;
            _entities.SaveChanges();
        }

        /// <summary>  
        /// generic method to get many record on the basis of a condition.  
        /// </summary>  
        /// <param name="where"></param>  
        /// <returns></returns>  
        public virtual IEnumerable<object> GetMany(Func<T, bool> where, Func<T, object> selection = null)
        {

            if (selection != null)
                return _entities.Set<T>().Where(where).Select(selection).AsEnumerable();

            else
                return _entities.Set<T>().Where(where).AsEnumerable();
        }


        /// <summary>  
        /// generic method to get many record on the basis of a condition but query able.  
        /// </summary>  
        /// <param name="where"></param>  
        /// <returns></returns>  
        public virtual IQueryable<T> GetManyQueryable(Func<T, bool> where)
        {
            return _entities.Set<T>().Where(where).AsQueryable();
        }


        /// <summary>  
        /// generic get method , fetches data for the entities on the basis of condition.  
        /// </summary>  
        /// <param name="where"></param>  
        /// <returns></returns>  
        public T Get(Func<T, Boolean> where)
        {
            return _entities.Set<T>().Where(where).FirstOrDefault<T>();
        }



        /// <summary>  
        /// generic delete method , deletes data for the entities on the basis of condition.  
        /// </summary>  
        /// <param name="where"></param>  
        /// <returns></returns>  
        public void Delete(Func<T, Boolean> where)
        {
            IQueryable<T> objects = _entities.Set<T>().Where<T>(where).AsQueryable();
            foreach (T obj in objects)
                _entities.Set<T>().Remove(obj);
        }


        /// <summary>  
        /// Inclue multiple  
        /// </summary>  
        /// <param name="predicate"></param>  
        /// <param name="include"></param>  
        /// <returns></returns>  
        public IQueryable<T> GetWithInclude(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params string[] include)
        {
            IQueryable<T> query = this._entities.Set<T>();
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }

        /// <summary>  
        /// Generic method to check if entity exists  
        /// </summary>  
        /// <param name="primaryKey"></param>  
        /// <returns></returns>  
        public bool Exists(object primaryKey)
        {
            return _entities.Set<T>().Find(primaryKey) != null;
        }

        /// <summary>  
        /// Gets a single record by the specified criteria (usually the unique identifier)  
        /// </summary>  
        /// <param name="predicate">Criteria to match on</param>  
        /// <returns>A single record that matches the specified criteria</returns>  
        public T GetSingle(Func<T, bool> predicate)
        {
            return _entities.Set<T>().Single<T>(predicate);
        }

        /// <summary>  
        /// The first record matching the specified criteria  
        /// </summary>  
        /// <param name="predicate">Criteria to match on</param>  
        /// <returns>A single record containing the first record matching the specified criteria</returns>  
        public T GetFirst(Func<T, bool> predicate)
        {
            return _entities.Set<T>().First<T>(predicate);
        }

        /// <summary>  
        /// Get all records basing on stored procedure  
        /// </summary>  
        /// <param name="predicate">Criteria to match on</param>  
        /// <returns>A single record containing the first record matching the specified criteria</returns>  
        public IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return _entities.Set<T>().FromSql<T>(query, parameters);
        }

        /// <summary>  
        /// Get all records basing on stored procedure  
        /// </summary>  
        /// <param name="predicate">Criteria to match on</param>  
        /// <returns>A single record containing the first record matching the specified criteria</returns>  
        public int ExecuteSqlCommand(string query, params object[] parameters)
        {
            return _entities.Database.ExecuteSqlCommand(query, parameters);
        }


    }
}
