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
        public IHttpActionResult PutBet(Bet entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.Update(entry);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Bet
        [ResponseType(typeof(Bet))]
        public IHttpActionResult PostBet(Bet entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.Create(entry);

            return CreatedAtRoute("DefaultApi", new { id = entry.BetId }, entry);
        }
    }
}
