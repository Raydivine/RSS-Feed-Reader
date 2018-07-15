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
using System.Threading;
using System.Configuration;

namespace RssFeedReaderApp
{
    public partial class RssFeedReaderApp : Form
    {
        string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="
                                   + AppDomain.CurrentDomain.BaseDirectory + "rssFeedReader.mdf"
                                   + ";Integrated Security=True";

        SqlConnection _connection;
        SqlDataAdapter _sqlAdapter;
        Thread t;

        int expireDayFrame;
        DateTime expireDate;

        public RssFeedReaderApp()
        {
            InitializeComponent();
            _connection = new SqlConnection(_connectionString);

            if (int.TryParse(ConfigurationSettings.AppSettings["key"], out expireDayFrame) == false) //Failed to read data from config file
                expireDayFrame = 3;

            updateButtonCheckedState(expireDayFrame);
        }

        private void RssFeedReaderApp_Load(object sender, EventArgs e)
        {
            t = new Thread(runFeedReader);
            t.IsBackground = true;
            t.Start();
        }

        private void runFeedReader()
        {
            while (true)
            {
                expireDate = DateTime.Now.AddDays(-expireDayFrame); // ExpireDay = Today - expireDayFrame 

                try
                {
                    RssFeedReader.downloadNewsToDb(expireDate);
                    removeExpireNewsThenBindTheDbToGridView();                
                }
                catch (Exception exc)
                {
                    string mss = "Cannot display news, error : " + exc.Message;
                }
                Thread.Sleep(10000);
            }

        }

        #region Methods
        /// <summary>
        /// remove the exipire new from database, then bind it to grid view
        /// </summary>
        private void removeExpireNewsThenBindTheDbToGridView()
        {
            string query = "DELETE FROM tNews WHERE updateTime < '" + expireDate.ToString("yyyy-MM-dd HH:mm:ss") + "';";
            query += "SELECT title,link,updateTime FROM tNews ORDER BY updateTime DESC; ";

            try
            {
                DataTable datatbale = new DataTable();
                _sqlAdapter = new SqlDataAdapter(query, _connection);
                _sqlAdapter.Fill(datatbale);
                gv_News.Invoke((MethodInvoker)delegate { gv_News.DataSource = datatbale; });
            }
            catch (Exception exc)
            {
                string mss = "Cannot display news, error : " + exc.Message;
            }
        }

        /// <summary>
        /// Set the days Frame in App Config file
        /// </summary>
        /// <param name="days"> number of days to indicate whenthe news expire</param>
        private void setDaysFrameInAppConfig(int days)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                config.AppSettings.Settings["key"].Value = days.ToString();
                config.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception exc)
            {
                string message = "Cannot update app config file, error : " + exc.Message;
            }
        }

        /// <summary>
        /// Update the toolStrip button check state, byright only 1 can be checked
        /// </summary>
        /// <param name="number of days"></param>
        private void updateButtonCheckedState(int days)
        {
            tsm_1day.Checked = false;
            tsm_2day.Checked = false;
            tsm_3day.Checked = false;

            switch (days)
            {
                case 1:
                    tsm_1day.Checked = true;
                    break;
                case 2:
                    tsm_2day.Checked = true;
                    break;
                case 3:
                    tsm_3day.Checked = true;
                    break;
                default:
                    tsm_2day.Checked = true;
                    break;
            }
        }

        private void RssFeedReaderApp_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
        #endregion

        #region Control Event Handler
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

        private void tsm_1day_Click(object sender, EventArgs e)
        {
            expireDayFrame = 1;
            updateButtonCheckedState(1);
            setDaysFrameInAppConfig(1);
        }

        private void tsm_2day_Click(object sender, EventArgs e)
        {
            expireDayFrame = 2;
            updateButtonCheckedState(2);
            setDaysFrameInAppConfig(2);
        }

        private void tsm_3day_Click(object sender, EventArgs e)
        {
            expireDayFrame = 3;
            updateButtonCheckedState(3);
            setDaysFrameInAppConfig(3);
        }

        private void tsb_help_Click(object sender, EventArgs e)
        {
            string mss = "This is a portable Rss Feed Reader\n\n";
            mss += "1. Click 'Register RSS URL' to subscribe your news\nAfter subscribe, the news will download and store offline\n\n";
            mss += "2. Click 'News Expire Days Frame' to set your news's expire days frame";
            mss += ", the news wont be keep when the publish date has reach the date\n\n";
            mss += "You can click the new's link to open it in browser\n";
            mss += "Please go to this link for demo guide, thanks\n";
            mss += "https://github.com/Raydivine/RSS-Feed-Reader";

            MessageBox.Show(mss);

        }
        #endregion



        
    }



}
