using System;
using System.Web.Http;

namespace RedditBet.API.Controllers
{   
    using RedditBet.API.Models;
    using RedditBet.API.Services;

    public class FulfillmentController : ApiController
    {
        FulfillmentService _service = new FulfillmentService();

        // This is for adding a brand new fulfillment record to a bet
        public IHttpActionResult PostFulfillment(FulfillmentViewModel data)
        {   
            var betService = new BetService();
            var bet = betService.Get(data.BetId);
            
            if (bet == null) return BadRequest(string.Format("Bet (betId: {0}) was not found.", data.BetId));
            if (bet.IsFullfilled()) return BadRequest("This Bet already been fulfilled");

            var oldF = bet.FulfillmentData;
            var newF = data.ToDomainModel();

            bet.FulfillmentData = newF;

            betService.Update(bet);

            _service.RemoveOldFulfillmentRecord(oldF);
                
            return Ok(newF);
        }
    }
}
