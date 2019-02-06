using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShorterURL.Lib
{
    public interface IShortenedURLRepository
    {
        ShortenedURL GetByURL(string url);
        ShortenedURL GetByHash(string hash);
        void Save(ShortenedURL shortenedURL);
    }
}
