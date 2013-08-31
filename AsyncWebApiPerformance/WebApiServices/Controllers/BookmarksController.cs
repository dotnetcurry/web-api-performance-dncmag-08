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
    public class BookmarksController : ApiController
    {
        private WebApiServicesContext db = new WebApiServicesContext();

        // GET api/Bookmarks
        //public IEnumerable<Bookmark> GetBookmark()
        //{
        //    Thread.Sleep(500);
        //    return db.Bookmarks.AsEnumerable();
        //}

        // GET api/Bookmarks
        public async Task<IEnumerable<Bookmark>> GetBookmark()
        {
            await Task.Delay(500);
            return await db.Bookmarks.ToListAsync<Bookmark>();
        }

        // GET api/Bookmarks/5
        public Bookmark GetBookmark(Int32 id)
        {
            Bookmark bookmark = db.Bookmarks.Find(id);
            if (bookmark == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return bookmark;
        }

        // PUT api/Bookmarks/5
        public HttpResponseMessage PutBookmark(Int32 id, Bookmark bookmark)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != bookmark.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(bookmark).State = EntityState.Modified;

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

        // POST api/Bookmarks
        public HttpResponseMessage PostBookmark(Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                db.Bookmarks.Add(bookmark);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, bookmark);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = bookmark.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Bookmarks/5
        public HttpResponseMessage DeleteBookmark(Int32 id)
        {
            Bookmark bookmark = db.Bookmarks.Find(id);
            if (bookmark == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Bookmarks.Remove(bookmark);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, bookmark);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}