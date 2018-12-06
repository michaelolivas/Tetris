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
                users[i].Add("Mike", "2000");
                //add_score(0, "Rigo", "100");
                name_block[i].Text = users[i].Keys.ElementAt(0);
                score_block[i].Text = users[i].Values.ElementAt(0);
            }
            
            save_scores();
            read_scores();
        }
        private void add_score(int index,string name, string score)
        {
            for(int i = 0; i <NUM_OF_USERS; i++)
            {

            }

            remove_score(index);
            users[index].Add(name, score);
        }
        private void remove_score(int index)
        {
            users[index].Remove(users[index].Keys.ElementAt(0));
        }
        private async void save_scores()
        {
            string json = JsonConvert.SerializeObject(users);
            scoresFile = await storageFolder.CreateFileAsync("user_scores.txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);
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

        private void TextBlock_SelectionChanged()
        {

        }

        private void TextBlock_SelectionChanged_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
