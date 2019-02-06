using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShorterURL.Lib.Tests
{
    [TestClass]
    public class ShortenedURLRepositoryTests
    {
        [TestMethod]
        public void GetUniqueHashForURL_SameStringSameID()
        {
            ShortenedURLRepository shortenRepo = new ShortenedURLRepository();
            string testString = "1-800-ilovebrandnewcarpet";
            string testString2 = "1-800-ilovebrandnewcarpet";

            int actual = shortenRepo.GetUniqueIDForURL(testString);
            int expected = shortenRepo.GetUniqueIDForURL(testString2);

            Assert.AreEqual(testString, testString2);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetUniqueHashForURL_DiffStringDiffID()
        {
            ShortenedURLRepository shortenRepo = new ShortenedURLRepository();
            string testString = "1-800-ilovebrandnewcarpet";
            string testString2 = testString + testString;

            int actual = shortenRepo.GetUniqueIDForURL(testString);
            int expected = shortenRepo.GetUniqueIDForURL(testString2);

            Assert.AreNotEqual(testString, testString2);
            Assert.AreNotEqual(expected, actual);
        }
    }
}
