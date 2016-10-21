using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Xml.Linq;
using MyPortfolio.Models;


namespace MyPortfolio.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Marcell's Demo";
            return View();
        }
        public ActionResult ArtistSearch()
        {
            ViewBag.Title = "Simular Music Artist Search | Marcell's Demo";
            return View();
        }
        /// <summary>
        /// Gets the artist name from a form post
        /// </summary>
        /// <param name="artistname">The name of the Artist to search.</param>
        /// <returns>Returns XML from last.fm API web request.</returns>
        /// 
        [HttpPost]
        public string Search(string artistName)
        {
            Artist artist = new Artist();
            var retArtist = artist.ArtistSearch(artistName);
            return  retArtist; 
        }
    }
}
