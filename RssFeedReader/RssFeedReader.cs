using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Library.RssFeedReader
{
    public struct News
    {
        DateTime dateTime;
        string link;
        string title;
        string story;

        #region Properties
        public DateTime DateTime
        {
            get
            {
                return this.dateTime;
            }
            set
            {
                this.dateTime = value;
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

        public News(DateTime dateTime, string link, string title, string story)
        {
            this.dateTime = dateTime;
            this.link = link;
            this.title = title;
            this.story = story;
        }   
    }

    public static class RssFeedReader
    {
        static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="
                                   + AppDomain.CurrentDomain.BaseDirectory + "rssFeedReader.mdf"
                                   + ";Integrated Security=True";

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
                        DateTime date = syncItem.PublishDate.DateTime;
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

        #region Methods of DataBase Manage

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

        private static string buildTheQueryToManageUrl(List<string> addList, List<string> removeList)
        {
            string addQuery = string.Empty, remvQuey = string.Empty;

            if (addList.Count > 0)
            {
                addQuery = "INSERT INTO tRssURL (url) VALUES ";

                foreach (string url in addList)
                {
                    addQuery += "('" + url + "'),";
                    //query += "INSERT INTO tRssURL (url) VALUES ('" + url + "'); ";
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

        public static void downloadNewsToDb()
        {
            List<string> urlList = getAllStoredUrl();
            List<News> newsList = new List<News>();
            string query = string.Empty;
            string urla = "http://feeds.bbci.co.uk/news/world/rss.xml";
            urlList.Add(urla);

            if (urlList.Count == 0) return; 

            foreach(string url in urlList )
            {
                newsList.AddRange(getNewsFromRssURL(url));
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

        private static void insertTheNewsIfIsNotInDb(SqlConnection connection, News news)
        {
            string query = "SELECT link FROM tNews WHERE tNews.Link ='" + news.Link + "'";

            try
            {

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        string title = news.Title.Replace("\'", "");
                        string sqlDateTime = news.DateTime.ToString("yyyy-MM-dd HH:mm:ss");

                        title = "hhh";
                        news.Link = "www";

                        query = "INSERT INTO tNews (title,link,updateTime) VALUES ('" + title + "','" + news.Link + "','" + sqlDateTime + "'); ";


                        reader.Close();
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception exc)
            {
                string errMss = "Cannot connect to database , error : " + exc.Message;
            }
        }


        private static string buildTheQueryToDownloadNews(List<News> newsList)
        {
            string query = string.Empty;

            if (newsList.Count > 0)
            {
                foreach (News news in newsList)
                {
                    string title = news.Title.Replace("\'", "");
                    string sqlDateTime = news.DateTime.ToString("yyyy-MM-dd HH:mm:ss");
            
                    query += "INSERT INTO tNews (title,link,updateTime) SELECT ";
                    query += "'" + title + "','" + news.Link + "','" + sqlDateTime  + "' ";
                    query += "WHERE NOT EXISTS (SELECT Max(Len(Link)) FROM tNews WHERE tNews.link = '" + news.Link + "'); ";      
                }

            }
            return query;
        }

        #endregion 

    }
}
