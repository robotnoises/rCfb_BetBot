using System;

namespace RedditBet.API.Repositories
{
    using System.Data.Entity;
    using RedditBet.API.Data;

    public interface IUnitOfWork : IDisposable
    {
        RedditBetDataContext GetContext();
        void Save();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly RedditBetDataContext _context;

        public UnitOfWork(RedditBetDataContext context)
        {
            _context = context;
        }

        public RedditBetDataContext GetContext()
        {
            return _context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
