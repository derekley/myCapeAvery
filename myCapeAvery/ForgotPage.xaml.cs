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
    public partial class ForgotPage : ContentPage
    {
        public ForgotPage()
        {
            InitializeComponent();
        }

        private async void onRecover(object sender, EventArgs e)
        {
            // Password Recovery
            SQLiteConnection conn = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "MySQLite.db3"));
            SQLiteCommand cmd = new SQLiteCommand(conn);

            // 1. Check if Username exists + Maiden key, otherwise display 'incorrect key'.
            cmd.CommandText = ($"SELECT Password FROM User WHERE Username = '{txtUsername.Text}' AND Answer = '{txtSecurity.Text}'");
            var Users = cmd.ExecuteQuery<User>();
            if(!Users.Any())
            {
                await DisplayAlert("Error", "The Username and/or Answer Key was incorrect. Try again!", "OK");
            }
            // 2. If Username + Maiden key exists, display password, and redirect to Login.
            else
            {
                await DisplayAlert("Recovery", "Your Password is:  ' " + Users[0].Password + " '  !", "OK");
                await Navigation.PopToRootAsync();
            }
        }
    }
}