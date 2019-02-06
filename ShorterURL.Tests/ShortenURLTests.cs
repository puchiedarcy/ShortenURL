using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShorterURL.Lib.Tests
{
    [TestClass]
    public class ShortenURLTests
    {
        [TestMethod]
        public void ParseHashFromURL_NonURLReturnsEmptyString()
        {
            ShortenURL shortenURL = new ShortenURL();
            string notURL = "this_is_not_a_url_from_FormatHashAsURL";

            string hash = shortenURL.ParseHashFromURL(notURL);

            Assert.AreEqual(string.Empty, hash);
        }

        [TestMethod]
        public void FormatHashAsURL_ParseHashFromURL_UndoEachOther()
        {
            ShortenURL shortenURL = new ShortenURL();
            string hash = "abceasyas123";

            string url = shortenURL.FormatHashAsURL(hash);

            Assert.AreEqual(hash, shortenURL.ParseHashFromURL(url));
        }

        [TestMethod]
        public void Shorten_FirstHashIsNotEmpty()
        {
            ShortenURL shortenURL = new ShortenURL();
            string url = "http://www.mlb.com/2020-world-series";

            string shortURL = shortenURL.Shorten(url);

            string shortURLHash = shortenURL.ParseHashFromURL(shortURL);
            Assert.AreNotEqual(string.Empty, shortURLHash);
        }

        [TestMethod]
        public void Shorten_FirstTwoHashesAreDifferent()
        {
            ShortenURL shortenURL = new ShortenURL();
            string url = "http://www.mlb.com/2020-world-series";

            string shortURL = shortenURL.Shorten(url);
            string shortURL2 = shortenURL.Shorten(url + url);

            string shortURLHash = shortenURL.ParseHashFromURL(shortURL);
            string shortURLHash2 = shortenURL.ParseHashFromURL(shortURL2);

            Assert.AreNotEqual(shortURLHash, shortURLHash2);
        }

        [TestMethod]
        public void Shorten_SameURLHasSameHash()
        {
            ShortenURL shortenURL = new ShortenURL();
            string url = "http://www.mlb.com/2020-world-series";

            string shortURL = shortenURL.Shorten(url);
            string shortURL2 = shortenURL.Shorten(url);

            string shortURLHash = shortenURL.ParseHashFromURL(shortURL);
            string shortURLHash2 = shortenURL.ParseHashFromURL(shortURL2);

            Assert.AreEqual(shortURLHash, shortURLHash2);
        }

        [TestMethod]
        public void Expand_ReturnsEmptyStringForNotShortenedURL()
        {
            ShortenURL shortenURL = new ShortenURL();
            string shortURL = shortenURL.FormatHashAsURL("somehash");

            string expandedURL = shortenURL.Expand(shortURL);

            Assert.AreEqual(string.Empty, expandedURL);
        }

        [TestMethod]
        public void Expand_ShortenedURLReturnsFullURL()
        {
            ShortenURL shortenURL = new ShortenURL();
            string url = "http://www.mlb.com/2020-world-series";

            string shortURL = shortenURL.Shorten(url);

            string expandedURL = shortenURL.Expand(shortURL);

            Assert.AreEqual(url, expandedURL);
        }

        [TestMethod]
        public void ClicksOnShortURL_ZeroForNotShortenedURL()
        {
            ShortenURL shortenURL = new ShortenURL();
            string shortURL = shortenURL.FormatHashAsURL("somehash");

            ulong clicks = shortenURL.ClicksOnShortURL(shortURL);

            Assert.AreEqual((ulong)0, clicks);
        }

        [TestMethod]
        public void ClicksOnShortURL_ZeroClicksOnNewShortURL()
        {
            ShortenURL shortenURL = new ShortenURL();
            string url = "http://www.mlb.com/2020-world-series";

            string shortURL = shortenURL.Shorten(url);
            ulong clicks = shortenURL.ClicksOnShortURL(shortURL);

            Assert.AreEqual((ulong)0, clicks);
        }

        [TestMethod]
        public void ClicksOnShortURL_ExpandIncrementsClicks()
        {
            ShortenURL shortenURL = new ShortenURL();
            string url = "http://www.mlb.com/2020-world-series";

            string shortURL = shortenURL.Shorten(url);

            shortenURL.Expand(shortURL);
            ulong clicks = shortenURL.ClicksOnShortURL(shortURL);
            Assert.AreEqual((ulong)1, clicks);
        }

        [TestMethod]
        public void ClicksOnShortURL_ExpandOnlyIncrementsOneURL()
        {
            ShortenURL shortenURL = new ShortenURL();
            string url = "http://www.mlb.com/2020-world-series";
            string url2 = "http://www.mlb.com/2020-world-series?team1=cubs&team2=rsox";


            string shortURL = shortenURL.Shorten(url);
            string shortURL2 = shortenURL.Shorten(url2);

            shortenURL.Expand(shortURL2);

            ulong clicks = shortenURL.ClicksOnShortURL(shortURL);
            ulong clicks2 = shortenURL.ClicksOnShortURL(shortURL2);
            Assert.AreEqual((ulong)0, clicks);
            Assert.AreEqual((ulong)1, clicks2);
        }
    }
}
