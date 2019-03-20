using HtmlAgilityPack;
using RPNWebsiteTools.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPNWebsiteTools
{
    public partial class Form1 : Form
    {
        List<YChannel> channels = new List<YChannel>();
        List<TwitterAcc> twitters = new List<TwitterAcc>();
        List<FbAcc> fbs = new List<FbAcc>();

        private string ytApiKey;

        public Form1()
        {
            InitializeComponent();

            ytApiKey = new ApiKeys().youtubeApi;

            LoadYouTubeChannelsList();

            LoadTwitterAccountsList();

            LoadFbAccountsList();

            LoadListViews();
            // LoadChannelsList();
        }

        private void LoadFbAccountsList()
        {
            fbs.Add(new FbAcc("RPN 639Khz BATAC", "RPNDZRLBATAC639KHZ"));
            fbs.Add(new FbAcc("RPN DZBS 1368 Baguio", "rpndzbs1368baguio"));
            fbs.Add(new FbAcc("RPN - DZKI / TV10, Iriga City", "RpnDzkiIrigaCity"));
            fbs.Add(new FbAcc("RPN DYKB 1404 Khz. Bacolod City", "rpndykb"));
            fbs.Add(new FbAcc("DYKC CEBU", "dykc675cebu"));
            fbs.Add(new FbAcc("RPN DXKO Cagayan De Oro", "RPNCagayanDeOro"));
            fbs.Add(new FbAcc("RPN DXKT Davao", "dxkt1071khz"));
            fbs.Add(new FbAcc("RPN DXKD Dipolog City", "1053khz"));
            fbs.Add(new FbAcc("DXKS RPN RADIO Surigao", "RPNSurigao"));
            fbs.Add(new FbAcc("RPN DXDX Gensan", "RPNDXDXGensan"));
            fbs.Add(new FbAcc("RPN DXXX Zamboanga", "RPNDXXXRadyoRonda"));
        }

        private void LoadTwitterAccountsList()
        {
            twitters.Add(new TwitterAcc("RPN Baguio", "BaguioRpn"));
            twitters.Add(new TwitterAcc("RPN Iriga", "dzkiiriga"));
            twitters.Add(new TwitterAcc("dykb.bacolod", "BacolodDykb"));
            twitters.Add(new TwitterAcc("dykc.cebu", "DykcCebu"));
            twitters.Add(new TwitterAcc("DXKO.Cagayan de Oro", "DXKO_CDO"));
            twitters.Add(new TwitterAcc("RPN Davao", "DavaoRpn"));
            twitters.Add(new TwitterAcc("RPN DXKD DIPOLOG", "DxkdRpn"));
            twitters.Add(new TwitterAcc("DXDXRPNGENSAN", "DxdxRpngensan"));
            twitters.Add(new TwitterAcc("dxks.surigao", "RPN_DXKS"));
            twitters.Add(new TwitterAcc("dxxx.zamboanga", "DxxxZamboanga"));
        }

        private void LoadYouTubeChannelsList()
        {
            channels.Add(new YChannel("RPN Baguio", "UC0wWamBzg1feSykTkqVEhPg"));
            channels.Add(new YChannel("RPN Iriga", "UCYqdBZOEe_jGAEFanu0j1mA"));
            channels.Add(new YChannel("RPN Bacolod", "UCRW2tujJbwQwN7fQIVPKkfw"));
            channels.Add(new YChannel("RPN Cebu", "UCENkWYZZ0M5MRTsAosAH57g"));
            channels.Add(new YChannel("RPN Davao", "UC2MchIQ5N6VDgT2053JsZjQ"));
            channels.Add(new YChannel("Surigao - RPN Surigao", "UCTEqreysOA6y1AbU4jmxl0A"));
            channels.Add(new YChannel("RPN Cagayan de Oro", "UCKRRue5YpvvercB3qGgbsMQ"));
            channels.Add(new YChannel("RPN Zamboanga", "UCzcBKBmq4DDUEIW6XvvnzqA"));
        }

        private async Task LoadListViews()
        {
            lvChannels.Items.Clear();
            lvTwitter.Items.Clear();
            lvFacebook.Items.Clear();
            lvChannels.MultiSelect = false;
            lvTwitter.MultiSelect = false;
            lvFacebook.MultiSelect = false;

            await LoadYouTubeStats();

            await LoadTwitterStats();

            await LoadFacebookStats();

            var date = DateTime.Now;
            lblUpdate.Text = "Last updated on " + date.ToString("dd MMM yyyy") + " at " + date.ToString("hh:mm tt");
        }

        private async Task LoadFacebookStats()
        {
            // Declare and construct the ColumnHeader objects.
            ColumnHeader header1, header2, header3, header4;
            header1 = new ColumnHeader();
            header2 = new ColumnHeader();
            header3 = new ColumnHeader();
            header4 = new ColumnHeader();

            // Set the text, alignment and width for each column header.
            header1.Text = "Page name";
            header1.TextAlign = HorizontalAlignment.Left;
            header1.Width = 110;

            header2.TextAlign = HorizontalAlignment.Left;
            header2.Text = "Likes";
            header2.Width = 80;

            header3.TextAlign = HorizontalAlignment.Left;
            header3.Text = "Followers";
            header3.Width = 80;

            header4.TextAlign = HorizontalAlignment.Left;
            header4.Text = "User ID";
            header4.Width = 300;

            // Add the headers to the ListView control.
            lvFacebook.Columns.Add(header1);
            lvFacebook.Columns.Add(header2);
            lvFacebook.Columns.Add(header3);
            lvFacebook.Columns.Add(header4);
            lvFacebook.View = View.Details;
            lvFacebook.FullRowSelect = true;

            foreach (var c in fbs)
            {
                var listViewItem = new ListViewItem(c.PageName);
                //listViewItem.SubItems.Add(await GetFBLikesCount(c.PageId));
                String[] count = await GetFBFollowerCount(c.PageId);
                listViewItem.SubItems.Add(count[0]);
                listViewItem.SubItems.Add(count[1]);
                listViewItem.SubItems.Add(c.PageId);

                lvFacebook.Items.AddRange(new ListViewItem[] { listViewItem });
            }
        }

        private async Task<string[]> GetFBFollowerCount(string pageId)
        {
            try
            {
                Debug.WriteLine(pageId);
                HtmlWeb htmlWeb = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = htmlWeb.Load("https://facebook.com/pg/" + pageId + "/community/?ref=page_internal");

                string uLikes, uFollowers;
                if (pageId == "rpndykb" || pageId == "RPNCagayanDeOro")
                {
                    uLikes = "//*[@id=\"u_0_m\"]/div[1]/div/div/div/div/div/div/div[1]/div[1]";
                    uFollowers = "//*[@id=\"u_0_m\"]/div[1]/div/div/div/div/div/div/div[2]/div[1]";
                }
                else
                {
                    uLikes = "//*[@id=\"u_0_l\"]/div[1]/div/div/div/div/div/div/div[1]/div[1]";
                    uFollowers = "//*[@id=\"u_0_l\"]/div[1]/div/div/div/div/div/div/div[2]/div[1]";
                }

                string likes = doc.DocumentNode.SelectNodes(uLikes)[0].InnerText;
                string followers = doc.DocumentNode.SelectNodes(uFollowers)[0].InnerText;
                Debug.WriteLine("Likes: " + likes);
                Debug.WriteLine("Follows: " + followers);

                return new string[] { likes, followers };
            }
            catch (Exception)
            {
                return new string[] { string.Empty, string.Empty };
            }
            //followers_count&quot;:4,&quot;friends

        }

        private async Task<string> GetFBLikesCount(string pageId)
        {
            Debug.WriteLine(pageId);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://www.facebook.com/" + pageId);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            Debug.WriteLine(content);
            //followers_count&quot;:4,&quot;friends
           
            return string.Empty;
        }

        private async Task LoadTwitterStats()
        {
            // Declare and construct the ColumnHeader objects.
            ColumnHeader header1, header2, header3;
            header1 = new ColumnHeader();
            header2 = new ColumnHeader();
            header3 = new ColumnHeader();

            // Set the text, alignment and width for each column header.
            header1.Text = "Screen name";
            header1.TextAlign = HorizontalAlignment.Left;
            header1.Width = 90;

            header2.TextAlign = HorizontalAlignment.Left;
            header2.Text = "Followers";
            header2.Width = 32;

            header3.TextAlign = HorizontalAlignment.Left;
            header3.Text = "User ID";
            header3.Width = 300;

            // Add the headers to the ListView control.
            lvTwitter.Columns.Add(header1);
            lvTwitter.Columns.Add(header2);
            lvTwitter.Columns.Add(header3);
            lvTwitter.View = View.Details;
            lvTwitter.FullRowSelect = true;

            foreach (var c in twitters)
            {
                var listViewItem = new ListViewItem(c.ScreenName);
                listViewItem.SubItems.Add(await GetFollowersCount(c.Userid));
                listViewItem.SubItems.Add(c.Userid);

                lvTwitter.Items.AddRange(new ListViewItem[] { listViewItem });
            }
        }

        private async Task<string> GetFollowersCount(string sname)
        {
            Debug.WriteLine(sname);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://twitter.com/" + sname);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            Debug.WriteLine(content);
            //followers_count&quot;:4,&quot;friends
            string fCount = await GetText("followers_count&quot;:", ",&quot;", content);
            return fCount;
        }

        private async Task LoadYouTubeStats()
        {
            // Declare and construct the ColumnHeader objects.
            ColumnHeader header1, header2, header3;
            header1 = new ColumnHeader();
            header2 = new ColumnHeader();
            header3 = new ColumnHeader();

            // Set the text, alignment and width for each column header.
            header1.Text = "Channel name";
            header1.TextAlign = HorizontalAlignment.Left;
            header1.Width = 70;

            header2.TextAlign = HorizontalAlignment.Left;
            header2.Text = "Sub Count";
            header2.Width = 32;

            header3.TextAlign = HorizontalAlignment.Left;
            header3.Text = "Channel ID";
            header3.Width = 300;

            // Add the headers to the ListView control.
            lvChannels.Columns.Add(header1);
            lvChannels.Columns.Add(header2);
            lvChannels.Columns.Add(header3);
            lvChannels.View = View.Details;
            lvChannels.FullRowSelect = true;

            foreach (var c in channels)
            {
                var listViewItem = new ListViewItem(c.ChannelName);
                listViewItem.SubItems.Add(await GetSubsCount(c.ChannelId));
                listViewItem.SubItems.Add(c.ChannelId);

                lvChannels.Items.AddRange(new ListViewItem[] { listViewItem });
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //await GetSubsCount();
            LoadListViews();
        }


        private async Task<string> GetSubsCount(string channelId)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://www.googleapis.com/youtube/v3/channels?part=statistics&id=" + channelId + "&key=" + ytApiKey);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            string subCount = await GetText("subscriberCount" + "\": \"", "\",", content);
            Debug.WriteLine(subCount);
            return subCount;
        }

        private async Task<string> GetText(string startString, string endString, string content)
        {
            int _start, _end;
            _start = content.IndexOf(startString, 0) + startString.Length;
            _end = content.IndexOf(endString, _start);
            return content.Substring(_start, _end - _start);
        }

        private void lvChannels_DoubleClick(object sender, EventArgs e)
        {
            var item = lvChannels.SelectedItems[0].SubItems[2].Text;
            Debug.WriteLine(item);

            Process.Start("https://youtube.com/channel/" + item);
        }

        private void lvTwitter_DoubleClick(object sender, EventArgs e)
        {
            var item = lvTwitter.SelectedItems[0].SubItems[2].Text;
            Debug.WriteLine(item);

            Process.Start("https://twitter.com/" + item);
        }

        private void lvFacebook_DoubleClick(object sender, EventArgs e)
        {
            var item = lvFacebook.SelectedItems[0].SubItems[3].Text;
            Debug.WriteLine(item);

            Process.Start("https://facebook.com/pg/" + item);
        }
    }

    public class YChannel
    {
        public string ChannelName { get; set; }
        public string ChannelId { get; set; }
        public string URL { get; set; }

        public YChannel(string c_name, string id)
        {
            this.ChannelName = c_name;
            this.ChannelId = id;
            this.URL = "https://www.youtube.com/channel/" + id;
        }
    }

    public class TwitterAcc
    {
        public string ScreenName { get; set; }
        public string Userid { get; set; }
        public string AccUrl { get; set; }

        public TwitterAcc(string screen_name, string id)
        {
            this.ScreenName = screen_name;
            this.Userid = id;
            this.AccUrl = @"https://twitter.com/" + screen_name;
        }
    }

    public class FbAcc
    {
        public string PageName { get; set; }
        public string PageId { get; set; }

        public FbAcc(string pname, string pid)
        {
            this.PageName = pname;
            this.PageId = pid;
        }
    }
}
