using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Library.RssFeedReader
{
    /// <summary>
    /// Strucutre of News,
    /// it contain 4 member which indicate 
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
        static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="
                                   + AppDomain.CurrentDomain.BaseDirectory + "rssFeedReader.mdf"
                                   + ";Integrated Security=True";

        /// <summary>
        /// Get the list of news from a RSS  Feed URL
        /// </summary>
        /// <param name="rssUrl"> the feed url</param>
        /// <returns>list of news</returns>
        public static List<News> getNewsFromRssURL(string rssUrl)
        {
            string err = string.Empty;
            List<News> newsList = new List<News>();

            if (Uri.IsWellFormedUriString(rssUrl, UriKind.Absolute))
            {
                try
                {
                    using (XmlReader xmlReader = XmlReader.Create(rssUrl))
                    {
                        SyndicationFeed syncFeed = SyndicationFeed.Load(xmlReader);

                        foreach (SyndicationItem syncItem in syncFeed.Items)
                        {
                            DateTime date = syncItem.PublishDate.DateTime;
                            string url = syncItem.Id;
                            string title = syncItem.Title.Text;

                            if (url == string.Empty || title == string.Empty)
                                continue;

                            newsList.Add(new News(date, url, title));
                        }
                    }
                }
                catch (Exception exc)
                {
                    err += "Error failed to retrieve news from RSS URL of '";
                    err += rssUrl + "' , detail : " + exc.Message;
                }
            }
            return newsList;
        }

        /// <summary>
        /// Get the list of non-expired news from a RSS  Feed URL
        /// </summary>
        /// <param name="rssUrl"> the feed url</param>
        /// <param name="expireDate"> the expired date of new</param>
        /// <returns>list of news</returns>
        public static List<News> getNewsFromRssURL(string rssUrl, DateTime expireDate)
        {        
            string err = string.Empty;
            List<News> newsList = new List<News>();

            if (Uri.IsWellFormedUriString(rssUrl, UriKind.Absolute))
            {
                try
                {
                    using (XmlReader xmlReader = XmlReader.Create(rssUrl))
                    {
                        SyndicationFeed syncFeed = SyndicationFeed.Load(xmlReader);

                        foreach (SyndicationItem syncItem in syncFeed.Items)
                        {
                            DateTime date = syncItem.PublishDate.DateTime;
                            string url = syncItem.Id;
                            string title = syncItem.Title.Text;

                            if ( url == string.Empty || title == string.Empty || ( date != null && date < expireDate)     )
                                continue;

                            newsList.Add(new News(date, url, title));
                        }
                    }
                }
                catch (Exception exc)
                {
                    err += "Error failed to retrieve news from RSS URL of '";
                    err += rssUrl + "' , detail : " + exc.Message;
                }
            }
            return newsList;
        }

        #region Methods of DataBase Manage

        /// <summary>
        /// Get the list RSS URL From table
        /// </summary>
        /// <returns>list of Rss URL</returns>
        public static List<string> getAllStoredUrl()
        {
            List<string> urls = new List<string>();
            string query = "SELECT url FROM tRssURL";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            urls.Add(reader["url"].ToString());
                        }
                    }
                    connection.Close();
                }
                catch (Exception exc)
                {
                    string errMss = "Cannot connect to database , error : " + exc.Message;
                }
            }
            return urls;
        }

        /// <summary>
        /// Manage the Rss Url in DataBase
        /// </summary>
        /// <param name="addList"> the new added urls</param>
        /// <param name="removeList"> the urls going to remove</param>
        public static void manageUrlInDb(List<string> addList, List<string> removeList)
        {
            if (addList.Count + removeList.Count == 0)
                return;

            string query = buildTheQueryToManageUrl(addList, removeList);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                catch (Exception exc)
                {
                    string errMss = "Cannot connect to database , error : " + exc.Message;
                }
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

            if (addList.Count > 0)
            {
                addQuery = "INSERT INTO tRssURL (url) VALUES ";

                foreach (string url in addList)
                {
                    addQuery += "('" + url + "'),";
                }
                addQuery = addQuery.TrimEnd(',');
                addQuery += " ;";
            }

            if (removeList.Count > 0)
            {
                remvQuey = "DELETE FROM tRssURL WHERE url IN (";

                foreach (string url in removeList)
                {
                    remvQuey += "'" + url + "',";
                }
                remvQuey = remvQuey.TrimEnd(',');
                remvQuey += ") ;";
            }

            return addQuery + remvQuey;
        }

        /// <summary>
        /// Download non- expire News to Database
        /// </summary>
        /// <param name="expireDate"> the expiredate of news </param>
        public static void downloadNewsToDb(DateTime expireDate)
        {
            List<string> urlList = getAllStoredUrl();
            List<News> newsList = new List<News>();

            if (urlList.Count == 0) return;

            foreach (string url in urlList)
            {
                newsList.AddRange(getNewsFromRssURL(url, expireDate));
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    foreach (News news in newsList)
                        insertTheNewsIfIsNotInDb(connection, news);

                    connection.Close();
                }
                catch (Exception exc)
                {
                    string errMss = "Cannot connect to database , error : " + exc.Message;
                }
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

            string query = "INSERT INTO tNews (title,link,updateTime) SELECT ";
            query += "'" + title + "','" + news.Link + "','" + sqlDateTime + "' ";
            query += "WHERE NOT EXISTS (SELECT Link FROM tNews WHERE tNews.link = '" + news.Link + "'); ";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception exc)
            {
                string errMss = "Cannot connect to database , error : " + exc.Message;
            }
        }

        /// <summary>
        /// Get List of News From Databse
        /// </summary>
        /// <returns> list of news</returns
        public static List<News> getNewsFromDb()
        {
            List<News> newsList = new List<News>();

            string query = "SELECT title,link,updateTime FROM tNews";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string title = reader["title"].ToString();
                            string link = reader["link"].ToString();
                            DateTime dateTime = DateTime.Parse((reader["updateTime"].ToString()));

                            newsList.Add(new News(dateTime, link, title));
                        }
                    }
                    connection.Close();
                }
                catch (Exception exc)
                {
                    string errMss = "Cannot connect to database , error : " + exc.Message;
                }
            }

            return newsList;
        }

        #endregion

        #region Un-used Methods
        private static string buildTheQueryToDownloadNews(List<News> newsList)
        {
            string query = string.Empty;

            if (newsList.Count > 0)
            {
                foreach (News news in newsList)
                {
                    string title = news.Title.Replace("\'", "");
                    string sqlDateTime = news.PublishDate.ToString("yyyy-MM-dd HH:mm:ss");

                    query += "INSERT INTO tNews (title,link,updateTime) SELECT ";
                    query += "'" + title + "','" + news.Link + "','" + sqlDateTime + "' ";
                    query += "WHERE NOT EXISTS (SELECT Max(Len(Link)) FROM tNews WHERE tNews.link = '" + news.Link + "'); ";
                }

            }
            return query;
        }

        public static void testInsert()
        {
            string query = "INSERT INTO tNews (title,link,updateTime) VALUES ('White House: Trump-Putin summit is on after hacking indictment','https://www.bbc.co.uk/news/world-us-canada-44830065','2018-07-14 02:27:55');";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                catch (Exception exc)
                {
                    string errMss = "Cannot connect to database , error : " + exc.Message;
                }
            }
        }
        #endregion

    }
}
