using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;

using System.IO;

using Plugin.SimpleAudioPlayer;

namespace myCapeAvery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        private int UID;
        private string Username;
        private ImageButton btnLike;
        private ImageButton btnDislike;
        private ImageButton btnDelete;
        
        public NewsPage(string guest)
        {
            InitializeComponent();

            btnPost.IsEnabled = false;
            txtPost.IsEnabled = false;

            SQLiteConnection conn = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "MySQLite.db3"));
            SQLiteCommand cmd = new SQLiteCommand(conn);

            // Clears the Wall for Post 'Refreshing'
            Wall.Children.Clear();

            // Get all Posts (not deleted), sorted newest to oldest.
            cmd.CommandText = "SELECT * FROM Post";
            var posts = cmd.ExecuteQuery<Post>();
            posts.Reverse();

            // Main loop to go through each Post.
            foreach (Post post in posts)
            {
                // Find the Username of the User of each Post, for later manipulation.
                cmd.CommandText = ($"SELECT * FROM User WHERE UID = '{post.UID}'");
                var getData = cmd.ExecuteQuery<User>();
                var postedBy = getData[0].Username;

                // Dynamically add each Post to the Wall.
                var lblBody = new Label() { Text = "" + post.Body + "" };
                var stack = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0, 0, 0, 10) };
                var lblPost = new Label() { Text = "Posted by: " + postedBy + " on " + post.PostDate.ToString("MM/dd/yy"), Margin = new Thickness(0, 0, 25, 0), FontAttributes = FontAttributes.Bold };
                btnLike = new ImageButton();
                btnLike.Source = "Like_null.png";
                btnLike.WidthRequest = 20;
                btnLike.BackgroundColor = Color.Transparent;
                btnLike.IsEnabled = false;
                btnLike.Clicked += async delegate { await onLike(post.PID); };
                btnDislike = new ImageButton();
                btnDislike.Source = "Dislike_null.png";
                btnDislike.WidthRequest = 20;
                btnDislike.BackgroundColor = Color.Transparent;
                btnDislike.IsEnabled = false;
                btnDislike.Clicked += async delegate { await onDislike(post.PID); };
                btnDelete = new ImageButton();
                btnDelete.Source = "Delete.png";
                btnDelete.WidthRequest = 20;
                btnDelete.BackgroundColor = Color.Transparent;
                btnDelete.IsEnabled = false;
                btnDelete.Clicked += async delegate { await onDelete(post.PID); };
                Wall.Children.Add(lblBody);
                stack.Children.Add(lblPost);
                stack.Children.Add(btnLike);
                stack.Children.Add(btnDislike);
                stack.Children.Add(btnDelete);
                Wall.Children.Add(stack);
            }
        }
        
        public NewsPage(int uID)
        {
            InitializeComponent();

            SQLiteConnection conn = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "MySQLite.db3"));
            SQLiteCommand cmd = new SQLiteCommand(conn);

            // Assign uID to UID variable (for current Users' posting later/navigation), then get current Users' Username.
            UID = uID;
            cmd.CommandText = ($"SELECT * FROM User WHERE UID = '{UID}'");
            var usr = cmd.ExecuteQuery<User>();
            Username = usr[0].Username;

            // Clears the Wall for Post 'Refreshing'
            Wall.Children.Clear();

            // Get all Posts (not deleted), sorted newest to oldest.
            cmd.CommandText = "SELECT * FROM Post";
            var posts = cmd.ExecuteQuery<Post>();
            posts.Reverse();

            // Main loop to go through each Post.
            foreach (Post post in posts)
            {
                // Find the Username of the User of each Post, for later manipulation.
                cmd.CommandText = ($"SELECT * FROM User WHERE UID = '{post.UID}'");
                var getData = cmd.ExecuteQuery<User>();
                var postedBy = getData[0].Username;

                // Get any Posts that are Liked or Disliked and linked by UID, for later manipulation.
                cmd.CommandText = ($"SELECT * FROM IsLiked WHERE UID = '{UID}' AND PID = '{post.PID}'");
                var likes = cmd.ExecuteQuery<IsLiked>();

                if (likes.Count == 0) // NO LIKE/DISLIKE DATA FOR THIS POST
                {
                    // Dynamically add each Post to the Wall.
                    var lblBody = new Label() { Text = "" + post.Body + "" };
                    var stack = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0, 0, 0, 10) };
                    var lblPost = new Label() { Text = "Posted by: " + postedBy + " on " + post.PostDate.ToString("MM/dd/yy"), Margin = new Thickness(0, 0, 25, 0), FontAttributes = FontAttributes.Bold };
                    btnLike = new ImageButton();
                    btnLike.Source = "Like_null.png";
                    btnLike.WidthRequest = 20;
                    btnLike.BackgroundColor = Color.Transparent;
                    btnLike.Clicked += async delegate { await onLike(post.PID); };
                    btnDislike = new ImageButton();
                    btnDislike.Source = "Dislike_null.png";
                    btnDislike.WidthRequest = 20;
                    btnDislike.BackgroundColor = Color.Transparent;
                    btnDislike.Clicked += async delegate { await onDislike(post.PID); };
                    btnDelete = new ImageButton();
                    btnDelete.Source = "Delete.png";
                    btnDelete.WidthRequest = 20;
                    btnDelete.BackgroundColor = Color.Transparent;
                    btnDelete.Clicked += async delegate { await onDelete(post.PID); };
                    Wall.Children.Add(lblBody);
                    stack.Children.Add(lblPost);
                    stack.Children.Add(btnLike);
                    stack.Children.Add(btnDislike);
                    stack.Children.Add(btnDelete);
                    Wall.Children.Add(stack);
                }
                else
                {
                    if (likes[0].Liked == true) // USER HAD PREVIOUSLY LIKED THIS POST
                    {
                        // Dynamically add each Post to the Wall.
                        var lblBody = new Label() { Text = "" + post.Body + "" };
                        var stack = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0, 0, 0, 10) };
                        var lblPost = new Label() { Text = "Posted by: " + postedBy + " on " + post.PostDate.ToString("MM/dd/yy"), Margin = new Thickness(0, 0, 25, 0), FontAttributes = FontAttributes.Bold };
                        btnLike = new ImageButton();
                        btnLike.Source = "Like.png";
                        btnLike.WidthRequest = 20;
                        btnLike.BackgroundColor = Color.Transparent;
                        btnLike.Clicked += async delegate { await onLike(post.PID); };
                        btnDislike = new ImageButton();
                        btnDislike.Source = "Dislike_null.png";
                        btnDislike.WidthRequest = 20;
                        btnDislike.BackgroundColor = Color.Transparent;
                        btnDislike.Clicked += async delegate { await onDislike(post.PID); };
                        btnDelete = new ImageButton();
                        btnDelete.Source = "Delete.png";
                        btnDelete.WidthRequest = 20;
                        btnDelete.BackgroundColor = Color.Transparent;
                        btnDelete.Clicked += async delegate { await onDelete(post.PID); };
                        Wall.Children.Add(lblBody);
                        stack.Children.Add(lblPost);
                        stack.Children.Add(btnLike);
                        stack.Children.Add(btnDislike);
                        stack.Children.Add(btnDelete);
                        Wall.Children.Add(stack);
                    }
                    else // USER HAD PREVIOUSLY DISLIKED THIS POST
                    {
                        // Dynamically add each Post to the Wall.
                        var lblBody = new Label() { Text = "" + post.Body + "" };
                        var stack = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0, 0, 0, 10) };
                        var lblPost = new Label() { Text = "Posted by: " + postedBy + " on " + post.PostDate.ToString("MM/dd/yy"), Margin = new Thickness(0, 0, 25, 0), FontAttributes = FontAttributes.Bold };
                        btnLike = new ImageButton();
                        btnLike.Source = "Like_null.png";
                        btnLike.WidthRequest = 20;
                        btnLike.BackgroundColor = Color.Transparent;
                        btnLike.Clicked += async delegate { await onLike(post.PID); };
                        btnDislike = new ImageButton();
                        btnDislike.Source = "Dislike.png";
                        btnDislike.WidthRequest = 20;
                        btnDislike.BackgroundColor = Color.Transparent;
                        btnDislike.Clicked += async delegate { await onDislike(post.PID); };
                        btnDelete = new ImageButton();
                        btnDelete.Source = "Delete.png";
                        btnDelete.WidthRequest = 20;
                        btnDelete.BackgroundColor = Color.Transparent;
                        btnDelete.Clicked += async delegate { await onDelete(post.PID); };
                        Wall.Children.Add(lblBody);
                        stack.Children.Add(lblPost);
                        stack.Children.Add(btnLike);
                        stack.Children.Add(btnDislike);
                        stack.Children.Add(btnDelete);
                        Wall.Children.Add(stack);
                    }
                }
            }
        }

        private async void onPost(object sender, EventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "MySQLite.db3"));
            SQLiteCommand cmd = new SQLiteCommand(conn);

            if (txtPost.Text == "")
            {
                await DisplayAlert("Invalid", "Post cannot be blank. Try again!", "OK");
            }
            else
            {
                Post newPost = new Post();
                newPost.UID = UID;
                newPost.Body = txtPost.Text;
                newPost.PostDate = DateTime.Today.ToUniversalTime();
                conn.Insert(newPost);

                // Clears the Wall for Post 'Refreshing'
                Wall.Children.Clear();

                // Get all Posts (not deleted), sorted newest to oldest.
                cmd.CommandText = "SELECT * FROM Post";
                var posts = cmd.ExecuteQuery<Post>();
                posts.Reverse();

                // Main loop to go through each Post.
                foreach (Post post in posts)
                {
                    // Find the Username of the User of each Post, for later manipulation.
                    cmd.CommandText = ($"SELECT * FROM User WHERE UID = '{post.UID}'");
                    var getData = cmd.ExecuteQuery<User>();
                    var postedBy = getData[0].Username;

                    // Get any Posts that are Liked or Disliked and linked by UID, for later manipulation.
                    cmd.CommandText = ($"SELECT * FROM IsLiked WHERE UID = '{UID}' AND PID = '{post.PID}'");
                    var likes = cmd.ExecuteQuery<IsLiked>();

                    if (likes.Count == 0) // NO LIKE/DISLIKE DATA FOR THIS POST
                    {
                        // Dynamically add each Post to the Wall.
                        var lblBody = new Label() { Text = "" + post.Body + "" };
                        var stack = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0, 0, 0, 10) };
                        var lblPost = new Label() { Text = "Posted by: " + postedBy + " on " + post.PostDate.ToString("MM/dd/yy"), Margin = new Thickness(0, 0, 25, 0), FontAttributes = FontAttributes.Bold };
                        btnLike = new ImageButton();
                        btnLike.Source = "Like_null.png";
                        btnLike.WidthRequest = 20;
                        btnLike.BackgroundColor = Color.Transparent;
                        btnLike.Clicked += async delegate { await onLike(post.PID); };
                        btnDislike = new ImageButton();
                        btnDislike.Source = "Dislike_null.png";
                        btnDislike.WidthRequest = 20;
                        btnDislike.BackgroundColor = Color.Transparent;
                        btnDislike.Clicked += async delegate { await onDislike(post.PID); };
                        btnDelete = new ImageButton();
                        btnDelete.Source = "Delete.png";
                        btnDelete.WidthRequest = 20;
                        btnDelete.BackgroundColor = Color.Transparent;
                        btnDelete.Clicked += async delegate { await onDelete(post.PID); };
                        Wall.Children.Add(lblBody);
                        stack.Children.Add(lblPost);
                        stack.Children.Add(btnLike);
                        stack.Children.Add(btnDislike);
                        stack.Children.Add(btnDelete);
                        Wall.Children.Add(stack);
                    }
                    else
                    {
                        if (likes[0].Liked == true) // USER HAD PREVIOUSLY LIKED THIS POST
                        {
                            // Dynamically add each Post to the Wall.
                            var lblBody = new Label() { Text = "" + post.Body + "" };
                            var stack = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0, 0, 0, 10) };
                            var lblPost = new Label() { Text = "Posted by: " + postedBy + " on " + post.PostDate.ToString("MM/dd/yy"), Margin = new Thickness(0, 0, 25, 0), FontAttributes = FontAttributes.Bold };
                            btnLike = new ImageButton();
                            btnLike.Source = "Like.png";
                            btnLike.WidthRequest = 20;
                            btnLike.BackgroundColor = Color.Transparent;
                            btnLike.Clicked += async delegate { await onLike(post.PID); };
                            btnDislike = new ImageButton();
                            btnDislike.Source = "Dislike_null.png";
                            btnDislike.WidthRequest = 20;
                            btnDislike.BackgroundColor = Color.Transparent;
                            btnDislike.Clicked += async delegate { await onDislike(post.PID); };
                            btnDelete = new ImageButton();
                            btnDelete.Source = "Delete.png";
                            btnDelete.WidthRequest = 20;
                            btnDelete.BackgroundColor = Color.Transparent;
                            btnDelete.Clicked += async delegate { await onDelete(post.PID); };
                            Wall.Children.Add(lblBody);
                            stack.Children.Add(lblPost);
                            stack.Children.Add(btnLike);
                            stack.Children.Add(btnDislike);
                            stack.Children.Add(btnDelete);
                            Wall.Children.Add(stack);
                        }
                        else // USER HAD PREVIOUSLY DISLIKED THIS POST
                        {
                            // Dynamically add each Post to the Wall.
                            var lblBody = new Label() { Text = "" + post.Body + "" };
                            var stack = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0, 0, 0, 10) };
                            var lblPost = new Label() { Text = "Posted by: " + postedBy + " on " + post.PostDate.ToString("MM/dd/yy"), Margin = new Thickness(0, 0, 25, 0), FontAttributes = FontAttributes.Bold };
                            btnLike = new ImageButton();
                            btnLike.Source = "Like_null.png";
                            btnLike.WidthRequest = 20;
                            btnLike.BackgroundColor = Color.Transparent;
                            btnLike.Clicked += async delegate { await onLike(post.PID); };
                            btnDislike = new ImageButton();
                            btnDislike.Source = "Dislike.png";
                            btnDislike.WidthRequest = 20;
                            btnDislike.BackgroundColor = Color.Transparent;
                            btnDislike.Clicked += async delegate { await onDislike(post.PID); };
                            btnDelete = new ImageButton();
                            btnDelete.Source = "Delete.png";
                            btnDelete.WidthRequest = 20;
                            btnDelete.BackgroundColor = Color.Transparent;
                            btnDelete.Clicked += async delegate { await onDelete(post.PID); };
                            Wall.Children.Add(lblBody);
                            stack.Children.Add(lblPost);
                            stack.Children.Add(btnLike);
                            stack.Children.Add(btnDislike);
                            stack.Children.Add(btnDelete);
                            Wall.Children.Add(stack);
                        }
                    }
                }

                txtPost.Text = "";

                // Play chime
                await Tune("send");
            }
        }

        private async Task onLike(int PID)
        {
            SQLiteConnection conn = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "MySQLite.db3"));
            SQLiteCommand cmd = new SQLiteCommand(conn);

            // 1. Check if an entry for this Post is in the helper IsLiked table; if it is UPDATE, if not INSERT.
            cmd.CommandText = ($"SELECT * FROM IsLiked WHERE PID = '{PID}' AND UID = '{UID}'");
            var getData = cmd.ExecuteQuery<IsLiked>();
            if (getData.Count == 0) // 2. New Entry = INSERT
            {
                IsLiked newEntry = new IsLiked();
                newEntry.PID = PID;
                newEntry.UID = UID;
                newEntry.Liked = true;
                newEntry.Disliked = false;
                conn.Insert(newEntry);
            }
            else // 2. Existing Entry = UPDATE
            {
                IsLiked updateEntry = new IsLiked();
                updateEntry.IID = getData[0].IID;
                updateEntry.PID = PID;
                updateEntry.UID = UID;
                updateEntry.Liked = true;
                updateEntry.Disliked = false;
                conn.Update(updateEntry);
            }

            // 3. Update Post Likes Count
            cmd.CommandText = ($"SELECT * FROM Post WHERE PID = '{PID}'");
            var getLikes = cmd.ExecuteQuery<Post>();

            Post updatePost = new Post();
            updatePost.PID = PID;
            updatePost.UID = getLikes[0].UID;
            updatePost.Body = getLikes[0].Body;
            updatePost.PostDate = getLikes[0].PostDate;
            updatePost.NumLikes = getLikes[0].NumLikes + 1;
            updatePost.NumDislikes = getLikes[0].NumDislikes;
            conn.Update(updatePost);

            // Clears the Wall for Post 'Refreshing'
            Wall.Children.Clear();

            // Get all Posts (not deleted), sorted newest to oldest.
            cmd.CommandText = "SELECT * FROM Post";
            var posts = cmd.ExecuteQuery<Post>();
            posts.Reverse();

            // Main loop to go through each Post.
            foreach (Post post in posts)
            {
                // Find the Username of the User of each Post, for later manipulation.
                cmd.CommandText = ($"SELECT * FROM User WHERE UID = '{post.UID}'");
                var getLike = cmd.ExecuteQuery<User>();
                var postedBy = getLike[0].Username;

                // Get any Posts that are Liked or Disliked and linked by UID, for later manipulation.
                cmd.CommandText = ($"SELECT * FROM IsLiked WHERE UID = '{UID}' AND PID = '{post.PID}'");
                var likes = cmd.ExecuteQuery<IsLiked>();

                if (likes.Count == 0) // NO LIKE/DISLIKE DATA FOR THIS POST
                {
                    // Dynamically add each Post to the Wall.
                    var lblBody = new Label() { Text = "" + post.Body + "" };
                    var stack = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0, 0, 0, 10) };
                    var lblPost = new Label() { Text = "Posted by: " + postedBy + " on " + post.PostDate.ToString("MM/dd/yy"), Margin = new Thickness(0, 0, 25, 0), FontAttributes = FontAttributes.Bold };
                    btnLike = new ImageButton();
                    btnLike.Source = "Like_null.png";
                    btnLike.WidthRequest = 20;
                    btnLike.BackgroundColor = Color.Transparent;
                    btnLike.Clicked += async delegate { await onLike(post.PID); };
                    btnDislike = new ImageButton();
                    btnDislike.Source = "Dislike_null.png";
                    btnDislike.WidthRequest = 20;
                    btnDislike.BackgroundColor = Color.Transparent;
                    btnDislike.Clicked += async delegate { await onDislike(post.PID); };
                    btnDelete = new ImageButton();
                    btnDelete.Source = "Delete.png";
                    btnDelete.WidthRequest = 20;
                    btnDelete.BackgroundColor = Color.Transparent;
                    btnDelete.Clicked += async delegate { await onDelete(post.PID); };
                    Wall.Children.Add(lblBody);
                    stack.Children.Add(lblPost);
                    stack.Children.Add(btnLike);
                    stack.Children.Add(btnDislike);
                    stack.Children.Add(btnDelete);
                    Wall.Children.Add(stack);
                }
                else
                {
                    if (likes[0].Liked == true) // USER HAD PREVIOUSLY LIKED THIS POST
                    {
                        // Dynamically add each Post to the Wall.
                        var lblBody = new Label() { Text = "" + post.Body + "" };
                        var stack = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0, 0, 0, 10) };
                        var lblPost = new Label() { Text = "Posted by: " + postedBy + " on " + post.PostDate.ToString("MM/dd/yy"), Margin = new Thickness(0, 0, 25, 0), FontAttributes = FontAttributes.Bold };
                        btnLike = new ImageButton();
                        btnLike.Source = "Like.png";
                        btnLike.WidthRequest = 20;
                        btnLike.BackgroundColor = Color.Transparent;
                        btnLike.Clicked += async delegate { await onLike(post.PID); };
                        btnDislike = new ImageButton();
                        btnDislike.Source = "Dislike_null.png";
                        btnDislike.WidthRequest = 20;
                        btnDislike.BackgroundColor = Color.Transparent;
                        btnDislike.Clicked += async delegate { await onDislike(post.PID); };
                        btnDelete = new ImageButton();
                        btnDelete.Source = "Delete.png";
                        btnDelete.WidthRequest = 20;
                        btnDelete.BackgroundColor = Color.Transparent;
                        btnDelete.Clicked += async delegate { await onDelete(post.PID); };
                        Wall.Children.Add(lblBody);
                        stack.Children.Add(lblPost);
                        stack.Children.Add(btnLike);
                        stack.Children.Add(btnDislike);
                        stack.Children.Add(btnDelete);
                        Wall.Children.Add(stack);
                    }
                    else // USER HAD PREVIOUSLY DISLIKED THIS POST
                    {
                        // Dynamically add each Post to the Wall.
                        var lblBody = new Label() { Text = "" + post.Body + "" };
                        var stack = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0, 0, 0, 10) };
                        var lblPost = new Label() { Text = "Posted by: " + postedBy + " on " + post.PostDate.ToString("MM/dd/yy"), Margin = new Thickness(0, 0, 25, 0), FontAttributes = FontAttributes.Bold };
                        btnLike = new ImageButton();
                        btnLike.Source = "Like_null.png";
                        btnLike.WidthRequest = 20;
                        btnLike.BackgroundColor = Color.Transparent;
                        btnLike.Clicked += async delegate { await onLike(post.PID); };
                        btnDislike = new ImageButton();
                        btnDislike.Source = "Dislike.png";
                        btnDislike.WidthRequest = 20;
                        btnDislike.BackgroundColor = Color.Transparent;
                        btnDislike.Clicked += async delegate { await onDislike(post.PID); };
                        btnDelete = new ImageButton();
                        btnDelete.Source = "Delete.png";
                        btnDelete.WidthRequest = 20;
                        btnDelete.BackgroundColor = Color.Transparent;
                        btnDelete.Clicked += async delegate { await onDelete(post.PID); };
                        Wall.Children.Add(lblBody);
                        stack.Children.Add(lblPost);
                        stack.Children.Add(btnLike);
                        stack.Children.Add(btnDislike);
                        stack.Children.Add(btnDelete);
                        Wall.Children.Add(stack);
                    }
                }
            }

            // Play chime
            await Tune("pop");
        }
        private async Task onDislike(int PID)
        {
            SQLiteConnection conn = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "MySQLite.db3"));
            SQLiteCommand cmd = new SQLiteCommand(conn);

            // 1. Check if an entry for this Post is in the helper IsLiked table; if it is UPDATE, if not INSERT.
            cmd.CommandText = ($"SELECT * FROM IsLiked WHERE PID = '{PID}' AND UID = '{UID}'");
            var getData = cmd.ExecuteQuery<IsLiked>();
            if (getData.Count == 0) // 2. New Entry = INSERT
            {
                IsLiked newEntry = new IsLiked();
                newEntry.PID = PID;
                newEntry.UID = UID;
                newEntry.Liked = false;
                newEntry.Disliked = true;
                conn.Insert(newEntry);
            }
            else // 2. Existing Entry = UPDATE
            {
                IsLiked updateEntry = new IsLiked();
                updateEntry.IID = getData[0].IID;
                updateEntry.PID = PID;
                updateEntry.UID = UID;
                updateEntry.Liked = false;
                updateEntry.Disliked = true;
                conn.Update(updateEntry);
            }

            // 3. Update Post Dislikes Count
            cmd.CommandText = ($"SELECT * FROM Post WHERE PID = '{PID}'");
            var getDislikes = cmd.ExecuteQuery<Post>();

            Post updatePost = new Post();
            updatePost.PID = PID;
            updatePost.UID = getDislikes[0].UID;
            updatePost.Body = getDislikes[0].Body;
            updatePost.PostDate = getDislikes[0].PostDate;
            updatePost.NumLikes = getDislikes[0].NumLikes;
            updatePost.NumDislikes = getDislikes[0].NumDislikes + 1;
            conn.Update(updatePost);

            // Clears the Wall for Post 'Refreshing'
            Wall.Children.Clear();

            // Get all Posts (not deleted), sorted newest to oldest.
            cmd.CommandText = "SELECT * FROM Post";
            var posts = cmd.ExecuteQuery<Post>();
            posts.Reverse();

            // Main loop to go through each Post.
            foreach (Post post in posts)
            {
                // Find the Username of the User of each Post, for later manipulation.
                cmd.CommandText = ($"SELECT * FROM User WHERE UID = '{post.UID}'");
                var getLike = cmd.ExecuteQuery<User>();
                var postedBy = getLike[0].Username;

                // Get any Posts that are Liked or Disliked and linked by UID, for later manipulation.
                cmd.CommandText = ($"SELECT * FROM IsLiked WHERE UID = '{UID}' AND PID = '{post.PID}'");
                var likes = cmd.ExecuteQuery<IsLiked>();

                if (likes.Count == 0) // NO LIKE/DISLIKE DATA FOR THIS POST
                {
                    // Dynamically add each Post to the Wall.
                    var lblBody = new Label() { Text = "" + post.Body + "" };
                    var stack = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0, 0, 0, 10) };
                    var lblPost = new Label() { Text = "Posted by: " + postedBy + " on " + post.PostDate.ToString("MM/dd/yy"), Margin = new Thickness(0, 0, 25, 0), FontAttributes = FontAttributes.Bold };
                    btnLike = new ImageButton();
                    btnLike.Source = "Like_null.png";
                    btnLike.WidthRequest = 20;
                    btnLike.BackgroundColor = Color.Transparent;
                    btnLike.Clicked += async delegate { await onLike(post.PID); };
                    btnDislike = new ImageButton();
                    btnDislike.Source = "Dislike_null.png";
                    btnDislike.WidthRequest = 20;
                    btnDislike.BackgroundColor = Color.Transparent;
                    btnDislike.Clicked += async delegate { await onDislike(post.PID); };
                    btnDelete = new ImageButton();
                    btnDelete.Source = "Delete.png";
                    btnDelete.WidthRequest = 20;
                    btnDelete.BackgroundColor = Color.Transparent;
                    btnDelete.Clicked += async delegate { await onDelete(post.PID); };
                    Wall.Children.Add(lblBody);
                    stack.Children.Add(lblPost);
                    stack.Children.Add(btnLike);
                    stack.Children.Add(btnDislike);
                    stack.Children.Add(btnDelete);
                    Wall.Children.Add(stack);
                }
                else
                {
                    if (likes[0].Liked == true) // USER HAD PREVIOUSLY LIKED THIS POST
                    {
                        // Dynamically add each Post to the Wall.
                        var lblBody = new Label() { Text = "" + post.Body + "" };
                        var stack = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0, 0, 0, 10) };
                        var lblPost = new Label() { Text = "Posted by: " + postedBy + " on " + post.PostDate.ToString("MM/dd/yy"), Margin = new Thickness(0, 0, 25, 0), FontAttributes = FontAttributes.Bold };
                        btnLike = new ImageButton();
                        btnLike.Source = "Like.png";
                        btnLike.WidthRequest = 20;
                        btnLike.BackgroundColor = Color.Transparent;
                        btnLike.Clicked += async delegate { await onLike(post.PID); };
                        btnDislike = new ImageButton();
                        btnDislike.Source = "Dislike_null.png";
                        btnDislike.WidthRequest = 20;
                        btnDislike.BackgroundColor = Color.Transparent;
                        btnDislike.Clicked += async delegate { await onDislike(post.PID); };
                        btnDelete = new ImageButton();
                        btnDelete.Source = "Delete.png";
                        btnDelete.WidthRequest = 20;
                        btnDelete.BackgroundColor = Color.Transparent;
                        btnDelete.Clicked += async delegate { await onDelete(post.PID); };
                        Wall.Children.Add(lblBody);
                        stack.Children.Add(lblPost);
                        stack.Children.Add(btnLike);
                        stack.Children.Add(btnDislike);
                        stack.Children.Add(btnDelete);
                        Wall.Children.Add(stack);
                    }
                    else // USER HAD PREVIOUSLY DISLIKED THIS POST
                    {
                        // Dynamically add each Post to the Wall.
                        var lblBody = new Label() { Text = "" + post.Body + "" };
                        var stack = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0, 0, 0, 10) };
                        var lblPost = new Label() { Text = "Posted by: " + postedBy + " on " + post.PostDate.ToString("MM/dd/yy"), Margin = new Thickness(0, 0, 25, 0), FontAttributes = FontAttributes.Bold };
                        btnLike = new ImageButton();
                        btnLike.Source = "Like_null.png";
                        btnLike.WidthRequest = 20;
                        btnLike.BackgroundColor = Color.Transparent;
                        btnLike.Clicked += async delegate { await onLike(post.PID); };
                        btnDislike = new ImageButton();
                        btnDislike.Source = "Dislike.png";
                        btnDislike.WidthRequest = 20;
                        btnDislike.BackgroundColor = Color.Transparent;
                        btnDislike.Clicked += async delegate { await onDislike(post.PID); };
                        btnDelete = new ImageButton();
                        btnDelete.Source = "Delete.png";
                        btnDelete.WidthRequest = 20;
                        btnDelete.BackgroundColor = Color.Transparent;
                        btnDelete.Clicked += async delegate { await onDelete(post.PID); };
                        Wall.Children.Add(lblBody);
                        stack.Children.Add(lblPost);
                        stack.Children.Add(btnLike);
                        stack.Children.Add(btnDislike);
                        stack.Children.Add(btnDelete);
                        Wall.Children.Add(stack);
                    }
                }
            }

            // Play chime
            await Tune("pop");
        }
        private async Task onDelete(int PID)
        {
            SQLiteConnection conn = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "MySQLite.db3"));
            SQLiteCommand cmd = new SQLiteCommand(conn);

            // Get current Users' role type
            cmd.CommandText = ($"SELECT * FROM User WHERE UID = '{UID}'");
            var getRole = cmd.ExecuteQuery<User>();
            var role = getRole[0].Role;

            // If 'Official', can delete Post; Otherwise, display 'Unauthorized'.
            if (role == "Official")
            {
                var choice = await DisplayAlert("Confirm", "Are you sure that you would like to delete this Post?", "Delete", "Cancel");
                if (choice)
                {
                    Post deletePost = new Post();
                    deletePost.PID = PID;
                    conn.Delete(deletePost);

                    // Get IsLiked IID (Primary Key).
                    cmd.CommandText = ($"SELECT * FROM IsLiked WHERE UID = '{UID}' AND PID = '{PID}'");
                    var getLike = cmd.ExecuteQuery<IsLiked>();

                    if (getLike.Count == 0)
                    {
                        // Do Nothing
                    }
                    else
                    {
                        IsLiked deleteIs = new IsLiked();
                        deleteIs.IID = getLike[0].IID;
                        conn.Delete(deleteIs);
                    }

                    // Clears the Wall for Post 'Refreshing'
                    Wall.Children.Clear();

                    // Get all Posts (not deleted), sorted newest to oldest.
                    cmd.CommandText = "SELECT * FROM Post";
                    var posts = cmd.ExecuteQuery<Post>();
                    posts.Reverse();

                    // Main loop to go through each Post.
                    foreach (Post post in posts)
                    {
                        // Find the Username of the User of each Post, for later manipulation.
                        cmd.CommandText = ($"SELECT * FROM User WHERE UID = '{post.UID}'");
                        var getData = cmd.ExecuteQuery<User>();
                        var postedBy = getData[0].Username;

                        // Get any Posts that are Liked or Disliked and linked by UID, for later manipulation.
                        cmd.CommandText = ($"SELECT * FROM IsLiked WHERE UID = '{UID}' AND PID = '{post.PID}'");
                        var likes = cmd.ExecuteQuery<IsLiked>();

                        if (likes.Count == 0) // NO LIKE/DISLIKE DATA FOR THIS POST
                        {
                            // Dynamically add each Post to the Wall.
                            var lblBody = new Label() { Text = "" + post.Body + "" };
                            var stack = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0, 0, 0, 10) };
                            var lblPost = new Label() { Text = "Posted by: " + postedBy + " on " + post.PostDate.ToString("MM/dd/yy"), Margin = new Thickness(0, 0, 25, 0), FontAttributes = FontAttributes.Bold };
                            btnLike = new ImageButton();
                            btnLike.Source = "Like_null.png";
                            btnLike.WidthRequest = 20;
                            btnLike.BackgroundColor = Color.Transparent;
                            btnLike.Clicked += async delegate { await onLike(post.PID); };
                            btnDislike = new ImageButton();
                            btnDislike.Source = "Dislike_null.png";
                            btnDislike.WidthRequest = 20;
                            btnDislike.BackgroundColor = Color.Transparent;
                            btnDislike.Clicked += async delegate { await onDislike(post.PID); };
                            btnDelete = new ImageButton();
                            btnDelete.Source = "Delete.png";
                            btnDelete.WidthRequest = 20;
                            btnDelete.BackgroundColor = Color.Transparent;
                            btnDelete.Clicked += async delegate { await onDelete(post.PID); };
                            Wall.Children.Add(lblBody);
                            stack.Children.Add(lblPost);
                            stack.Children.Add(btnLike);
                            stack.Children.Add(btnDislike);
                            stack.Children.Add(btnDelete);
                            Wall.Children.Add(stack);
                        }
                        else
                        {
                            if (likes[0].Liked == true) // USER HAD PREVIOUSLY LIKED THIS POST
                            {
                                // Dynamically add each Post to the Wall.
                                var lblBody = new Label() { Text = "" + post.Body + "" };
                                var stack = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0, 0, 0, 10) };
                                var lblPost = new Label() { Text = "Posted by: " + postedBy + " on " + post.PostDate.ToString("MM/dd/yy"), Margin = new Thickness(0, 0, 25, 0), FontAttributes = FontAttributes.Bold };
                                btnLike = new ImageButton();
                                btnLike.Source = "Like.png";
                                btnLike.WidthRequest = 20;
                                btnLike.BackgroundColor = Color.Transparent;
                                btnLike.Clicked += async delegate { await onLike(post.PID); };
                                btnDislike = new ImageButton();
                                btnDislike.Source = "Dislike_null.png";
                                btnDislike.WidthRequest = 20;
                                btnDislike.BackgroundColor = Color.Transparent;
                                btnDislike.Clicked += async delegate { await onDislike(post.PID); };
                                btnDelete = new ImageButton();
                                btnDelete.Source = "Delete.png";
                                btnDelete.WidthRequest = 20;
                                btnDelete.BackgroundColor = Color.Transparent;
                                btnDelete.Clicked += async delegate { await onDelete(post.PID); };
                                Wall.Children.Add(lblBody);
                                stack.Children.Add(lblPost);
                                stack.Children.Add(btnLike);
                                stack.Children.Add(btnDislike);
                                stack.Children.Add(btnDelete);
                                Wall.Children.Add(stack);
                            }
                            else // USER HAD PREVIOUSLY DISLIKED THIS POST
                            {
                                // Dynamically add each Post to the Wall.
                                var lblBody = new Label() { Text = "" + post.Body + "" };
                                var stack = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0, 0, 0, 10) };
                                var lblPost = new Label() { Text = "Posted by: " + postedBy + " on " + post.PostDate.ToString("MM/dd/yy"), Margin = new Thickness(0, 0, 25, 0), FontAttributes = FontAttributes.Bold };
                                btnLike = new ImageButton();
                                btnLike.Source = "Like_null.png";
                                btnLike.WidthRequest = 20;
                                btnLike.BackgroundColor = Color.Transparent;
                                btnLike.Clicked += async delegate { await onLike(post.PID); };
                                btnDislike = new ImageButton();
                                btnDislike.Source = "Dislike.png";
                                btnDislike.WidthRequest = 20;
                                btnDislike.BackgroundColor = Color.Transparent;
                                btnDislike.Clicked += async delegate { await onDislike(post.PID); };
                                btnDelete = new ImageButton();
                                btnDelete.Source = "Delete.png";
                                btnDelete.WidthRequest = 20;
                                btnDelete.BackgroundColor = Color.Transparent;
                                btnDelete.Clicked += async delegate { await onDelete(post.PID); };
                                Wall.Children.Add(lblBody);
                                stack.Children.Add(lblPost);
                                stack.Children.Add(btnLike);
                                stack.Children.Add(btnDislike);
                                stack.Children.Add(btnDelete);
                                Wall.Children.Add(stack);
                            }
                        }
                    }
                }
                // Play chime
                await Tune("delete");
            }
            else
            {
                await DisplayAlert("Unauthorized", "Only Officials may delete community Posts. If you would like to report this Post, please contact the Mayor's office.", "OK");
            }
        }

        private async Task Tune(string name)
        {
            if (name == "pop")
            {
                // Play chime
                var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                player.Load("pop.mp3");
                player.Play();
            }
            else if (name == "send")
            {
                // Play chime
                var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                player.Load("send.mp3");
                player.Play();
            }
            else if (name == "delete")
            {
                // Play chime
                var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                player.Load("delete.mp3");
                player.Play();
            }
        }
    }
}
 