using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TetrisMUWP.ScoreManager;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TetrisMUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class highScores : Page
    {
        public Dictionary<string, string>[] users = { new Dictionary<string, string>(), new Dictionary<string, string>(), new Dictionary<string, string>(), new Dictionary<string, string>(), new Dictionary<string, string>() };
        public const int NUM_OF_USERS = 5;

        public TextBlock[] name_block = new TextBlock[NUM_OF_USERS];
        public TextBlock[] score_block = new TextBlock[NUM_OF_USERS];
        Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        Windows.Storage.StorageFile scoresFile;

        /// <summary>
        /// testing variables
        /// </summary>
        string testname = "Jorome";
        string testscore = "31100"; //will be used for passed in score

        public highScores()
        {
            this.InitializeComponent();
            name_block[0] = User0;
            name_block[1] = User1;
            name_block[2] = User2;
            name_block[3] = User3;
            name_block[4] = User4;
            score_block[0] = Score0;
            score_block[1] = Score1;
            score_block[2] = Score2;
            score_block[3] = Score3;
            score_block[4] = Score4;
            for (int i = 0; i < NUM_OF_USERS; i++)
            {
                users[i].Add("Ply", "0");
                name_block[i].Text = users[i].Keys.ElementAt(0);//.ToString();
                score_block[i].Text = users[i].Values.ElementAt(0);//.ToString();
            }

            //newplayer(NewName, NewScore); //test
            //compare_score(NewName, NewScore);

            ///testing values for high score list algorithm
            ///takes 3 characters from username
            compare_score("Jorome", "150");
            compare_score("Rigo", "389");
            compare_score("Leo", "390");
            compare_score("Michael", "100");
            compare_score("Tetris God", "200000000");

            save_scores();
            read_scores();
        }

        /*
        public static playerscore player { get; set; } //test
        public playerscore player = new playerscore(); //test

        public string NewName;
        public string NewScore;


       public void newplayer(string name, string points)
        {
            NewName = name;
            NewScore = points; //NewScore;

            compare_score(name, points);
            update_UI();

            save_scores();
            read_scores();
        }*/
        /// <summary>
        /// This method updates each name_block and score_block whenever it is called
        /// </summary>
            private void update_UI()
            {
                for (int i = 0; i < NUM_OF_USERS; i++)
                {
                name_block[i].Text = users[i].Keys.ElementAt(0);
                score_block[i].Text = users[i].Values.ElementAt(0).ToString();
                }
            }

        /// <summary>
        /// This method compares the users score after ending the game with the scores that are
        /// stored in the Json file.
        /// </summary>
        /// scores are string and so they have to be converted to long for comparison
        /// <param name="name"></param>
        /// <param name="score"></param>
        private void compare_score(string name, string score)
        {
            if (Convert.ToUInt64(score) > Convert.ToUInt64(score_block[0].Text))
            {
                int i = 0;
                while (i != 4)
                {
                    //y = x(initial)
                    remove_score(i + 1);
                    add_score(i + 1, name_block[0].Text, score_block[0].Text);
                    //x = z;
                    remove_score(0);
                    add_score(0, name_block[i + 1].Text, score_block[i + 1].Text);

                    i++;
                    update_UI();
                }
                remove_score(0);
                add_score(0, name, score);
                update_UI();
            }

            else if (Convert.ToUInt64(score) > Convert.ToUInt64(score_block[1].Text))
            {
                int i = 1;
                while (i != 4)
                {
                    //y = x
                    remove_score(i + 1);
                    add_score(i + 1, name_block[1].Text, score_block[1].Text);
                    //x = z;
                    remove_score(1);
                    add_score(1, name_block[i + 1].Text, score_block[i + 1].Text);

                    i++;
                    update_UI();
                }
                remove_score(1);
                add_score(1, name, score);
                update_UI();
            }
            else if (Convert.ToUInt64(score) > Convert.ToUInt64(score_block[2].Text))
            {
                int i = 2;
                while (i != 4)
                {
                    //y = x
                    remove_score(i + 1);
                    add_score(i + 1, name_block[2].Text, score_block[2].Text);
                    //x = z;
                    remove_score(2);
                    add_score(2, name_block[i + 1].Text, score_block[i + 1].Text);

                    i++;
                    update_UI();
                }
                remove_score(2);
                add_score(2, name, score);
                update_UI();
            }
            else if (Convert.ToUInt64(score) > Convert.ToUInt64(score_block[3].Text))
            {
                remove_score(4);
                add_score(4, name_block[3].Text, score_block[3].Text);
                remove_score(3);
                add_score(3, name, score);
                update_UI();
            }
            else if (Convert.ToUInt64(score) > Convert.ToUInt64(score_block[4].Text))
            {
                remove_score(4);
                add_score(4, name, score);
                update_UI();
            }

            update_UI();
            save_scores();
            read_scores();
        }

        /// <summary>
        /// Add score is the method used mostly in compare score method
        /// </summary>
        /// This method takes index, name and score as a parameter.
        /// the length of string name is taken and is limited to three characters.
        /// <param name="index"></param>
        /// <param name="name"></param>
        /// <param name="score"></param>
        private void add_score(int index, string name, string score)
        {
            int MaxLength = 3;
            if (name.Length > MaxLength)
                name = name.Substring(0, MaxLength);
            users[index].Add(name, score);
        }

        /// <summary>
        /// This method takes index as a paramater
        /// The index is used to located the desired user score to be removed
        /// </summary>
        /// <param name="index"></param>
        private void remove_score(int index)
        {
            users[index].Remove(users[index].Keys.ElementAt(0));
        }
        /// <summary>
        /// This method saves the changes made in the dictionary and is written to the json file
        /// </summary>
        private async void save_scores()
        {
            string json = JsonConvert.SerializeObject(users);
            scoresFile = await storageFolder.CreateFileAsync("user_scores.txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);
            //Write data to the file
            await Windows.Storage.FileIO.WriteTextAsync(scoresFile, json);
        }
        /// <summary>
        /// This method is the opposite of save_scores()
        /// it reads from the Json file to be outputed for user visual
        /// </summary>
        private async void read_scores()
        {
            //read file
            string savedTickets = await Windows.Storage.FileIO.ReadTextAsync(scoresFile);
            Debug.Write(savedTickets);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Start)); //open the start window again
        }

        private void TextBlock_SelectionChanged_1(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

