﻿using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Collections.Generic;

namespace RedditBet.API.Controllers
{
    using RedditBet.API.Models;
    using RedditBet.API.Services;
    using System.Web.Http.Cors;

    [RoutePrefix("api/bet"), EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BetController : ApiController
    {
        private BetService _service = new BetService();

        // GET: api/Bet
        public BetCollection GetBet()
        {
            return new BetCollection(_service.GetAll());
        }

        // GET: api/Bet/5
        [ResponseType(typeof(Bet))]
        public IHttpActionResult GetBet(int id)
        {
            var bet = _service.Get(id);

            if (bet == null)
            {
                return NotFound();
            }

            return Ok(bet);
        }
        
        // PUT: api/Bet/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBet(BetViewModel bet)
        {
            if (bet.BetId == 0) return BadRequest("A BetId cannot be 0 for PUT requests.");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.Update(bet.ToMappedType());

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Bet
        [ResponseType(typeof(BetCreationResponse))]
        public IHttpActionResult PostBet(BetViewModel bet)
        {
            if (bet.BetId != 0) return BadRequest("A BetId must be 0 for POST requests.");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = bet.ToMappedType();

            _service.Create(model);

            var response = new BetCreationResponse(model);

            return CreatedAtRoute("DefaultApi", new { id = response.BetId }, response);
        }
    }
}
