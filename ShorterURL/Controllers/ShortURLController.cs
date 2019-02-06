using System.Web.Mvc;
using ShorterURL.Lib;

namespace ShorterURL.Controllers
{
    public class ShortURLController : Controller
    {
        public ActionResult Index(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        public ActionResult Shorten(string url)
        {
            ShortenURL shortenURL = new ShortenURL();
            string shortenedURL = shortenURL.Shorten(url);

            return RedirectToAction("Index", new { message = "Your shortened URL is: " + shortenedURL });
        }

        public ActionResult Expand(string shortURL)
        {
            ShortenURL shortenURL = new ShortenURL();
            string expandedURL = shortenURL.Expand(shortURL);

            return RedirectToAction("Index", new { message = "Your expanded URL is: " + expandedURL });
        }

        public ActionResult Clicks(string clickedURL)
        {
            ShortenURL shortenURL = new ShortenURL();
            ulong clicks = shortenURL.ClicksOnShortURL(clickedURL);

            return RedirectToAction("Index", new { message = "Your short URL has been clicked " + clicks + " times." });
        }
    }
}