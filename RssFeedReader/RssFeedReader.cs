using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Library.RssFeedReader
{
    public struct News
    {
        string link;
        string title;
        string story;

        public News(string link, string title, string story)
        {
            this.link = link;
            this.title = title;
            this.story = story;
        }
    }

    public static class RssFeedReader
    {
        public static List<News> getNewsFromRssURL(string rssUrl)
        {
            string err = string.Empty;
            List<News> newsList = new List<News>();

            try
            {
                using (XmlReader xmlReader = XmlReader.Create(rssUrl))
                {
                    SyndicationFeed syncFeed = SyndicationFeed.Load(xmlReader);

                    foreach (SyndicationItem syncItem in syncFeed.Items)
                    {
                        string url = syncFeed.Links.ToString();
                        string title = syncItem.Title.Text;
                        string story = syncItem.Summary.Text;

                        newsList.Add(new News(url, title, story));
                    }
                }
            }
            catch (Exception exc)
            {
                err += "Error failed to retrieve news from RSS URL of '";
                err += rssUrl + "' , detail : " + exc.Message;
            }

            return newsList;
        }
    }
}
