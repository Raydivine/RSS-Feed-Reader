using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Library.RssFeedReader;

namespace RssFeedReaderApp
{
    public partial class ManageRssURL : Form
    {
        string errMss = string.Empty;
        string itemToRemove = string.Empty;
        List<string> removeList = new List<string>();
        string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="
                                    + AppDomain.CurrentDomain.BaseDirectory + "rssFeedReader.mdf"
                                    + ";Integrated Security=True";

        /// <summary>
        /// 1. Get all the stored urls from database
        /// 2. Add the urls into listBox
        /// 3. Set the toolTip delay
        /// </summary>
        public ManageRssURL()
        {
            InitializeComponent();
  
            List<string> urlsStore = RssFeedReader.getUrlListFromDb();
            urlsStore.ForEach(url => lb_RssUrlStore.Items.Add(url));

            ToolTip toolTip1 = new ToolTip();

            toolTip1.AutoPopDelay = 2000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 200;
            toolTip1.ShowAlways = true;

            toolTip1.SetToolTip(this.btn_addUrl, "Add a valid http link");
        }

        /// <summary>
        /// 1. Check is the input non-empty 
        /// 2. Check is the input an url , if not display a message to user
        /// 3. Add the input into listBox
        /// 4. Clear the textBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_addUrl_Click(object sender, EventArgs e)
        {
            string input = txt_RssURL.Text;

            if (input != string.Empty)
            {
                if (Uri.IsWellFormedUriString(input, UriKind.Absolute) == false)
                    MessageBox.Show("The input you enter is not like an Url, please check again");
                else
                {
                    if (!lb_RssUrlStore.Items.Contains(input))
                    {
                        lb_RssUrlStore.Items.Add(txt_RssURL.Text);
                    }
                }
            }     
            txt_RssURL.Clear();
        }

        /// <summary>
        /// 1. Add the selected item into removeList
        /// 2. Remove the slected item from listBox
        /// 3. Clear the textBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (lb_RssUrlStore.SelectedItem != null)
            {
                removeList.Add(lb_RssUrlStore.SelectedItem.ToString());
                lb_RssUrlStore.Items.Remove(lb_RssUrlStore.SelectedItem);
                txt_RssURL.Clear();
            }
        }

        /// <summary>
        /// When the form is closing by user, update the url in database by passing the item in listBox and removeList.
        /// The item in listBox might contain both existed and new added url
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            List<string> existed = lb_RssUrlStore.Items.Cast<string>().ToList();

            if (e.CloseReason == CloseReason.UserClosing)
            {
                RssFeedReader.updateUrlInDb(existed, removeList);
            }
       
        }

        /// <summary>
        /// Display help message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_help_Click(object sender, EventArgs e)
        {
            string mss = "Please insert a valid Rss URL in the textbox, then click the 'Add' button.\n"
                       + "If you would like remove an url, select it from the combo box, then click 'Remove' button\n"
                       + "Your subscribed url will be updated after you close this form";

            MessageBox.Show(mss);
        }
    }
}
