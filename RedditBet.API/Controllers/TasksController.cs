using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RedditBet.API.Data;
using RedditBet.API.Models;
using RedditBet.API.Services;

namespace RedditBet.API.Controllers
{
    [RoutePrefix("api/Tasks")]
    public class TasksController : ApiController
    {
        private TaskService _service = new TaskService();

        // GET: api/Tasks
        public IEnumerable<BotTask> GetTasks()
        {
            return _service.GetAll();
        }

        [Route("Incomplete")]
        public IEnumerable<BotTask> GetTasksIncomplete()
        {
            return _service.GetIncomplete();
        }

        // GET: api/Tasks/5
        [ResponseType(typeof(BotTask))]
        public IHttpActionResult GetTask(int id)
        {
            var task = _service.Get(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PUT: api/Tasks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTask(BotTask task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // if (id != task.TaskId)
            // {
            //     return BadRequest();
            // }

            _service.Update(task);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tasks
        [ResponseType(typeof(BotTask))]
        public IHttpActionResult PostTask(BotTask task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.Create(task);

            return CreatedAtRoute("DefaultApi", new { id = task.TaskId }, task);
        }

        // DELETE: api/Tasks/5
        [ResponseType(typeof(BotTask))]
        public IHttpActionResult DeleteTask(int id)
        {
            _service.Remove(id);

            return Ok();
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}