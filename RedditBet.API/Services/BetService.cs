using System;
using System.Data.Entity;
using System.Collections.Generic;

namespace RedditBet.API.Services
{
    using RedditBet.API.Models;
    using RedditBet.API.Repositories;
    
    public class BetService
    {
        private readonly IRepository<Bet> _repo;

        public BetService()
        {
            _repo = new Repository<Bet>(DatabaseContext.Create());
        }

        public BetService(DbContext context)
        {
            _repo = new Repository<Bet>(context);
        }

        public Bet Get(int id)
        {
            var bet = _repo.Get(id);

            return bet;
        }

        public IEnumerable<Bet> GetAll()
        {
            return _repo.GetAll();
        }

        public void Create(Bet b)
        {
            var tp = GenerateTempPage(b.Solicitor);
            
            if (!tp.HasToken())
            {
                var tpService = new TempPageService();
                tp.Token = tpService.GenerateToken();
            }

            b.TempPages.Add(tp);

            _repo.Add(b);
        }

        public void Update(Bet b)
        {
            _repo.Update(b);
        }

        public void Remove(Bet b)
        {
            _repo.Remove(b);
        }

        private TempPageData GenerateTempPage(string userName)
        {
            var tempPage = new TempPageData();
                        
            tempPage.UserName = userName;

            return tempPage;
        }
    }
}