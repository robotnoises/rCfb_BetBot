using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Collections.Generic;

namespace RedditBet.API.Controllers
{
    using RedditBet.API.Models;
    using RedditBet.API.Services;

    [RoutePrefix("api/tasks")]
    public class TasksController : ApiController
    {
        private TaskService _service = new TaskService();

        // GET: api/Tasks
        public IEnumerable<BotTask> GetTasks()
        {
            return _service.GetAll();
        }

        [Route("incomplete")]
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

        // POST: api/tasks/markcomplete
        [Route("markcomplete/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult MarkTaskComplete(int id)
        {
            _service.MarkComplete(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}