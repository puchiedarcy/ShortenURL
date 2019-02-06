using System;

namespace ShorterURL.Lib
{
    /* POCO for holding shortened URL data together,
     * such as the full URL, its Hash,
     * and number of Clicks.
     */
    public class ShortenedURL
    {
        public string URL { get; protected set; }
        public string Hash { get; protected set; }
        public ulong Clicks { get; protected set; }

        public ShortenedURL(string url, string hash)
        {
            URL = url;
            Hash = hash;
            Clicks = 0;
        }

        public void Expanded()
        {
            Clicks++;
        }
    }
}
