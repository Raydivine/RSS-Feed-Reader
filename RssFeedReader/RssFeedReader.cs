using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace RssFeedReader
{
    public struct News
    {
        string title;
        string story;
    }

    public class RssFeedReader
    {
        public RssFeedReader() { }

        public List<News> getNewsFromRssURL(string rssUrl)
        {
            List<News> newsList = new List<News>();

            return newsList;
        }
    }
}
