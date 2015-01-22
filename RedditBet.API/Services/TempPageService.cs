using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;

namespace RedditBet.API.Services
{
    using RedditBet.API.Data;
    using RedditBet.API.Models;
    using RedditBet.API.Repositories;
    
    internal class TempPageService
    {
        private IUnitOfWork<TempPage> _uow;

        public TempPageService()
        {
            _uow = new UnitOfWork<TempPage>(DatabaseContext.Create());
        }

        public TempPageTokenStatus ValidateToken(string token)
        {
            // Todo: need to log these statuses 

            if (string.IsNullOrEmpty(token)) return TempPageTokenStatus.INVALID;

            var result = _uow.GetWhere(x => x.Token == token);
            
            if (result.Count() > 1) return TempPageTokenStatus.INVALID;
            
            var tempPage = result.FirstOrDefault();
                       
            if (tempPage == null)                           return TempPageTokenStatus.INVALID;
            if (!tempPage.Visited && tempPage.IsFresh())    return TempPageTokenStatus.OK;
            if (tempPage.Visited)                           return TempPageTokenStatus.USED;
            if (!tempPage.IsFresh())                        return TempPageTokenStatus.STALE;
            else                                            return TempPageTokenStatus.INVALID;
        }
    }
}