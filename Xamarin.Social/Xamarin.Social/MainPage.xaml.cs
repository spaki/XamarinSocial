using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.Social
{
    public partial class MainPage : ContentPage
    {
        private string youtubeKey = "[GOOGLE API KEY HERE!!!]";
        private HttpClient client = new HttpClient();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void BtnGet_Clicked(object sender, EventArgs e)
        {
            btnGet.IsEnabled = false;

            await GetYoutubeSubscribers();
            await GetTwitterFollows();
            await GetInstagramFollows();

            btnGet.IsEnabled = true;
        }

        private async Task GetYoutubeSubscribers()
        {
            var url = $"https://www.googleapis.com/youtube/v3/channels?part=statistics&id={txtYoutube.Text}&key={youtubeKey}";

            try
            {
                var response = await client.GetStringAsync(url);
                var result = JObject.Parse(response);
                lblYoutube.Text = result["items"][0]["statistics"]["subscriberCount"].ToString();
            }
            catch
            {
                lblYoutube.Text = "0";
            }
        }

        private async Task GetTwitterFollows()
        {
            var url = $"https://cdn.syndication.twimg.com/widgets/followbutton/info.json?screen_names={txtTwitter.Text}";

            try
            {
                var response = await client.GetStringAsync(url);
                var jsonArray = JArray.Parse(response);
                var result = JObject.Parse(jsonArray[0].ToString());
                lblTwitter.Text = result["followers_count"].ToString();
            }
            catch
            {
                lblTwitter.Text = "0";
            }
        }

        private async Task GetInstagramFollows()
        {
            var url = $"https://www.instagram.com/web/search/topsearch/?query={txtInstagram.Text}";

            try
            {
                var response = await client.GetStringAsync(url);
                var result = JObject.Parse(response);
                lblInstagram.Text = result["users"][0]["user"]["follower_count"].ToString();
            }
            catch
            {
                lblInstagram.Text = "0";
            }
        }
    }
}
