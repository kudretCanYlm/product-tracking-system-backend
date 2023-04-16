using DAS.Core.Specifications.Base;
using DAS.Data.Context;
using DAS.Model.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DAS.Core.Infrastructure
{
    public class RepositoryBase<T>:IRepository<T> where T:BaseEntity
    {
        private DASContext dasContext;
        private readonly IDbSet<T> dbset;
		public IQueryable<T> Table => dbset;

		public IQueryable<T> TableNoTracking => dbset.AsNoTracking();

		public  IEnumerable<T> GetSpec(ISpecification<T> spec)
		{
			return  ApplySpecification(spec).ToList();
		}

		private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
			return SpecificationEvaluator<T>.GetQuery(Table, spec);
		}

		protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DasContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected DASContext DasContext
        {
            get { return dasContext ?? (dasContext = DatabaseFactory.Get()); }
        }

        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }
        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            DasContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void UpdateMany(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                dbset.Attach(entity);
                DasContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }
        public virtual T GetById(Guid id)
        {
            return dbset.Find(id);
        }
        public virtual T GetById(string id)
        {
            return dbset.Find(id);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }

       public virtual IQueryable<T> GetManyQuery(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where);
        }



        /// <summary>
        /// Return a paged list of entities
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="page">Which page to retrieve</param>
        /// <param name="where">Where clause to apply</param>
        /// <param name="order">Order by to apply</param>
        /// <returns></returns>
        public virtual IQueryable<T> GetPage<TOrder>(Page page, Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order)
        {
            var result = dbset.OrderBy(order).Where(where).GetPage(page);

            return result;
        }

		public IQueryable<T> GetPage<TOrder>(IQueryable<T> include, Page page, Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order)
		{
            var result = include.OrderBy(order).Where(where).GetPage(page);
            return result;
		}

		public T Get(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault<T>();
        }

		public IQueryable<T> GetQuery()
		{
            return dbset;
		}

	}
}
