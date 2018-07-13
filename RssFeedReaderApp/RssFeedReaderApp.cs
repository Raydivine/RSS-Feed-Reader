﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library.RssFeedReader;

namespace RssFeedReaderApp
{
    public partial class RssFeedReaderApp : Form
    {
        System.Threading.Thread t;

        public RssFeedReaderApp()
        {
            string url = "http://feeds.bbci.co.uk/news/world/rss.xml";
            //string url = "http://rss.cnn.com/rss/edition_world.rss";

            InitializeComponent();
            
            /*
            List<News> newsList = RssFeedReader.getNewsFromRssURL(url);

            foreach(News news in newsList)
            {
                girdView_News.Rows.Add(news.DateTime, news.Title, news.Link);
            }*/
            
        }

        private void tsB_manageRssURL_Click(object sender, EventArgs e)
        {
            ManageRssURL manageRssURLForm = new ManageRssURL();
            manageRssURLForm.Show();
        }

        private void girdView_News_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex != -1) //If user is clicked the URL cell 
            {
                try
                {
                    string url = girdView_News.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    System.Diagnostics.Process.Start(url);
                }
                catch (Exception exc)
                {
                    string error = "Could not open the link in browser, error : " + exc.Message;
                    MessageBox.Show(error);
                }
            }
        }

        private void RssFeedReaderApp_Load(object sender, EventArgs e)
        {
   
            t = new System.Threading.Thread(runFeedReader);
            t.Start();
            
        }

        private void runFeedReader()
        {
            string url = "http://feeds.bbci.co.uk/news/world/rss.xml";

            while (true)
            {
                List<News> newsList = RssFeedReader.getNewsFromRssURL(url);

                try
                {

                    foreach (News news in newsList)
                    {
                        foreach (DataGridViewRow row in girdView_News.Rows)
                        {
                            if (row.Cells[2].Value != null && row.Cells[2].Value.ToString() != news.Link)
                                girdView_News.Rows.Add(news.DateTime, news.Title, news.Link);
                        }
                        
                    }
                }
                catch(Exception exc)
                {
                    string eroor = exc.Message;
                }
                
            }

        }

     

       
    }
}
