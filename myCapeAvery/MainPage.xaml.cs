using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using SQLite;

using System.IO;

namespace myCapeAvery
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    [Table("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Answer { get; set; }
        public string Role { get; set; }
    }

    [Table("Post")]
    public class Post
    {
        [PrimaryKey, AutoIncrement]
        public int PID { get; set; }
        public int UID { get; set; }
        public string Body { get; set; }
        public DateTime PostDate { get; set; }
        public int NumLikes { get; set; }
        public int NumDislikes { get; set; }
    }

    [Table("Advertisement")]
    public class Advertisement
    { 
        [PrimaryKey, AutoIncrement]
        public int AID { get; set; }
        public string Body { get; set; }
        public DateTime EndDate { get; set; }
        public int Length { get; set; }
    }

    [Table("IsLiked")]
    public class IsLiked
    {
        [PrimaryKey, AutoIncrement]
        public int IID { get; set; }
        public int UID { get; set; }
        public int PID { get; set; }
        public bool Liked { get; set; }
        public bool Disliked { get; set; }
    }

    public partial class MainPage : ContentPage
    {
        private SQLiteAsyncConnection conn = new SQLiteAsyncConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "MySQLite.db3"));

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            
            // Create Tables (from above)
            await conn.CreateTableAsync<User>();
            await conn.CreateTableAsync<Post>();
            await conn.CreateTableAsync<Advertisement>();
            await conn.CreateTableAsync<IsLiked>();
            

            // Seed data if there are no Users in the User Table
            var userList = await conn.Table<User>().ToListAsync();
            if (!userList.Any())
            {
                // Seed 'Admin' User
                User newUser = new User();
                newUser.Username = "admin";
                newUser.Password = "admin";
                newUser.Answer = "admin";
                newUser.Role = "Official";

                await conn.InsertAsync(newUser);
            }

            /*
            // USED FOR WIPING THE DATABASE FOR TESTING; UNCOMMENT THIS SECTION AND COMMENT OUT LINES 75-79 TO RUN
            await conn.DropTableAsync<User>();
            await conn.DropTableAsync<Post>();
            await conn.DropTableAsync<Advertisement>();
            await conn.DropTableAsync<IsLiked>();
            await DisplayAlert("DEBUG", "The Database has been reset!", "OK");
            */
        }

        private async void onLogin(object sender, EventArgs e)
        {
            // Login Connection
            SQLiteConnection conn2 = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "MySQLite.db3"));
            SQLiteCommand cmd = new SQLiteCommand(conn2);

            var button = new Button();
            button = (Button)sender;
            if (button.Text == "Login as Guest")
            {
                await Navigation.PushAsync(new HomePage("guest"));
            }
            else
            {
                // 1. Check for blank entry
                if(txtUsername.Text?.ToLower() == "" || txtPassword.Text?.ToLower() == "" || txtUsername.Text?.ToLower() == null || txtPassword.Text?.ToLower() == null)
                {
                    await DisplayAlert("Error", "The Username and/or Password cannot be blank. Try again!", "OK");
                }
                else
                {
                    // 2. Check if Username exists, otherwise display 'not found' and attempt to funnel to registration
                    cmd.CommandText = ($"SELECT COUNT(*) FROM User WHERE Username = '{txtUsername.Text.ToLower()}'");
                    var countUsers = cmd.ExecuteScalar<int>();
                    if (countUsers == 0)
                    {
                        await DisplayAlert("Error", "The Username was not found. Register for a new account!", "OK");
                    }
                    // 3. If Username exists, check combo of Username + Password for successful login attempt. Otherwise, display 'invalid entry'.
                    else if (countUsers == 1)
                    {
                        cmd.CommandText = ($"SELECT COUNT(*) FROM User WHERE Username = '{txtUsername.Text.ToLower()}' AND Password = '{txtPassword.Text.ToLower()}'");
                        countUsers = cmd.ExecuteScalar<int>();
                        if (countUsers == 0)
                        {
                            await DisplayAlert("Error", "The Password is incorrect. Try again!", "OK");
                        }
                        // 4. Successful Login.
                        else if (countUsers == 1)
                        {
                            cmd.CommandText = ($"SELECT * FROM User WHERE Username = '{txtUsername.Text.ToLower()}' AND Password = '{txtPassword.Text.ToLower()}'");
                            var Users = cmd.ExecuteQuery<User>();
                            var UID = Users[0].UID;

                            await Navigation.PushAsync(new HomePage(UID));
                        }
                    }
                }
            }
        }

        private async void onRegister(object sender, EventArgs e)
        {
            // Navigate to Registration
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void onForgot(object sender, EventArgs e)
        {
            // Navigate to Password Recovery
            await Navigation.PushAsync(new ForgotPage());
        }
    }
}
