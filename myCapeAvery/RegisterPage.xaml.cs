using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;

using System.IO;

namespace myCapeAvery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void onRegister(object sender, EventArgs e)
        {
            // New Account Registration
            SQLiteConnection conn = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "MySQLite.db3"));
            SQLiteCommand cmd = new SQLiteCommand(conn);

            // 1. Validation: Does Username already exist, are textboxes empty, etc? If not, move on.
            cmd.CommandText = "SELECT Username FROM User";
            var Users = cmd.ExecuteQuery<User>();
            int dupes = 0;
            foreach (User i in Users)
            {
                if (i.Username == txtUsername.Text.ToLower())
                {
                    dupes += 1;
                }
            }

            if (dupes > 0)
            {
                await DisplayAlert("Error", "The Username is already taken. Please select another!", "OK");
            }
            else if (txtUsername.Text == "" || txtPassword.Text == "" || txtSecurity.Text == "" || pckType.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "All fields are required. Please enter data and/or select User Type!", "OK");
            }
            // 2. Add new User to database, then login and redirect to Home.
            else
            {
                if (pckType.SelectedItem.ToString() == "Official")
                {
                    var code = await DisplayPromptAsync("Error", "Selecting this User Type must be approved by the Mayor's office. Please enter the approval code to continue.", "OK", "Cancel");
                    if (code == "123189")
                    {
                        User newUser = new User();
                        newUser.Username = txtUsername.Text.ToLower();
                        newUser.Password = txtPassword.Text.ToLower();
                        newUser.Answer = txtSecurity.Text.ToLower();
                        newUser.Role = pckType.SelectedItem.ToString();
                        conn.Insert(newUser);

                        // Get UID for HomePage navigation
                        cmd.CommandText = ($"SELECT * FROM User WHERE Username = '{txtUsername.Text.ToLower()}' AND Password = '{txtPassword.Text.ToLower()}'");
                        Users = cmd.ExecuteQuery<User>();
                        var UID = Users[0].UID;

                        await Navigation.PushAsync(new HomePage(UID));
                    }
                    else
                    {
                        await DisplayAlert("Error", "The approval code entered was invalid and/or incorrect! Please select another User Type or try again!", "OK");
                    }
                }
                else
                {
                    User newUser = new User();
                    newUser.Username = txtUsername.Text.ToLower();
                    newUser.Password = txtPassword.Text.ToLower();
                    newUser.Answer = txtSecurity.Text.ToLower();
                    newUser.Role = pckType.SelectedItem.ToString();
                    conn.Insert(newUser);

                    // Get UID for HomePage navigation
                    cmd.CommandText = ($"SELECT * FROM User WHERE Username = '{txtUsername.Text.ToLower()}' AND Password = '{txtPassword.Text.ToLower()}'");
                    Users = cmd.ExecuteQuery<User>();
                    var UID = Users[0].UID;

                    await Navigation.PushAsync(new HomePage(UID));
                }
            }
        }
    }
}