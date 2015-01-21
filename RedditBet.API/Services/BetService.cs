using System;
using System.Web;
using System.Collections.Generic;

namespace RedditBet.API.Services
{
    using RedditBet.API.Data;
    using RedditBet.API.Models;
    using RedditBet.API.Repositories;
    
    public class BetService
    {
        private readonly IUnitOfWork<Bet> _uow;

        public BetService()
        {
            _uow = new UnitOfWork<Bet>(DatabaseContext.Create());
        }

        public Bet Get(int id)
        {
            return _uow.Get(id);
        }

        public IEnumerable<Bet> GetAll()
        {
            return _uow.GetAll();
        }

        public void Create(Bet b)
        {
            b.TempPages.Add(GenerateTempPage(b.Solicitor));

            // if (!string.IsNullOrEmpty(b.Challenger))

            _uow.Add(b);
        }

        public void Update(Bet b)
        {
            _uow.Update(b);
        }

        public void Remove(Bet b)
        {
            _uow.Remove(b);
        }

        private TempPage GenerateTempPage(string userName)
        {
            var tempPage = new TempPage();

            tempPage.CreateToken();
            tempPage.UserName = userName;

            return tempPage;
        }
    }
}