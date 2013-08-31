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
    public class EmailsController : ApiController
    {
        private WebApiServicesContext db = new WebApiServicesContext();

        // GET api/Emails
        //public IEnumerable<Email> GetEmail()
        //{
        //    Thread.Sleep(500);
        //    return db.Emails.AsEnumerable();
        //}

        // GET api/Emails
        public async Task<IEnumerable<Email>> GetEmail()
        {
            await Task.Delay(500);
            return await db.Emails.ToListAsync<Email>();
        }

        // GET api/Emails/5
        public Email GetEmail(Int32 id)
        {
            Email email = db.Emails.Find(id);
            if (email == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return email;
        }

        // PUT api/Emails/5
        public HttpResponseMessage PutEmail(Int32 id, Email email)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != email.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(email).State = EntityState.Modified;

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

        // POST api/Emails
        public HttpResponseMessage PostEmail(Email email)
        {
            if (ModelState.IsValid)
            {
                db.Emails.Add(email);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, email);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = email.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Emails/5
        public HttpResponseMessage DeleteEmail(Int32 id)
        {
            Email email = db.Emails.Find(id);
            if (email == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Emails.Remove(email);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, email);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}