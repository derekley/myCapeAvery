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
    public partial class SettingsPage : ContentPage
    {
        private int UID;
        private string role, username;

        public SettingsPage(int uID)
        {
            InitializeComponent();

            SQLiteConnection conn = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "MySQLite.db3"));
            SQLiteCommand cmd = new SQLiteCommand(conn);

            UID = uID;

            // Get Current User Info.
            cmd.CommandText = ($"SELECT * FROM User WHERE UID = '{UID}'");
            var getData = cmd.ExecuteQuery<User>();
            username = getData[0].Username;
            role = getData[0].Role;

            if(role == "Official")
            {
                staAdmin.IsVisible = true;
                staUser.IsVisible = false;

                // FOR OFFICIALS
                cmd.CommandText = ($"SELECT * FROM User");
                int numUsers = cmd.ExecuteQuery<User>().Count;
                cmd.CommandText = ($"SELECT SUM(Length) FROM Advertisement");
                int sumAds = cmd.ExecuteScalar<int>();
                int cost = sumAds * 10;

                // Print Info
                lblUserData.Text = numUsers.ToString();
                lblAdData.Text = cost.ToString("c");
            }
            else
            {
                staUser.IsVisible = true;
                staAdmin.IsVisible = false;

                // FOR RESIDENTS
                cmd.CommandText = ($"SELECT * FROM Post WHERE UID = '{UID}'");
                int numPosts = cmd.ExecuteQuery<Post>().Count;
                cmd.CommandText = ($"SELECT SUM(NumLikes) FROM Post WHERE UID = '{UID}'");
                int sumLikes = cmd.ExecuteScalar<int>();
                cmd.CommandText = ($"SELECT SUM(NumDislikes) FROM Post WHERE UID = '{UID}'");
                int sumDislikes = cmd.ExecuteScalar<int>();

                // Print Info
                lblUsername.Text = username;
                lblUserType.Text = role;
                lblPostData.Text = numPosts.ToString();
                lblLikeData.Text = sumLikes.ToString();
                lblDislikeData.Text = sumDislikes.ToString();
            }
        }

        private async void onDeactivate(object sender, EventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "MySQLite.db3"));
            SQLiteCommand cmd = new SQLiteCommand(conn);

            if (role == "Official")
            {
                cmd.CommandText = ($"SELECT * FROM User WHERE Username = '{srcUsers.Text}'");
                int getCount = cmd.ExecuteQuery<User>().Count;
                if (getCount == 0)
                {
                    // NO USER
                    await DisplayAlert("Error", "No User was found by that username.", "OK");
                }
                else
                {
                    cmd.CommandText = ($"SELECT * FROM User WHERE Username = '{srcUsers.Text}'");
                    var getData = cmd.ExecuteQuery<User>();
                    if(getData[0].Role == "Official")
                    {
                        var code = await DisplayPromptAsync("Error", "This user is also an Admin and must be approved by the Mayor's office. Please enter the approval code to continue.", "OK", "Cancel");
                        if (code == "123189")
                        {
                            User deleteUser = new User();
                            deleteUser.UID = getData[0].UID;
                            conn.Delete(deleteUser);
                            await DisplayAlert("Confirmation", "User has been Deactivated!", "OK");
                        }
                    }
                    else
                    {
                        var choice = await DisplayAlert("Warning", "Are you sure that you would like to Deactivate the account belonging to: " + srcUsers.Text + "?", "Yes", "Cancel");
                        if (choice)
                        {
                            User deleteUser = new User();
                            deleteUser.UID = getData[0].UID;
                            conn.Delete(deleteUser);
                            await DisplayAlert("Confirmation", "User has been Deactivated!", "OK");
                        }
                    }
                }
            }
            else
            {
                var choice = await DisplayAlert("Warning", "Are you sure that you would like to Deactivate your account? This is non-reversible.", "Deactivate", "Cancel");
                if(choice)
                {
                    User deleteUser = new User();
                    deleteUser.UID = UID;
                    conn.Delete(deleteUser);
                    await Navigation.PopToRootAsync();
                }
            }

            // FOR OFFICIALS
            cmd.CommandText = ($"SELECT * FROM User");
            int numUsers = cmd.ExecuteQuery<User>().Count;
            cmd.CommandText = ($"SELECT SUM(Length) FROM Advertisement");
            int sumAds = cmd.ExecuteScalar<int>();
            int cost = sumAds * 10;

            // Print Info
            lblUserData.Text = numUsers.ToString();
            lblAdData.Text = cost.ToString("c");
        }

        private async void onPromote(object sender, EventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "MySQLite.db3"));
            SQLiteCommand cmd = new SQLiteCommand(conn);

            cmd.CommandText = ($"SELECT * FROM User WHERE Username = '{srcUsers.Text}'");
            int getCount = cmd.ExecuteQuery<User>().Count;
            if (getCount == 0)
            {
                // NO USER
                await DisplayAlert("Error", "No User was found by that username.", "OK");
            }
            else
            {
                cmd.CommandText = ($"SELECT * FROM User WHERE Username = '{srcUsers.Text}'");
                var getData = cmd.ExecuteQuery<User>();
                if(getData[0].Role == "Official")
                {
                    await DisplayAlert("Error", "This user is already an Admin user.", "OK");
                }
                else
                {
                    var choice = await DisplayAlert("Confirmation", "Are you sure that you would like to Promote the account belonging to: " + srcUsers.Text + "?", "Yes", "Cancel");
                    if (choice)
                    {
                        User updateUser = new User();
                        updateUser.UID = getData[0].UID;
                        updateUser.Username = getData[0].Username;
                        updateUser.Password = getData[0].Password;
                        updateUser.Answer = getData[0].Answer;
                        updateUser.Role = "Official";
                        conn.Update(updateUser);
                        await DisplayAlert("Confirmation", "User has been Promoted!", "OK");
                    }
                }
            }
        }

        private async void onSearch(object sender, EventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "MySQLite.db3"));
            SQLiteCommand cmd = new SQLiteCommand(conn);

            cmd.CommandText = ($"SELECT * FROM User WHERE Username = '{srcUsers.Text}'");
            int getCount = cmd.ExecuteQuery<User>().Count;
            if (getCount == 0)
            {
                // NO USER
                cmd.CommandText = ($"SELECT * FROM User WHERE Username LIKE '%{srcUsers.Text}%'");
                getCount = cmd.ExecuteQuery<User>().Count;
                if (getCount > 0)
                {
                    cmd.CommandText = ($"SELECT * FROM User WHERE Username LIKE '%{srcUsers.Text}%'");
                    var getData = cmd.ExecuteQuery<User>();
                    await DisplayAlert("Error", "No User was found by that username, but did find at least one similar user: " + getData[0].Username, "OK");
                }
                else
                {
                    await DisplayAlert("Error", "No User was found by that username.", "OK");
                }
            }
        }
    }
}