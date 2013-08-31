using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Web.Mvc;

namespace AsyncWebApiPerformance.Models
{
    public class DashboardController : AsyncController
    {
        //
        // GET: /Dashboard/
        public async Task<ActionResult> Index()
        {
            //Services.FeedService feedService = new Services.FeedService();
            Stopwatch timer = Stopwatch.StartNew();
            timer.Start();
            var feeds = await GetFeeds();
            timer.Stop();
            feeds.TimeTaken = timer.ElapsedMilliseconds;
            return View(feeds);
        }

        public async Task<Dashboard> GetFeeds()
        {
            var emails = await new HttpClient().GetStringAsync("http://localhost:18545/api/Emails");
            var myTasks = await new HttpClient().GetStringAsync("http://localhost:18545/api/Tasks");
            var notes = await new HttpClient().GetStringAsync("http://localhost:18545/api/Notes");
            var bookmarks = await new HttpClient().GetStringAsync("http://localhost:18545/api/Bookmarks");

            //await Task.WhenAll(emails, myTasks, notes, bookmarks);

            Dashboard dash = new Dashboard();
            dash.Emails = Deserialize<Email>(emails);
            dash.Bookmarks = Deserialize<Bookmark>(bookmarks);
            dash.Notes = Deserialize<Note>(notes);
            dash.Tasks = Deserialize<MyTask>(myTasks);
            return dash;
        }

        //public ActionResult Index()
        //{
        //    //Services.FeedService feedService = new Services.FeedService();
        //    Stopwatch timer = Stopwatch.StartNew();
        //    timer.Start();
        //    var feeds = GetFeeds();
        //    timer.Stop();
        //    feeds.TimeTaken = timer.ElapsedMilliseconds;
        //    return View(feeds);
        //}

        //public Dashboard GetFeeds()
        //{

        //    string emails = new WebClient().DownloadString("http://localhost:18545/api/Emails");
        //    string myTasks = new WebClient().DownloadString("http://localhost:18545/api/Tasks");
        //    string notes = new WebClient().DownloadString("http://localhost:18545/api/Notes");
        //    string bookmarks = new WebClient().DownloadString("http://localhost:18545/api/Bookmarks");

        //    Dashboard dash = new Dashboard();
        //    dash.Emails = Deserialize<Email>(emails);
        //    dash.Bookmarks = Deserialize<Bookmark>(bookmarks);
        //    dash.Notes = Deserialize<Note>(notes);
        //    dash.Tasks = Deserialize<MyTask>(myTasks);
        //    return dash;
        //}

        public List<T> Deserialize<T>(string data)
        {
            List<T> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(data);
            return list;
        }
        //
        // GET: /Dashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Dashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Dashboard/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Dashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Dashboard/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Dashboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Dashboard/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
