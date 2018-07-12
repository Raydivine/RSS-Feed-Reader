using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Library.RssFeedReader
{
    public struct News
    {
        DateTimeOffset dateTimeOff;
        string link;
        string title;
        string story;

        #region Properties
        public DateTimeOffset DateTimeOff
        {
            get
            {
                return this.dateTimeOff;
            }
            set
            {
                this.dateTimeOff = value;
            }
        }

        public string Link
        {
            get
            {
                return this.link;
            }
            set
            {
                this.link = value;
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }

        public string Story
        {
            get
            {
                return this.story;
            }
            set
            {
                this.story = value;
            }
        }
        #endregion

        public News(DateTimeOffset dateTimeOff, string link, string title, string story)
        {
            this.dateTimeOff = dateTimeOff;
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
                        DateTimeOffset date = syncItem.PublishDate;
                        string url = syncItem.Id;
                        string title = syncItem.Title.Text;
                        string story = syncItem.Summary.Text;

                        if (url == string.Empty || title == string.Empty)
                            continue;
                        
                        newsList.Add(new News(date, url, title, story));
                        
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
