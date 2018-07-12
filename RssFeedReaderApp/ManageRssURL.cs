﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RssFeedReaderApp
{
    public partial class ManageRssURL : Form
    {
        string errMss = string.Empty;
        string itemToRemove = string.Empty;

       // String connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Source\Repos\RSS-Feed-Reader\RssFeedReader\rssFeedReader.mdf;Integrated Security = True";

        
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="
                                    + AppDomain.CurrentDomain.BaseDirectory + "rssFeedReader.mdf"
                                    + ";Integrated Security=True";

        public ManageRssURL()
        {
            InitializeComponent();
            
         

            List<string> urlsStore = getAllStoredUrl();
            urlsStore.ForEach(url => lb_RssUrlStore.Items.Add(url));
        }

        private void btn_addUrl_Click(object sender, EventArgs e)
        {
            lb_RssUrlStore.Items.Add(txt_RssURL.Text);
            txt_RssURL.Clear();
        }

        private List<string> getAllStoredUrl()
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

                        while( reader.Read() )
                        {
                            urls.Add(reader["url"].ToString());
                        }
                    }
                    connection.Close();
                }
                catch (Exception exc)
                {
                    errMss += "Cannot connect to database , error : " + exc.Message;
                    MessageBox.Show(errMss);
                }
            }
            return urls;
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            lb_RssUrlStore.Items.Remove(lb_RssUrlStore.SelectedItem);
            txt_RssURL.Clear();
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                List<string> urls = lb_RssUrlStore.Items.Cast<string>().ToList();
                string query = "TRUNCATE TABLE tRssURL; ";

                foreach (string url in urls)
                {
                    query += "INSERT INTO tRssURL (url) VALUES ('" + url + "') ;";
                }

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
                        errMss += "Cannot connect to database , error : " + exc.Message;
                        MessageBox.Show(errMss);
                    }
                }
            }
        }
    }
}
