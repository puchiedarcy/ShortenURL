using System.Text;

namespace ShorterURL.Lib
{
    /* This class takes in a previous hash (starting with empty string)
     * and generates the next logical smallest hash.
     * The idea here is basically counting with a much higher base (the length of CharPool).
     *
     * For examply, the first hash is the first char of CharPool, "a".
     * Next is  "b", "c", etc until all single character hashes are taken.
     * After the last single char hash, the start of CharPool is appended and the cycle repeats,
     * e.g. "aa", "ba", "ca" ... "800", "900", "000", "aaaa", etc.
     */
    public class ShortHashMaker : IHashMaker
    {
        // More chars can be added to this string
        // to increase the number of available hashes.
        public const string CharPool = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        public string NextHash(string lastHash)
        {
            StringBuilder sb = new StringBuilder();
            BuildHash(lastHash, sb);
            return sb.ToString();
        }

        // Recursively build the hash, incase there is any "carrying" needed to be done.
        public static void BuildHash(string remainingHash, StringBuilder sb)
        {
            if (remainingHash.Length == 0)
            {
                sb.Append(ShortHashMaker.CharPool[0]);
                return;
            }

            char nextChar = GetNextChar(remainingHash[0]);
            sb.Append(nextChar);

            // The char pool has looped, thus we need to "carry" and increment the next char.
            if (nextChar == CharPool[0])
            {
                BuildHash(remainingHash.Substring(1), sb);
            }
            else
            {
                sb.Append(remainingHash.Substring(1));
                return;
            }
        }

        public static char GetNextChar(char current)
        {
            int currentCharIndex = CharPool.IndexOf(current);
            if (currentCharIndex + 1 < CharPool.Length)
            {
                return CharPool[currentCharIndex + 1];
            }
            else
            {
                return CharPool[0];
            }

        }
    }
}
