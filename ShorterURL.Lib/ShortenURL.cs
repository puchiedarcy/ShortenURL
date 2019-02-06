namespace ShorterURL.Lib
{
    /* Primary class responsible for Shortening and Expanding a URL.
     */
    public class ShortenURL
    {
        IHashMaker hasher;

        // These two are static simply to facilitate an in-memory data store.
        // In production, these would likely not be static as
        // the data stores themselves would actually be persistent.
        static IShortenedURLRepository shortenedURLRepo;
        static string lastHash = string.Empty;

        const string shortDomain = "short.io";

        public ShortenURL()
        {
            hasher = new ShortHashMaker();
            if (shortenedURLRepo == null)
            {
                shortenedURLRepo = new ShortenedURLRepository();
            }
        }

        public string Shorten(string url)
        {
            ShortenedURL shortenedURL = shortenedURLRepo.GetByURL(url);

            if (shortenedURL == null) // shorten this URL for the first time
            {
                shortenedURL = new ShortenedURL(url, hasher.NextHash(lastHash));
                shortenedURLRepo.Save(shortenedURL);
                lastHash = shortenedURL.Hash;
            }

            return FormatHashAsURL(shortenedURL.Hash);
        }

        public string Expand(string shortURL)
        {
            ShortenedURL shortenedURL = GetShortenedURL(shortURL);

            if (shortenedURL == null) // shortened URL does not exist.
            {
                return string.Empty;
            }

            shortenedURL.Expanded();

            return shortenedURL.URL;
        }

        public ulong ClicksOnShortURL(string shortURL)
        {
            ShortenedURL shortenedURL = GetShortenedURL(shortURL);

            if (shortenedURL == null) // shortened URL does not exist.
            {
                return 0;
            }

            return shortenedURL.Clicks;
        }

        private ShortenedURL GetShortenedURL(string shorterURL)
        {
            string hash = ParseHashFromURL(shorterURL);

            return shortenedURLRepo.GetByHash(hash);
        }

        public string FormatHashAsURL(string hash)
        {
            return string.Format("http://{0}/{1}", shortDomain, hash);
        }

        public string ParseHashFromURL(string url)
        {
            string expectedStart = FormatHashAsURL(string.Empty);
            if (url.IndexOf(expectedStart) == 0) {
                return url.Substring(expectedStart.Length);
            }

            return string.Empty;
        }
    }
}
