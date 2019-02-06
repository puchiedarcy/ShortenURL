using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace ShorterURL.Lib.Tests
{
    [TestClass]
    public class ShortHashMakerTests
    {
        [TestMethod]
        public void GetNextChar_FirstCharGetsSecondChar()
        {
            char firstChar = ShortHashMaker.CharPool[0];
            char secondChar = ShortHashMaker.CharPool[1];

            char nextChar = ShortHashMaker.GetNextChar(firstChar);

            Assert.AreEqual(secondChar, secondChar);
        }

        [TestMethod]
        public void GetNextChar_LastCharGetsFirstChar()
        {
            char lastChar = ShortHashMaker.CharPool[ShortHashMaker.CharPool.Length - 1];
            char firstChar = ShortHashMaker.CharPool[0];

            char nextChar = ShortHashMaker.GetNextChar(lastChar);

            Assert.AreEqual(firstChar, nextChar);
        }

        [TestMethod]
        public void BuildHash_EmptyStringGivesFirstChar()
        {
            StringBuilder sb = new StringBuilder();

            ShortHashMaker.BuildHash(string.Empty, sb);

            Assert.AreEqual(ShortHashMaker.CharPool.Substring(0, 1), sb.ToString());
        }

        [TestMethod]
        public void BuildHash_SingleCharGivesNextChar()
        {
            StringBuilder sb = new StringBuilder();

            string remaningHash = ShortHashMaker.CharPool.Substring(1, 1);

            ShortHashMaker.BuildHash(remaningHash, sb);

            Assert.AreEqual(ShortHashMaker.CharPool.Substring(2, 1), sb.ToString());
        }
        
        [TestMethod]
        public void BuildNext_LastCharGivesFirstCharTwice()
        {
            StringBuilder sb = new StringBuilder();
            string expected = ShortHashMaker.CharPool.Substring(0, 1);
            expected += expected;

            string remainingHash = ShortHashMaker.CharPool.Substring(ShortHashMaker.CharPool.Length - 1, 1);

            ShortHashMaker.BuildHash(remainingHash, sb);


            Assert.AreEqual(expected, sb.ToString());
        }

        [TestMethod]
        public void BuildNext_TwoCharHashChangesOnlyFirstChar()
        {
            StringBuilder sb = new StringBuilder();
            string remainingHash = ShortHashMaker.CharPool.Substring(9, 1) + ShortHashMaker.CharPool.Substring(24, 1);
            string expected = ShortHashMaker.CharPool.Substring(10, 1) + ShortHashMaker.CharPool.Substring(24, 1);

            ShortHashMaker.BuildHash(remainingHash, sb);

            Assert.AreEqual(expected, sb.ToString());
        }

        [TestMethod]
        public void BuildNext_LastThreeCharHashChangesToFirstFourCharHash()
        {
            StringBuilder sb = new StringBuilder();
            string remaningHash = ShortHashMaker.CharPool.Substring(ShortHashMaker.CharPool.Length - 1, 1);
            remaningHash += (remaningHash + remaningHash);

            string expected = ShortHashMaker.CharPool.Substring(0, 1);
            expected += expected;
            expected += expected;

            ShortHashMaker.BuildHash(remaningHash, sb);

            Assert.AreEqual(expected, sb.ToString());
        }

        [TestMethod]
        public void NextHash_LastThreeCharHashChangesToFirstFourCharHash()
        {
            string lastHash = ShortHashMaker.CharPool.Substring(ShortHashMaker.CharPool.Length - 1, 1);
            lastHash += (lastHash + lastHash);

            string expected = ShortHashMaker.CharPool.Substring(0, 1);
            expected += expected;
            expected += expected;

            ShortHashMaker hasher = new ShortHashMaker();
            var nextHash = hasher.NextHash(lastHash);

            Assert.AreEqual(expected, nextHash);
        }
    }
}
