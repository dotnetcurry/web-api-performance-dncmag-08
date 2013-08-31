using AsyncWebApiPerformance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AsyncWebApiPerformance.Services
{
    public class FeedService
    {
        public async Task<Dashboard> GetFeeds()
        {
            //WebClient client = new WebClient();
            //string emails = client.DownloadString("http://localhost:18545/api/Emails");
            //string myTasks = client.DownloadString("http://localhost:18545/api/Tasks");
            //string notes = client.DownloadString("http://localhost:18545/api/Notes");
            //string bookmarks = client.DownloadString("http://localhost:18545/api/Bookmarks");

            string emails = await new HttpClient().GetStringAsync("http://localhost:18545/api/Emails");
            string myTasks = await new HttpClient().GetStringAsync("http://localhost:18545/api/Tasks");
            string notes = await new HttpClient().GetStringAsync("http://localhost:18545/api/Notes");
            string bookmarks = await new HttpClient().GetStringAsync("http://localhost:18545/api/Bookmarks");

            Dashboard dash = new Dashboard();
            dash.Emails = Deserialize<Email>(emails);
            dash.Bookmarks = Deserialize<Bookmark>(bookmarks);
            dash.Notes = Deserialize<Note>(notes);
            dash.Tasks = Deserialize<MyTask>(myTasks);
            return dash;
        }

        public List<T> Deserialize<T>(string data)
        {
            List<T> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(data);
            return list;
        }
    }
}