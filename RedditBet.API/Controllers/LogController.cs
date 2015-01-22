using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Web.Http.Description;

namespace RedditBet.API.Controllers
{
    using RedditBet.API.Models;
    using RedditBet.API.Services;

    public class LogController : ApiController
    {
        private readonly LogService _service = new LogService();

        // GET: api/Log
        public IEnumerable<Log> GetLogs()
        {
            return _service.GetAll();
        }

        // GET: api/Log/5
        [ResponseType(typeof(Log))]
        public IHttpActionResult GetLog(int id)
        {
            Log log =  _service.Get(id);
            if (log == null)
            {
                return NotFound();
            }

            return Ok(log);
        }

        // POST: api/Log
        [ResponseType(typeof(Log))]
        public IHttpActionResult PostLog(Log log)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.Create(log);

            return CreatedAtRoute("DefaultApi", new { id = log.LogId }, log);
        }

        // DELETE: api/Log/5
        [ResponseType(typeof(Log))]
        public IHttpActionResult DeleteLog(int id)
        {
            _service.Remove(id);

            return Ok();
        }
    }
}