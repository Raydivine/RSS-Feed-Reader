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
        public RssFeedReaderApp()
        {
            string url = "http://rss.cnn.com/rss/edition_world.rss";

            InitializeComponent();

            List<News> newsList = RssFeedReader.getNewsFromRssURL(url);

            foreach(News news in newsList)
            {
                girdView_News.Rows.Add(news.DateTimeOff, news.Title, news.Link, news.Story);
            }

            //string news = "https://www.thestar.com.my/news/nation/2018/07/12/it-takes-more-than-money-to-buy-gr-jewellery/";
            //girdView_News.Rows.Add("2018-07-06 11.30pm", news, "Rosman",  "dfsddsfsdfsdfsdfsdfdsfsd");
        }

        private void tsB_manageRssURL_Click(object sender, EventArgs e)
        {
            ManageRssURL manageRssURLForm = new ManageRssURL();
            manageRssURLForm.Show();
        }

        private void girdView_News_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex != -1) //If user is clicked the URL cell 
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
    }
}
