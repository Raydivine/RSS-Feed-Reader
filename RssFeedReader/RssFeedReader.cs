using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="
                                   + AppDomain.CurrentDomain.BaseDirectory + "rssFeedReader.mdf"
                                   + ";Integrated Security=True";

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

        public static void manageUrlInDataBase(List<string> addList, List<string> removeList)
        {
            if (addList.Count + removeList.Count == 0)
                return;

            string query = constructQuery(addList, removeList);

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

        private static string constructQuery(List<string> addList, List<string> removeList)
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
                addQuery = addQuery.Substring(0, addQuery.Length - 1);
                addQuery += " ;";
            }

            if (removeList.Count > 0)
            {
                remvQuey = "DELETE FROM tRssURL WHERE url IN (";

                foreach (string url in removeList)
                {
                    remvQuey += "'" + url + "',";
                }
                remvQuey = remvQuey.Substring(0, remvQuey.Length - 1);
                remvQuey += ") ;";
            }

            return addQuery + remvQuey;
        }

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
