using System.Collections.Generic;

namespace ShorterURL.Lib
{
    /* Simply, in-memory repository of two dictionaries
     * for the sotrage and retrieval of ShortenedURL objects.
     */
    public class ShortenedURLRepository : IShortenedURLRepository
    {
        // shortenedURLs keys are the URLs unique hash code
        // and the values are ShortenedURL objects.
        Dictionary<int, ShortenedURL> shortenedURLs;

        // hashToID is a lookup table to find unique URL IDs
        // by the hash that was earlier generated for it.
        // This is to be used in conjuction with shortenedURLs
        // for finding ShortenedURL objects by hash.
        Dictionary<string, int> hashToID;

        public ShortenedURLRepository()
        {
            shortenedURLs = new Dictionary<int, ShortenedURL>();
            hashToID = new Dictionary<string, int>();
        }

        public ShortenedURL GetByURL(string url)
        {
            int id = GetUniqueIDForURL(url);
            if(!shortenedURLs.ContainsKey(id))
            {
                return null;
            }

            return shortenedURLs[id];
        }

        public ShortenedURL GetByHash(string hash)
        {
            if (!hashToID.ContainsKey(hash))
            {
                return null;
            }

            return shortenedURLs[hashToID[hash]];
        }

        public void Save(ShortenedURL shortenedURL)
        {
            int id = GetUniqueIDForURL(shortenedURL.URL);
            shortenedURLs.Add(id, shortenedURL);
            hashToID.Add(shortenedURL.Hash, id);
        }

        public int GetUniqueIDForURL(string url)
        {
            return url.GetHashCode();
        }
    }
}
