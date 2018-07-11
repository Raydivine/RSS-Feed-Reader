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

namespace RssFeedReaderApp
{
    public partial class ManageRssURL : Form
    {
        String errMss = string.Empty;

       // String connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Source\Repos\RSS-Feed-Reader\RssFeedReader\rssFeedReader.mdf;Integrated Security = True";

        
        String connectionString
                = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="
                    + AppDomain.CurrentDomain.BaseDirectory + "rssFeedReader.mdf"
                    + ";Integrated Security=True";

        public ManageRssURL()
        {
            InitializeComponent();
        }

        private void btn_addUrl_Click(object sender, EventArgs e)
        {
            lb_RssUrlStore.Items.Add(txt_RssURL.Text);


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("can open");
                }
                catch(Exception exc)
                {

                    errMss += "Cannot connect to database , error : " + exc.Message;
                    MessageBox.Show(errMss);
                }
            }



            //txt_RssURL.Text;
        }
    }
}
