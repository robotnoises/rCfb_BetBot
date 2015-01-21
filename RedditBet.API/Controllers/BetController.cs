using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Collections.Generic;

namespace RedditBet.API.Controllers
{
    using RedditBet.API.Models;
    using RedditBet.API.Services;

    [RoutePrefix("api/Bet")]
    public class BetController : ApiController
    {
        private BetService _service = new BetService();

        // GET: api/Bet
        public IEnumerable<Bet> GetBet()
        {
            return _service.GetAll();
        }

        // GET: api/Bet/5
        [ResponseType(typeof(Bet))]
        public IHttpActionResult GetBet(int id)
        {
            var task = _service.Get(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PUT: api/Bet/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBet(Bet bet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.Update(bet);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Bet
        [ResponseType(typeof(Bet))]
        public IHttpActionResult PostBet(Bet bet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.Create(bet);

            return CreatedAtRoute("DefaultApi", new { id = bet.BetId }, bet);
        }
    }
}
