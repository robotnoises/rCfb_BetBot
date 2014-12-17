using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedditBet.API.Repositories
{
    public interface IRepository<T> : IDisposable
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}