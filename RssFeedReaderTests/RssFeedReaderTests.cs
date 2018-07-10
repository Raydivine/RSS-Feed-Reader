using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.RssFeedReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.RssFeedReader.Tests
{
    [TestClass()]
    public class RssFeedReaderTests
    {
        [TestMethod()]
        public void getNewsFromRssURLTest()
        {
            // CNN world RSS feed URL
            string url = "http://rss.cnn.com/rss/edition_world.rss";

            List<News> newsList = Library.RssFeedReader.RssFeedReader.getNewsFromRssURL(url);
            Assert.AreNotEqual(0, newsList.Count());
        }
    }
}