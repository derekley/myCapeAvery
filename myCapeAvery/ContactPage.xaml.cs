using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Essentials;

namespace myCapeAvery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactPage : ContentPage
    {
        public ContactPage()
        {
            InitializeComponent();
        }

        private void onWeb(object sender, EventArgs e)
        {
            // Visit website
            Xamarin.Essentials.Browser.OpenAsync("http://www.linkedin.com/in/derekley");
        }

        private void onSMS(object sender, EventArgs e)
        {
            // Send a text message
            var message = new SmsMessage("Hello, Mayor!", "7247999313");
            Sms.ComposeAsync(message);
        }

        private void onCall(object sender, EventArgs e)
        {
            // Start a call
            Xamarin.Essentials.PhoneDialer.Open("7247999313");
        }
    }
}