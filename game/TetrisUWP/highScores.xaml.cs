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



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TetrisUWP
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

        string NewName = "Jorome";
        string NewScore = "31100"; //will be used for passed in score

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

            //// Use for Testing /////
            /* compare_score("test2", "3500");
             compare_score("rigo", "4500");
             compare_score("bert", "5000");
             compare_score("leo", "3000");
             compare_score("midtest", "4700");
             compare_score("test3", "4999");*/

            notupdate_score();
            save_scores();
            read_scores();
        }

        private void update_UI()
        {
            for (int i = 0; i < NUM_OF_USERS; i++)
            {
                name_block[i].Text = users[i].Keys.ElementAt(0);
                score_block[i].Text = users[i].Values.ElementAt(0).ToString();
            }
        }

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
               add_score(4,name_block[3].Text,score_block[3].Text);
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

            save_scores();
            read_scores();
        }

        private void add_score(int index, string name, string score)
        {
            int MaxLength = 3;
            if (name.Length > MaxLength)
                name = name.Substring(0, MaxLength);
            users[index].Add(name, score);
        }
        private void remove_score(int index)
        {
            users[index].Remove(users[index].Keys.ElementAt(0));
        }
        private async void save_scores()
        {
            string json = JsonConvert.SerializeObject(users);
            scoresFile = await storageFolder.CreateFileAsync("user_scores.json", Windows.Storage.CreationCollisionOption.ReplaceExisting);
            //Write data to the file
            await Windows.Storage.FileIO.WriteTextAsync(scoresFile, json);
        }
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
        
        private void notupdate_score()
        {
            save.Visibility = Visibility.Collapsed;
            nameplayer.Visibility = Visibility.Collapsed;
            if (Convert.ToUInt64(NewScore) < Convert.ToUInt64(score_block[4].Text))
            {
                save.Visibility = Visibility.Collapsed;
                nameplayer.Visibility = Visibility.Collapsed;
            }   
        }
        
        private void Save_Click(object sender, RoutedEventArgs e)
        {
                save.Visibility = Visibility.Collapsed;
                nameplayer.Visibility = Visibility.Collapsed;

               compare_score(NewName, NewScore); // test score
        }

        private void player_name(TextBox sender, TextBoxTextChangingEventArgs args)
        {
          NewName = nameplayer.Text;
        }
    }
}

