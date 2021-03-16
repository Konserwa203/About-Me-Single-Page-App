using AboutMePage.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AboutMePage.Controllers
{
    public class AboutUsController : Controller
    {
        public async Task GetPhotosFromInstagramAsync()
        {
            var client = new HttpClient();
            var path = "https://graph.instagram.com/me/media?fields=konserwaa,media_url&access_token=IGQVJYTlNvdWdsaTZAOVl9CRjVmZA2JrVmxNbldxWGJqd0NFN1lUVV8wNHFiSXNXclNEREp3U1YxOFFudFJxUEZACZAThidVZApbHRWbXRDNUFiVUNFQUc0cGJiWnVHWE9jdDJhSkEwYm9VSm1IUWUzYkp3WQZDZD";
            var response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                var photosData = JsonConvert.DeserializeObject<InstagramItem>(responseStr);
                ViewBag.InstaPhotos = photosData;
            }
        }
        public IEnumerable<RssItem> GetNewsFromRss()
        {
            var xml = XElement.Load("https://biznes.interia.pl/waluty/aktualnosci/feed");
            //var xml = XElement.Load("https://news.google.com/rss/topics/CAAqJggKIiBDQkFTRWdvSUwyMHZNRGRqTVhZU0FtVnVHZ0pWVXlnQVAB?hl=pl&gl=PL&ceid=PL:pl");
            
            var items =  xml.Descendants("item").Select(n => new RssItem
            {
                Title = n.Element("title").Value,
                Description = n.Element("description").Value,
                Link = n.Element("link").Value,
                PubDate = n.Element("pubDate").Value,
            }).Take(2);

            xml = XElement.Load("https://biznes.interia.pl/gieldy/aktualnosci/feed");
            // xml = XElement.Load("https://news.google.com/rss/topics/CAAqJggKIiBDQkFTRWdvSUwyMHZNRGx6TVdZU0FtVnVHZ0pWVXlnQVAB?hl=pl&gl=PL&ceid=PL:pl");
            var items2 = xml.Descendants("item").Select(n => new RssItem
            {
                Title = n.Element("title").Value,
                Description = n.Element("description").Value,
                Link = n.Element("link").Value,
                PubDate = n.Element("pubDate").Value,
            }).Take(2);

            var itemsConcat = items.Concat(items2);
            return itemsConcat;

        }

        public async Task<IActionResult> IndexAsync()
        {
            await GetPhotosFromInstagramAsync();
            var rssData = GetNewsFromRss();
            return View(rssData);
        }
    }
}
