using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Library.RssFeedReader
{
    /// <summary>
    /// Strucutre of News,
    /// it contain 4 member which indicate the news's elements
    /// </summary>
    public struct News
    {
        DateTime publishDate;
        string link;
        string title;
        string story;

        #region Properties
        public DateTime PublishDate
        {
            get
            {
                return this.publishDate;
            }
            set
            {
                this.publishDate = value;
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

        #region Constructor
        public News(DateTime dateTime, string link, string title)
        {
            this.publishDate = dateTime;
            this.link = link;
            this.title = title;
            this.story = string.Empty; 
        }

        public News(DateTime dateTime, string link, string title, string story)
        {
            this.publishDate = dateTime;
            this.link = link;
            this.title = title;
            this.story = story;
        }
        #endregion
    }

    public static class RssFeedReader
    {
        static string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="
                                        + AppDomain.CurrentDomain.BaseDirectory + "rssFeedReader.mdf;"
                                        + "Integrated Security=True";

        /// <summary>
        /// Get the list of news from a RSS  Feed URL
        /// </summary>
        /// <param name="rssUrl"> the feed url</param>
        /// <returns>list of news</returns>
        public static List<News> getNewsFromRssURL(string rssUrl)
        {
            DateTime date;
            List<News> newsList = new List<News>();
            string url = string.Empty, title = string.Empty;

            try
            {
                if (Uri.IsWellFormedUriString(rssUrl, UriKind.Absolute))
                    using (XmlReader xmlReader = XmlReader.Create(rssUrl))
                    {
                        SyndicationFeed syncFeed = SyndicationFeed.Load(xmlReader);

                        foreach (SyndicationItem syncItem in syncFeed.Items)
                        {
                            url = syncItem.Id;
                            title = syncItem.Title.Text;
                            date = syncItem.PublishDate.DateTime;

                            if (url != string.Empty && title != string.Empty)
                                newsList.Add(new News(date, url, title));
                        }
                    }
            }
            catch (Exception exc)
            {
                string errMss = "Error failed to retrieve news from RSS URL of '" + rssUrl + "' , detail : " + exc.Message;
            }         
            return newsList;
        }

        /// <summary>
        /// Get the list of non-expired news from a RSS  Feed URL
        /// </summary>
        /// <param name="rssUrl"> the feed url</param>
        /// <param name="expireDate"> the expired date of new</param>
        /// <returns>list of news</returns>
        public static List<News> getFreshNewsFromUrl(string rssUrl, DateTime expireDate)
        {
            DateTime date;
            List<News> newsList = new List<News>();
            string url = string.Empty, title = string.Empty;

            try
            {
                if (Uri.IsWellFormedUriString(rssUrl, UriKind.Absolute))
                    using (XmlReader xmlReader = XmlReader.Create(rssUrl))
                    {
                        SyndicationFeed syncFeed = SyndicationFeed.Load(xmlReader);

                        foreach (SyndicationItem syncItem in syncFeed.Items)
                        {
                            url = syncItem.Id;
                            title = syncItem.Title.Text;
                            date = syncItem.PublishDate.DateTime;

                            if (url != string.Empty && title != string.Empty && (date != null && date > expireDate) )
                                newsList.Add(new News(date, url, title));
                        }
                    }
            }
            catch (Exception exc)
            {
                string errMss = "Cannot retrieve news from RSS URL of '" + rssUrl + "' , detail : " + exc.Message;
            }
            return newsList;
        }

        #region Methods of DataBase Manage

        /// <summary>
        /// Get the list of RSS URL From Database
        /// </summary>
        /// <returns>list of Rss URL</returns>
        public static List<string> getUrlListFromDb()
        {
            List<string> urlList = new List<string>();
            string query = "SELECT url FROM tRssURL";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            urlList.Add(reader["url"].ToString());
                        }
                    }
                    connection.Close();
                }            
            }
            catch (Exception exc)
            {
                string errMss = "Cannot get list of url from database , error : " + exc.Message;
            }         
            return urlList;
        }

        /// <summary>
        /// Manage the Rss Url in DataBase
        /// </summary>
        /// <param name="addList"> the new added urls</param>
        /// <param name="removeList"> the urls going to remove</param>
        public static void updateUrlInDb(List<string> addList, List<string> removeList)
        {
            if (addList.Count + removeList.Count >0)
            {
                string query = buildTheQueryToManageUrl(addList, removeList);
                executeSqlQuery(query);
            }           
        }

        /// <summary>
        /// Build a query that add new added urls, and remove the unwanted urls
        /// </summary>
        /// <param name="addList"> the new added urls</param>
        /// <param name="removeList"> the urls going to remove</param>
        /// <returns>  a query to update databse</returns>
        private static string buildTheQueryToManageUrl(List<string> addList, List<string> removeList)
        {
            string addQuery = string.Empty, remvQuey = string.Empty;

            if (removeList.Count > 0)
            {
                remvQuey = "DELETE FROM tRssURL WHERE url IN (";

                foreach (string url in removeList)
                {
                    remvQuey += "'" + url + "',";
                }
                remvQuey = remvQuey.TrimEnd(',');
                remvQuey += ") ; ";
            }

            foreach (string url in addList)
            {
                addQuery += "INSERT INTO tRssURL (url) SELECT '" + url + "' ";
                addQuery += "WHERE NOT EXISTS (SELECT url FROM tRssURL WHERE tRssURL.url = '" + url + "'); ";
            }
            return remvQuey + addQuery;
        }

        /// <summary>
        /// Update the news in database, by deleting old news and inserting fresh news
        /// </summary>
        /// <param name="expireDate">if the new's publish date is older than this date, then is expire</param>
        public static void UpdateNewsInDb(DateTime expireDate)
        {
            removeExpireNewsInDb(expireDate);
            downloadFreshNewsToDb(expireDate);
        }

        /// <summary>
        /// Remove the expire news in database, the expire news has older publishDate than expireDate
        /// </summary>
        /// <param name="expireDate">if the new's publish date is older than this date, then is expire</param>
        private static void removeExpireNewsInDb(DateTime expireDate)
        {
            string query = "DELETE FROM tNews WHERE publishDate < '" + expireDate.ToString("yyyy-MM-dd HH:mm:ss") + "';";
            executeSqlQuery(query);
        }

        /// <summary>
        /// Download non- expire News to Database
        /// </summary>
        /// <param name="expireDate">if the new's publish date is older than this date, then is expire</param>
        private static void downloadFreshNewsToDb(DateTime expireDate)
        {
            List<News>  newsList = new List<News>();
            List<string> urlList = getUrlListFromDb();
            
            if (urlList.Count == 0) return;

            urlList.ForEach(url => newsList.AddRange(getFreshNewsFromUrl(url, expireDate)) );
            
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    foreach (News news in newsList)
                        insertTheNewsIfIsNotInDb(connection, news);

                    connection.Close();
                }           
            }
            catch (Exception exc)
            {
                string errMss = "Cannot download news to database , error : " + exc.Message;
            }       
        }

        /// <summary>
        /// Inset a news into Db
        /// </summary>
        /// <param name="connection"> an opened databse connection</param>
        /// <param name="news"> a news that is going to add into database</param>
        private static void insertTheNewsIfIsNotInDb(SqlConnection connection, News news)
        {
            string title = news.Title.Replace("\'", "");
            string sqlDateTime = news.PublishDate.ToString("yyyy-MM-dd HH:mm:ss");
            string query = "INSERT INTO tNews (title,link,publishDate) SELECT "
                           + "'" + title + "','" + news.Link + "','" + sqlDateTime + "' "
                           +"WHERE NOT EXISTS (SELECT Link FROM tNews WHERE tNews.link = '" + news.Link + "'); ";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception exc)
            {
                string errMss = "Cannot insert a news into , error : " + exc.Message;
            }
        }

        /// <summary>
        ///  Execute a sql query
        /// </summary>
        /// <param name="query">a valid sql query</param>
        private static void executeSqlQuery(string query)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch (Exception exc)
            {
                string errMss = "Cannot execute a sql query , error : " + exc.Message;
            }
        }
        #endregion

        #region Un-used Methods
        /// <summary>
        /// Get List of News From Databse
        /// </summary>
        /// <returns> list of news</returns
        public static List<News> getNewsFromDb()
        {
            DateTime dateTime;
            List<News> newsList = new List<News>();
            string title = string.Empty, link = string.Empty, query = "SELECT title,link,publishDate FROM tNews";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            link = reader["link"].ToString();
                            title = reader["title"].ToString();
                            dateTime = DateTime.Parse((reader["publishDate"].ToString()));

                            newsList.Add(new News(dateTime, link, title));
                        }
                    }
                    connection.Close();
                }            
            }
            catch (Exception exc)
            {
                string errMss = "Cannot retrieve news from database , error : " + exc.Message;
            }
            return newsList;
        }

        /// <summary>
        /// Build a query that download all the news to Database
        /// </summary>
        /// <param name="newsList"> list of news</param>
        /// <returns></returns>
        private static string buildTheQueryToDownloadNews(List<News> newsList)
        {
            string query = string.Empty, title = string.Empty, sqlDateTime = string.Empty;

            if (newsList.Count > 0)
            {
                foreach (News news in newsList)
                {
                    title = news.Title.Replace("\'", "");
                    sqlDateTime = news.PublishDate.ToString("yyyy-MM-dd HH:mm:ss");

                    query = "INSERT INTO tNews (title,link,publishDate) SELECT "
                            + "'" + title + "','" + news.Link + "','" + sqlDateTime + "' "
                            + "WHERE NOT EXISTS (SELECT Max(Len(Link)) FROM tNews WHERE tNews.link = '" + news.Link + "'); ";
                }

            }
            return query;
        }

        /// <summary>
        /// Dummy method to test inserting news into Database
        /// </summary>
        public static void testInsert()
        {
            string query = "INSERT INTO tNews (title,link,publishDate) VALUES ('White House: Trump-Putin summit is on after hacking indictment','https://www.bbc.co.uk/news/world-us-canada-44830065','2018-07-14 02:27:55');";
            executeSqlQuery(query);
        }
        #endregion

    }
}
