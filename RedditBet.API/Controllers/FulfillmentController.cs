using System;
using System.Web.Http;

namespace RedditBet.API.Controllers
{   
    using RedditBet.API.Models;
    using RedditBet.API.Services;

    public class FulfillmentController : ApiController
    {
        FulfillmentService _service = new FulfillmentService();

        // Todo

        private const string _betNotFoundMsg = "";
        private const string _betAlreadyFulfilledMsg = "";
        private const string _betHasExistingFulfillmentMsg = "";

        // This is for adding a brand new fulfillment record to a bet
        public IHttpActionResult PostFulfillment(FulfillmentViewModel data)
        {
            var bet = GetBet(data);
            
            if (bet == null) return BadRequest(string.Format("Bet (betId: {0}) was not found.", data.BetId));
            if (bet.IsFullfilled()) return BadRequest("This Bet already been fulfilled");
            if (bet.Fulfillment != null) return BadRequest("This Bet already has a fulfillment record. Use PUT if you wish to update.");
            
            _service.Add(data.ToMappedType());
                        
            return Ok(data);
        }

        public IHttpActionResult PutFulfillment(FulfillmentViewModel data)
        {
            var bet = GetBet(data);

            if (bet == null) return BadRequest(string.Format("Bet (betId: {0}) was not found.", data.BetId));
            if (bet.IsFullfilled()) return BadRequest("This Bet already been fulfilled");
            
            _service.Update(data.ToMappedType());

            return Ok(data);
        }

        private Bet GetBet(FulfillmentViewModel data)
        {
            var betService = new BetService();
            var bet = betService.Get(data.BetId);

            return bet;
        }
    }
}
