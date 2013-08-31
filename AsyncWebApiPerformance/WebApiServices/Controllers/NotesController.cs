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
    public class NotesController : ApiController
    {
        private WebApiServicesContext db = new WebApiServicesContext();

         //GET api/Notes
        public async Task<IEnumerable<Note>> GetNotes()
        {
            await Task.Delay(500);
            return await db.Notes.ToListAsync<Note>();
        }

        //public IEnumerable<Note> GetNotes()
        //{
        //    Thread.Sleep(500);
        //    return db.Notes.AsEnumerable<Note>();
        //}

        // GET api/Notes/5
        public Note GetNotes(Int32 id)
        {
            Note notes = db.Notes.Find(id);
            if (notes == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return notes;
        }

        // PUT api/Notes/5
        public HttpResponseMessage PutNotes(Int32 id, Note notes)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != notes.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(notes).State = EntityState.Modified;

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

        // POST api/Notes
        public HttpResponseMessage PostNotes(Note notes)
        {
            if (ModelState.IsValid)
            {
                db.Notes.Add(notes);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, notes);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = notes.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Notes/5
        public HttpResponseMessage DeleteNotes(Int32 id)
        {
            Note notes = db.Notes.Find(id);
            if (notes == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Notes.Remove(notes);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, notes);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}