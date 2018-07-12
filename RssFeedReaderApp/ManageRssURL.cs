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


        // String connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Source\Repos\RSS-Feed-Reader\RssFeedReader\rssFeedReader.mdf;Integrated Security = True";

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="
                                    + AppDomain.CurrentDomain.BaseDirectory + "rssFeedReader.mdf"
                                    + ";Integrated Security=True";


        public ManageRssURL()
        {
            InitializeComponent();

            List<string> urlsStore = RssFeedReader.getAllStoredUrl();
            urlsStore.ForEach(url => lb_RssUrlStore.Items.Add(url));
        }

        private void btn_addUrl_Click(object sender, EventArgs e)
        {
            string input = txt_RssURL.Text;

            if (input != string.Empty &&  ! lb_RssUrlStore.Items.Contains(input))
            {
                addList.Add(txt_RssURL.Text);
                lb_RssUrlStore.Items.Add(txt_RssURL.Text);
            }
            txt_RssURL.Clear();
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            removeList.Add(lb_RssUrlStore.SelectedItem.ToString());
            lb_RssUrlStore.Items.Remove(lb_RssUrlStore.SelectedItem);      
            txt_RssURL.Clear();
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                List<string> urls = lb_RssUrlStore.Items.Cast<string>().ToList();
                RssFeedReader.manageUrlInDataBase(addList, removeList);
            }
        }
    }
}
