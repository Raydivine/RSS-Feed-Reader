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
        int expireDayFrame, refreshPeriodInMilliSec;
        DateTime expireDate;
        Thread t;
        SqlConnection _connection;
        SqlDataAdapter _sqlAdapter;
        string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="
                                   + AppDomain.CurrentDomain.BaseDirectory + "rssFeedReader.mdf;"
                                   + "Integrated Security=True";

        ManualResetEvent _resetEvent = new ManualResetEvent(false);

        enum TimeSpanInMillisec
        {
            tenSec = 10000,
            tenMin = 600000,
            thirtyMin = 1800000,
            oneHour = 3600000,
            twoHour = 7200000,
            fourHour = 14400000
        }

        /// <summary>
        /// 1. Create a sql Connecton
        /// 2. Read the expire Day setting from app config file
        ///      if fail to read, make it 3 days and write to the file
        /// 3. Retrieve the settings from config gile
        /// </summary>
        public RssFeedReaderApp()
        {
            InitializeComponent();
            _connection = new SqlConnection(_connectionString);

            retrieveDayFrameSettingFromAppConfig();
            retrieveRefreshPeriodSettingFromAppConfig();
        }

        /// <summary>
        /// 1. Read the expire Day setting from app config file
        ///      if fail to read, make it 3 days and write to the file
        /// 2. Update the Tool Strip Menu's button's check state
        /// </summary>
        private void retrieveDayFrameSettingFromAppConfig()
        {
            if (int.TryParse(ConfigurationSettings.AppSettings["daysFrame"], out expireDayFrame) == false) //Failed to read data from config file
            {
                expireDayFrame = 3; //Use 3 days as default setting
                setDaysFrameInAppConfig(expireDayFrame);
            }
            updateDaysFrameBtnCheckedState(expireDayFrame);
        }

        /// <summary>
        /// 1. Read the refresh period setting from app config file
        ///      if fail to read, make it 10 second and write to the file
        /// 2. Update the Tool Strip Menu's button's check state
        /// </summary>
        private void retrieveRefreshPeriodSettingFromAppConfig()
        {
            if (int.TryParse(ConfigurationSettings.AppSettings["refreshPeriod"], out refreshPeriodInMilliSec) == false) //Failed to read data from config file
            {
                refreshPeriodInMilliSec = (int)TimeSpanInMillisec.tenSec;
                setRefreshPeriodInAppConfig(refreshPeriodInMilliSec);
            }
            updateRefreshPeriodBtnCheckedState(refreshPeriodInMilliSec);
        }

        /// <summary>
        /// Start a background task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RssFeedReaderApp_Load(object sender, EventArgs e)
        {
            t = new Thread(runFeedReader);
            t.IsBackground = true;
            t.Start();
        }

        /// <summary>
        /// 1. Get a expire Date which is current date minus by number of days
        /// 2. Update the news in database by remove old news and insert fresh news
        /// 3. Bind the database to grid view for present all fresh news
        /// 4. Sleep for a period and then repeat the process
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
                _resetEvent.WaitOne(refreshPeriodInMilliSec);
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
                gv_News.Invoke((MethodInvoker)delegate { gv_News.DataSource = datatbale; });
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
                config.AppSettings.Settings["daysFrame"].Value = days.ToString();
                config.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception exc)
            {
                string errMss = "Cannot update app config file, error : " + exc.Message;
            }
        }

        /// <summary>
        /// Set the refresh period in App Config File
        /// </summary>
        /// <param name="milliSec"> refresh period in milliSecond </param>
        private void setRefreshPeriodInAppConfig(int milliSec)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                config.AppSettings.Settings["refreshPeriod"].Value = milliSec.ToString();
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
        private void updateDaysFrameBtnCheckedState(int days)
        {
            tsm_1day.Checked = false;
            tsm_2day.Checked = false;
            tsm_3day.Checked = false;

            switch (days)
            {
                case 1: tsm_1day.Checked = true; break;
                case 2: tsm_2day.Checked = true; break;
                case 3: tsm_3day.Checked = true; break;
                default: tsm_3day.Checked = true; break;
            }
        }

        private void updateRefreshPeriodBtnCheckedState(int refreshPeriodInMilliSec)
        {
            tsm_refresh_10Sec.Checked = false;
            tsm_refresh_10min.Checked = false;
            tsm_refresh_30mins.Checked = false;
            tsm_refresh_1Hour.Checked = false;
            tsm_refresh_2Hours.Checked = false;
            tsm_refresh_4Hours.Checked = false;

            switch (refreshPeriodInMilliSec)
            {
                case (int)TimeSpanInMillisec.tenSec: tsm_refresh_10Sec.Checked = true; break;
                case (int)TimeSpanInMillisec.tenMin: tsm_refresh_10min.Checked = true; break;
                case (int)TimeSpanInMillisec.thirtyMin: tsm_refresh_30mins.Checked = true; break;
                case (int)TimeSpanInMillisec.oneHour: tsm_refresh_1Hour.Checked = true; break;
                case (int)TimeSpanInMillisec.twoHour: tsm_refresh_2Hours.Checked = true; break;
                case (int)TimeSpanInMillisec.fourHour: tsm_refresh_4Hours.Checked = true; break;
                default: tsm_refresh_10Sec.Checked = true; break;
            }
        }

        /// <summary>
        /// Exit the thread delay,  which is _resetEvent.WaitOne(refreshPeriodInMilliSec) call in runFeedReader()
        /// </summary>
        private void exitThreadDelay()
        {
            _resetEvent.Set();
            _resetEvent.Reset();
        }
        #endregion

        #region Control Event Handler
        /// <summary>
        /// Exit this application when the Main From is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RssFeedReaderApp_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void tsB_manageRssURL_Click(object sender, EventArgs e)
        {
            ManageRssURL manageRssURLForm = new ManageRssURL();
            manageRssURLForm.actionMainForm = exitThreadDelay;
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
            updateDaysFrameBtnCheckedState(1);
            setDaysFrameInAppConfig(1);
            exitThreadDelay();
        }

        private void tsm_2day_Click(object sender, EventArgs e)
        {
            expireDayFrame = 2;
            updateDaysFrameBtnCheckedState(2);
            setDaysFrameInAppConfig(2);
            exitThreadDelay();
        }

        private void tsm_3day_Click(object sender, EventArgs e)
        {
            expireDayFrame = 3;
            updateDaysFrameBtnCheckedState(3);
            setDaysFrameInAppConfig(3);
            exitThreadDelay();
        }

        private void tsm_1Hour_Click(object sender, EventArgs e)
        {
            setRefreshPeriod(TimeSpanInMillisec.oneHour);
        }

        private void tsm_10min_Click(object sender, EventArgs e)
        {
            setRefreshPeriod(TimeSpanInMillisec.tenMin);
        }

        private void tsm_10Sec_Click(object sender, EventArgs e)
        {
            setRefreshPeriod(TimeSpanInMillisec.tenSec);
        }

        private void tsm_refresh_4Hours_Click(object sender, EventArgs e)
        {
            setRefreshPeriod(TimeSpanInMillisec.fourHour);
        }

        private void tsm_refresh_2Hours_Click(object sender, EventArgs e)
        {
            setRefreshPeriod(TimeSpanInMillisec.twoHour);
        }

        private void tsm_refresh_30mins_Click(object sender, EventArgs e)
        {
            setRefreshPeriod(TimeSpanInMillisec.thirtyMin);
        }

        private void setRefreshPeriod(TimeSpanInMillisec timeSpanInMilli)
        {
            refreshPeriodInMilliSec = (int)timeSpanInMilli;
            updateRefreshPeriodBtnCheckedState(refreshPeriodInMilliSec);
            setRefreshPeriodInAppConfig(refreshPeriodInMilliSec);
            exitThreadDelay();
        }

        private void tsb_help_Click(object sender, EventArgs e)
        {
            string mss = "This is a portable Rss Feed Reader\n"
            + "1.Click 'Register RSS URL' to subscribe your news\nAfter subscribe, the news will download and store offline\n"
            + "2.Click 'News Expire Days Frame' to set your news's expire days frame, the news wont be keep when the publish date has reach the date\n"
            + "3.Click 'News Refresh Period' to set your news's refresh period\n\n"
            + "You can click the new's link to open it in browser\n"
            + "Please go to this link for demo guide, thanks\n"
            + "https://github.com/Raydivine/RSS-Feed-Reader";

            MessageBox.Show(mss);
        }
        #endregion
    }



}