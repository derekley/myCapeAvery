using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;

using System.IO;
using System.Runtime.CompilerServices;

namespace myCapeAvery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private int UID;
        private bool isGuest = false;

        public HomePage(string guest)
        {
            SQLiteConnection conn = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "MySQLite.db3"));
            SQLiteCommand cmd = new SQLiteCommand(conn);
            int count = 1;

            InitializeComponent();
            btnSettings.IsEnabled = false;
            btnAds.IsEnabled = false;
            isGuest = true;

            // Every 5 seconds will display new Advertisement on the HomePage from the list of ACTIVE advertisements in the database.
            // It will also rely on User's devices to re-evaluate current Advertisements and change their status to EXPIRED, if necessary.
            // 1. Re-evaluate existing Advertisements to establish truly ACTIVE ones to display.
            var Today = DateTime.Today.ToUniversalTime();

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                // 2. Now have an accurate list of ACTIVE Advertisements; Rotate through them at 10-second intervals.
                cmd.CommandText = ($"SELECT * FROM Advertisement WHERE EndDate <= '{Today}'");
                int Ads = cmd.ExecuteQuery<Advertisement>().Count();
                // No ACTIVE Advertisements
                if (Ads == 0)
                {
                    // Do Nothing.
                }
                else
                {
                    cmd.CommandText = ($"SELECT * FROM Advertisement WHERE EndDate <= '{Today}'");
                    var AdData = cmd.ExecuteQuery<Advertisement>();
                    if (count < Ads)
                    {
                        btnAds.Text = AdData[count - 1].Body;
                        count += 1;
                    }
                    else if (count == Ads)
                    {
                        btnAds.Text = AdData[count - 1].Body;
                        count = 1;
                    }
                }

                return true; // return true to repeat counting, false to stop timer
            });
        }

        public HomePage(int uID)
        {
            SQLiteConnection conn = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "MySQLite.db3"));
            SQLiteCommand cmd = new SQLiteCommand(conn);
            int count = 1;
            bool show = true;

            InitializeComponent();

            // Global variable assignment for later Navigation
            UID = uID;

            // Every 5 seconds will display new Advertisement on the HomePage from the list of ACTIVE advertisements in the database.
            // It will also rely on User's devices to re-evaluate current Advertisements and change their status to EXPIRED, if necessary.
            // 1. Re-evaluate existing Advertisements to establish truly ACTIVE ones to display.
            var Today = DateTime.Today.ToUniversalTime();

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                // 2. Now have an accurate list of ACTIVE Advertisements; Rotate through them at 10-second intervals.
                cmd.CommandText = ($"SELECT * FROM Advertisement WHERE EndDate <= '{Today}'");
                int Ads = cmd.ExecuteQuery<Advertisement>().Count();
                // No ACTIVE Advertisements
                if (Ads == 0)
                {
                    // Do Nothing.
                }
                else
                {
                    if(show)
                    {
                        show = false;
                        lblInfo.Text = "Click Here to Place an Ad!";
                    }
                    else
                    {
                        show = true;
                        lblInfo.Text = "Advertisement";
                    }
                    cmd.CommandText = ($"SELECT * FROM Advertisement WHERE EndDate <= '{Today}'");
                    var AdData = cmd.ExecuteQuery<Advertisement>();
                    if (count < Ads)
                    {
                        btnAds.Text = AdData[count-1].Body;
                        count += 1;
                    }
                    else if (count == Ads)
                    {
                        btnAds.Text = AdData[count-1].Body;
                        count = 1;
                    }
                }

                return true; // return true to repeat counting, false to stop timer
            });
        }

        private async void onSights(object sender, EventArgs e)
        {
            // Navigate to Attractions
            await Navigation.PushAsync(new AttractionsPage());
        }

        private async void onEvents(object sender, EventArgs e)
        {
            // Navigate to Events
            await Navigation.PushAsync(new EventsPage());
        }

        private async void onWeather(object sender, EventArgs e)
        {
            // Navigate to Weather
            await Navigation.PushAsync(new WeatherPage());
        }

        private async void onFeed(object sender, EventArgs e)
        {
            // Navigate to Newsfeed
            if (isGuest == true)
            {
                await Navigation.PushAsync(new NewsPage("guest"));
            }
            else
            {
                await Navigation.PushAsync(new NewsPage(UID));
            }
        }

        private async void onContact(object sender, EventArgs e)
        {
            // Navigate to Contact
            await Navigation.PushAsync(new ContactPage());
        }

        private async void onAbout(object sender, EventArgs e)
        {
            // Navigate to About
            await Navigation.PushAsync(new AboutPage());
        }

        private async void onSettings(object sender, EventArgs e)
        {
            // Navigate to Settings
            await Navigation.PushAsync(new SettingsPage(UID));
        }

        private async void onAd(object sender, EventArgs e)
        {
            // Navigate to Advertisement
            await Navigation.PushAsync(new AdPage());
        }
    }
}