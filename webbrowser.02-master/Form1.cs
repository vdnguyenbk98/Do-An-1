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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Gecko.Xpcom.Initialize(Application.StartupPath+ "\\xulrunner");
            geckoWebBrowser1.Navigate("https://www.google.com");
           
           
            txtUrl.Text = "https://www.google.com";
            
        }

      //Mở kết nối
            SqlConnection kn = new SqlConnection(@"Data Source=DESKTOP-VGNG2FP;Initial Catalog=lichsuweb;Integrated Security=True");


        //bieu tuong kinh lup
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            
            Gecko.GeckoWebBrowser web = tabControl1.SelectedTab.Controls[0] as Gecko.GeckoWebBrowser;
            
            
                web.Navigate(txtUrl.Text);
            
        }
        //nut tao newtab
        Gecko.GeckoWebBrowser webTab = null;
        private void btnNewtab_Click(object sender, EventArgs e)
        {
            TabPage tab = new TabPage();
            tab.Text = "New Tab";
            tabControl1.Controls.Add(tab);
            tabControl1.SelectTab(tabControl1.TabCount - 1);
            webTab = new Gecko.GeckoWebBrowser();
            webTab.Parent = tab;
            webTab.Dock = DockStyle.Fill;            
            webTab.Navigate("https://www.google.com");          
            txtUrl.Text= "https://www.google.com";
            webTab.DocumentCompleted += WebTab_DocumentCompleted1;
            webTab.Navigated += WebTab_Navigated;
        }
        //lưu lịch sử ở các newtab
        private void WebTab_Navigated(object sender, Gecko.GeckoNavigatedEventArgs e)
        {
            String t = webTab.Url.ToString();
            String them;
            kn.Open();
            them = "INSERT INTO  dbo.history VALUES('" + t + "','" + DateTime.Now.ToString("hh:mm:ss") + "','" + DateTime.Now.ToString("MM/dd/yyyy") + "')";
            SqlCommand commandthem = new SqlCommand(them, kn);
            commandthem.ExecuteNonQuery();
            kn.Close();
        }

        private void WebTab_DocumentCompleted1(object sender, Gecko.Events.GeckoDocumentCompletedEventArgs e)
        {
            tabControl1.SelectedTab.Text = webTab.DocumentTitle;

        }

        
        //nut go back
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Gecko.GeckoWebBrowser web = tabControl1.SelectedTab.Controls[0] as Gecko.GeckoWebBrowser;
            if (web != null)
            {
                if (web.CanGoBack)
                    web.GoBack();
            }
        }
        //nut go forward
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Gecko.GeckoWebBrowser web = tabControl1.SelectedTab.Controls[0] as Gecko.GeckoWebBrowser;
            if (web != null)
            {
                if (web.CanGoForward)
                    web.GoForward();
            }

        }
        //nhap url vao textbox cua cac tab
        private void txtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            Gecko.GeckoWebBrowser web = tabControl1.SelectedTab.Controls[0] as Gecko.GeckoWebBrowser;
            
            
                if(e.KeyCode==Keys.Enter)
                { web.Navigate(txtUrl.Text); }
            
        }
        //nut home
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Gecko.GeckoWebBrowser web = tabControl1.SelectedTab.Controls[0] as Gecko.GeckoWebBrowser;
           if(web!=null)
            {
                web.Navigate("https://www.google.com");
            }
        }

        /// <summary>
        ///  nut refresh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            Gecko.GeckoWebBrowser web = tabControl1.SelectedTab.Controls[0] as Gecko.GeckoWebBrowser;
            if (web != null)
            {
                web.Refresh();
            }
        }
        //nut stop
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Gecko.GeckoWebBrowser web = tabControl1.SelectedTab.Controls[0] as Gecko.GeckoWebBrowser;
            if (web != null)
            {
                web.Stop();
            }
        }
        //nut close-all-tab
        private void btnHistory_Click(object sender, EventArgs e)
        {
            
            Form2 f2 = new Form2();
            f2.ShowDialog();

        }
        //lưu lịch sử ở tabpage1
        private void geckoWebBrowser1_Navigated(object sender, Gecko.GeckoNavigatedEventArgs e)
        {
            String t = geckoWebBrowser1.Url.ToString();
            String them;
            kn.Open();
            them = "INSERT INTO  dbo.history VALUES('"+t+"','" + DateTime.Now.ToString("hh:mm:ss") + "','"+ DateTime.Now.ToString("MM/dd/yyyy") + "')";
            SqlCommand commandthem = new SqlCommand(them, kn);
            commandthem.ExecuteNonQuery();
            kn.Close();
            

        }
        // title cho tabpage1
        private void geckoWebBrowser1_DocumentCompleted_1(object sender, Gecko.Events.GeckoDocumentCompletedEventArgs e)
        {
            tabControl1.SelectedTab.Text = geckoWebBrowser1.DocumentTitle;
        }
    }
}
