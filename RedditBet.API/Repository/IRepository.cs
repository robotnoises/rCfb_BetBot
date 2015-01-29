using System;
using System.Collections.Generic;

namespace RedditBet.API.Repositories
{
    public interface IRepository<T> : IDisposable where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetWhere(Func<T, bool> predicate);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        new void Dispose();
    }
}