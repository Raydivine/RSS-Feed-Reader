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
using System.Data.SqlClient;

namespace RssFeedReaderApp
{
    public partial class RssFeedReaderApp : Form
    {
        string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="
                                   + AppDomain.CurrentDomain.BaseDirectory + "rssFeedReader.mdf"
                                   + ";Integrated Security=True";
        string _cmd = "SELECT title,link,updateTime FROM tNews";

        SqlConnection _connection;
        SqlDataAdapter _da;
        DataTable _table = new DataTable();

        System.Threading.Thread t;

        public RssFeedReaderApp()
        {
            string url = "http://feeds.bbci.co.uk/news/world/rss.xml";
            //string url = "http://rss.cnn.com/rss/edition_world.rss";

            InitializeComponent();
            _connection = new SqlConnection(_connectionString);
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
                    string url = gv_News.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
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
            //RssFeedReader.testInsert();

            t = new System.Threading.Thread(runFeedReader);
            t.Start();

            /*
            RssFeedReader.downloadNewsToDb();

            dataGridViewBinding();

            */
        }

        private void runFeedReader()
        {
            string url = "http://feeds.bbci.co.uk/news/world/rss.xml";

            try
            {
                RssFeedReader.downloadNewsToDb();

                List<News> newsList = RssFeedReader.getNewsFromDb();

                //dataGridViewBinding();

            }
            catch(Exception exc)
            {
                string mss = "Cannot display news, error : " + exc.Message;
            }

        }

        private void dataGridViewBinding()
        {
            try
            {
                _da = new SqlDataAdapter(_cmd, _connection);
                _da.Fill(_table);
                dataGridView1.DataSource = _table;
                //gv_News.DataSource = _table;
            }
            catch (Exception exc)
            {
                string mss = "Cannot display news, error : " + exc.Message;
            }


        }

       
    }
}
