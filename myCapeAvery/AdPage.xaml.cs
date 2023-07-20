using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.IO;

using SQLite;

namespace myCapeAvery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdPage : ContentPage
    {
        public AdPage()
        {
            InitializeComponent();
        }

        private async void onDate(object sender, DateChangedEventArgs e)
        {
            var Today = new DateTime();
            Today = DateTime.Today.ToUniversalTime();

            int length = (datAd.Date.ToUniversalTime() - Today).Days;
            int cost = length * 10;

            lblInfo.Text = "$10 x " + length + " day(s) = ";
            lblCost.Text = cost.ToString("c");
        }

        private async void onPost(object sender, EventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "MySQLite.db3"));
            SQLiteCommand cmd = new SQLiteCommand(conn);

            // 1. Get Today's date
            var Today = new DateTime();
            Today = DateTime.Today.ToUniversalTime();

            int length = (datAd.Date.ToUniversalTime() - Today).Days;
            int cost = length * 10;

            cmd.CommandText = ($"SELECT Body FROM Advertisement WHERE EndDate <= '{Today}'");
            var Ads = cmd.ExecuteQuery<Advertisement>();
            int dupes = 0;
            foreach (Advertisement i in Ads)
            {
                if (i.Body == txtBody.Text)
                {
                    dupes += 1;
                }
            }

            // 2. Checks
            if (dupes > 0)
            {
                await DisplayAlert("Error", "An active advertisement with that message already exists. Please try again!", "OK");
            }
            else if (txtBody.Text == "")
            {
                await DisplayAlert("Error", "Advertisement body message required (up to 55 characters). Please try again!", "OK");
            }
            else if (datAd.Date <= Today)
            {
                await DisplayAlert("Error", "End Date of the advertisement run cannot be in the past. Please try again!", "OK");
            }
            else if (length < 30)
            {
                await DisplayAlert("Error", "Advertisement runs require a minimum commitment of 30 days. Please try again!", "OK");
            }
            else
            {
                // 3. Post the ad
                Advertisement newAd = new Advertisement();
                newAd.Body = txtBody.Text;
                newAd.EndDate = datAd.Date.ToUniversalTime();
                newAd.Length = length;
                conn.Insert(newAd);

                lblInfo.Text = "$10 x " + length + " day(s) = ";
                lblCost.Text = cost.ToString("c");

                lblPay.IsVisible = true;
                lblPay.Text = "Payment Processing.";
                await Task.Delay(1000);
                lblPay.Text = "Payment Processing..";
                await Task.Delay(1000);
                lblPay.Text = "Payment Processing...";
                await Task.Delay(1000);
                lblPay.Text = "Payment Processing.";
                await Task.Delay(1000);
                lblPay.Text = "Payment Processing..";
                await Task.Delay(1000);
                lblPay.Text = "Payment Processing...";
                await Task.Delay(1000);

                await DisplayAlert("Success", "Your advertisement has been processed. Thank you!", "OK");
                await Navigation.PopAsync();
            }
        }
    }
}