using Microsoft.VisualStudio.TestTools.UnitTesting;
using RssFeedReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssFeedReader.Tests
{
    [TestClass()]
    public class RssFeedReaderTests
    {
        [TestMethod()]
        public void getNewsFromRssURLTest()
        {
            string url = "https://www.thestar.com.my/rss/";

            List<News> newsList = RssFeedReader.getNewsFromRssURL(url);
            Assert.AreNotEqual(0, newsList.Count());

        }
    }
}