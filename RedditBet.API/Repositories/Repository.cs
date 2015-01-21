using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;

namespace RedditBet.API.Repositories
{
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Core.Objects.DataClasses;
    using System.Data.Entity.Core.Metadata.Edm;
        
    /// <summary>
    /// Generic repository
    /// </summary>
    /// <typeparam name="T">An EntityObject Type</typeparam>
    public class Repository<T> : IRepository2<T> where T : class
    {
        protected IUnitOfWork _instance;
        
        public virtual IUnitOfWork Instance
        {
            get
            {
                if (_instance == null)
                    throw new InvalidOperationException("A new IUnitOfWork instance must be created.");
                return _instance as IUnitOfWork;
            }
        }

        public virtual DbContext Context
        {
            get 
            {
                return _instance.GetContext();
            }
        }

        public Repository(IUnitOfWork instance)
        {
            _instance = instance;
        }

        public IEnumerable<T> GetAll()
        {
            return _instance.GetContext().Set<T>().ToList();
        }

        public T Get(int id)
        {
            return _instance.GetContext().Set<T>().Find(id);
        }

        public void Add(T entity)
        {
            _instance.GetContext().Set<T>().Add(entity);
            _instance.Save();
        }

        public void Update(T entity)
        {
            _instance.GetContext().Set<T>().Attach(entity);
            _instance.GetContext().Entry(entity).State = EntityState.Modified;
            _instance.Save();
        }

        public void Remove(T entity)
        {
            _instance.GetContext().Set<T>().Remove(entity);
            _instance.Save();
        }
    }
}