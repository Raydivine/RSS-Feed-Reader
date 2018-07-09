using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Xml;

namespace RssFeedReader
{
    public struct News
    {
        string title;
        string story;

        public News(string t, string s)
        {
            title = t;
            story = s;
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
                        newsList.Add(new News(syncItem.Title.Text, syncItem.Summary.Text));
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
