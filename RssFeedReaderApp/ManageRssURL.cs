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

        List<string> addList = new List<string>();
        List<string> removeList = new List<string>();

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="
                                    + AppDomain.CurrentDomain.BaseDirectory + "rssFeedReader.mdf"
                                    + ";Integrated Security=True";

        public ManageRssURL()
        {
            InitializeComponent();
  
            List<string> urlsStore = RssFeedReader.getAllStoredUrl();
            urlsStore.ForEach(url => lb_RssUrlStore.Items.Add(url));

            ToolTip toolTip1 = new ToolTip();

            toolTip1.AutoPopDelay = 2000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 200;
            toolTip1.ShowAlways = true;

            toolTip1.SetToolTip(this.btn_addUrl, "Add a valid http link");
        }

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
                        addList.Add(txt_RssURL.Text);
                        lb_RssUrlStore.Items.Add(txt_RssURL.Text);
                    }
                }
            }     
            txt_RssURL.Clear();
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (lb_RssUrlStore.SelectedItem != null)
            {
                removeList.Add(lb_RssUrlStore.SelectedItem.ToString());
                lb_RssUrlStore.Items.Remove(lb_RssUrlStore.SelectedItem);
                txt_RssURL.Clear();
            }
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                RssFeedReader.manageUrlInDb(addList, removeList);
            }
        }

        private void btn_help_Click(object sender, EventArgs e)
        {
            string mss = "Please insert a valid Rss URL in the textbox, then click the 'Add' button.\n";
            mss += "If you would like remove an url, select it from the combo box, then click 'Remove' button\n";
            mss += "Your subscribed url will be updated after you close this form";

            MessageBox.Show(mss);
        }
    }
}
