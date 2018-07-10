using System;
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

            lb_newsReader.DataSource = newsList;

        }
    }
}
