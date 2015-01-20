using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RedditBet.API.Repositories
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }

    public interface IRepository2<T> where T : class
    {
        IUnitOfWork Instance { get; }
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}