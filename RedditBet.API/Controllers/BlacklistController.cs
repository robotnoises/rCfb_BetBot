﻿using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Collections.Generic;

namespace RedditBet.API.Controllers
{
    using RedditBet.API.Models;
    using RedditBet.API.Services;

    [RoutePrefix("api/blacklist")]
    public class BlacklistController : ApiController
    {
        private BlacklistService _service = new BlacklistService();

        // GET: api/blacklist
        public IEnumerable<BlacklistEntry> GetBlacklist()
        {
            return _service.GetAll();
        }

        // GET: api/blacklist/5
        [ResponseType(typeof(BlacklistEntry))]
        public IHttpActionResult GetBlacklist(int id)
        {
            var task = _service.Get(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PUT: api/blacklist/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBlacklist(BlacklistEntry entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.Update(entry);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/blacklist
        [ResponseType(typeof(BlacklistEntry))]
        public IHttpActionResult PostBlacklist(BlacklistEntry entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.Create(entry);

            return CreatedAtRoute("DefaultApi", new { id = entry.BlacklistEntryId }, entry);
        }
    }
}
