﻿using System;
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
    }
}