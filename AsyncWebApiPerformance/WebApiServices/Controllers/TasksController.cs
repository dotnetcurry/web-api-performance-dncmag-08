using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApiServices.Models;

namespace WebApiServices.Controllers
{
    public class TasksController : ApiController
    {
        private WebApiServicesContext db = new WebApiServicesContext();

        //// GET api/Tasks
        //public IEnumerable<MyTask> GetTask()
        //{
        //    Thread.Sleep(500);
        //    return db.Tasks.AsEnumerable();
        //}

        // GET api/Tasks
        public async Task<IEnumerable<MyTask>> GetTask()
        {
            await Task.Delay(500);
            return await db.Tasks.ToListAsync<MyTask>();
        }


        // GET api/Tasks/5
        public MyTask GetTask(Int32 id)
        {
            MyTask task = db.Tasks.Find(id);
            if (task == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return task;
        }

        // PUT api/Tasks/5
        public HttpResponseMessage PutTask(Int32 id, MyTask task)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != task.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(task).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Tasks
        public HttpResponseMessage PostTask(MyTask task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, task);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = task.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Tasks/5
        public HttpResponseMessage DeleteTask(Int32 id)
        {
            MyTask task = db.Tasks.Find(id);
            if (task == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Tasks.Remove(task);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, task);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}