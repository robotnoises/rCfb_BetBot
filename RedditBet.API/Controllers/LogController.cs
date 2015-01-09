using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RedditBet.API.Models;
using RedditBet.API.Services;

namespace RedditBet.API.Controllers
{
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

        //// PUT: api/Log/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutLog(int id, Log log)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != log.LogId)
        //    {
        //        return BadRequest();
        //    }

        //    _service.Entry(log).State = EntityState.Modified;

        //    try
        //    {
        //        await _service.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LogExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

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

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _service.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool LogExists(int id)
        //{
        //    return _service.Logs.Count(e => e.LogId == id) > 0;
        //}
    }
}