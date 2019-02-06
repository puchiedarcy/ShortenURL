using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShorterURL.Lib
{
    public interface IHashMaker
    {
        string NextHash(string lastHash);
    }
}
