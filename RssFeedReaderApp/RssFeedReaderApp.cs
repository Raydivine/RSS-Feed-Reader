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
        int expireDayFrame;
        DateTime expireDate;

        SqlConnection _connection;
        SqlDataAdapter _sqlAdapter;
        string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="
                                   + AppDomain.CurrentDomain.BaseDirectory + "rssFeedReader.mdf;"
                                   + "Integrated Security=True";
        /// <summary>
        /// 1. Create a sql Connecton
        /// 2. Read the expire Day setting from app config file
        ///      if fail to read, make it 3 days and write to the file
        /// 3. update the Tool Strip Menu's button's check state
        /// </summary>
        public RssFeedReaderApp()
        {
            InitializeComponent();
            _connection = new SqlConnection(_connectionString);

            if (int.TryParse(ConfigurationSettings.AppSettings["key"], out expireDayFrame) == false) //Failed to read data from config file
            {
                expireDayFrame = 3; //Use 3 days as default setting
                setDaysFrameInAppConfig(expireDayFrame);
            }
            updateButtonCheckedState(expireDayFrame);
        }

        /// <summary>
        /// Start a background task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RssFeedReaderApp_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(runFeedReader);
            t.IsBackground = true;
            t.Start();
        }

        /// <summary>
        /// 1. Get a expire Date which is current date minus by number of days
        /// 2. Update the news in database by remove old news and insert fresh news
        /// 3. Bind the database to grid view for present all fresh news
        /// 4. Sleep for 10 sec and then repeat the process
        /// </summary>
        private void runFeedReader()
        {
            while (true)
            {
                expireDate = DateTime.Now.AddDays(-expireDayFrame); // ExpireDay = Today - expireDayFrame 

                try
                {
                    RssFeedReader.UpdateNewsInDb(expireDate);
                    bindTheNewsDbToGridView();                
                }
                catch (Exception exc)
                {
                    string errMss = "Cannot display news, error : " + exc.Message;
                }
                Thread.Sleep(10000); // 10 second
            }
        }

        #region Methods
        /// <summary>
        /// 1. Bind the sql table of 'tNews' to window form's gridView
        /// 2. The gridView need to be invoke because it is running in a thread
        /// </summary>
        private void bindTheNewsDbToGridView()
        {
            string query = "SELECT title,link,publishDate FROM tNews ORDER BY publishDate DESC; ";

            try
            {
                DataTable datatbale = new DataTable();
                _sqlAdapter = new SqlDataAdapter(query, _connection);
                _sqlAdapter.Fill(datatbale);
                gv_News.Invoke((MethodInvoker) delegate { gv_News.DataSource = datatbale; });
            }
            catch (Exception exc)
            {
                string errMss = "Cannot display news, error : " + exc.Message;
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
                string errMss = "Cannot update app config file, error : " + exc.Message;
            }
        }

        /// <summary>
        /// Update the toolStrip button check state, only 1 button can be checked
        /// </summary>
        /// <param name="number of days"></param>
        private void updateButtonCheckedState(int days)
        {
            tsm_1day.Checked = false;
            tsm_2day.Checked = false;
            tsm_3day.Checked = false;

            switch (days)
            {
                case 1  : tsm_1day.Checked = true; break;
                case 2  : tsm_2day.Checked = true; break;
                case 3  : tsm_3day.Checked = true; break;
                default : tsm_3day.Checked = true; break;
            }
        }

        /// <summary>
        /// Exit this application when the Main From is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            string url = string.Empty;

            if (e.ColumnIndex == 1 && e.RowIndex != -1) //If user is clicked the URL cell 
            {
                try
                {
                    url = gv_News.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    System.Diagnostics.Process.Start(url); //Open the url in browser
                }
                catch (Exception exc)
                {
                    string errMss = "Could not open the link in browser, error : " + exc.Message;
                    MessageBox.Show(errMss);
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
            string mss = @"This is a portable Rss Feed Reader\n\n
                           1. Click 'Register RSS URL' to subscribe your news\nAfter subscribe, the news will download and store offline\n\n
                           2. Click 'News Expire Days Frame' to set your news's expire days frame
                           , the news wont be keep when the publish date has reach the date\n\n
                           You can click the new's link to open it in browser\n
                           Please go to this link for demo guide, thanks\n
                           https://github.com/Raydivine/RSS-Feed-Reader";

            MessageBox.Show(mss);
        }
        #endregion     
    }



}
