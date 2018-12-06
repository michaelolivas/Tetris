﻿using System;
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
        public Dictionary<string, ulong>[] users = { new Dictionary<string, ulong>(), new Dictionary<string, ulong>(), new Dictionary<string, ulong>(), new Dictionary<string, ulong>(), new Dictionary<string, ulong>() };
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
                users[i].Add("Mike", 2000);
                name_block[i].Text = users[i].Keys.ElementAt(0).ToString();
                score_block[i].Text = users[i].Values.ElementAt(0).ToString();
            }

            compare_score("Jorome", 2001);
            compare_score("dylan", 2002);
            update_UI();
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
        //works, still need to work on conditions
        private void compare_score(string name, ulong score)
        {
            if (score > Convert.ToUInt64(score_block[0].Text))
            {
                remove_score(0);
                add_score(0, name, score);
                update_UI();
            }

            else if (score > Convert.ToUInt64(score_block[1].Text))
            {
                remove_score(1);
                add_score(1, name, score);
                update_UI();
            }
                 else if (score > Convert.ToUInt64(score_block[2].Text))
            {
                remove_score(2);
                add_score(2, name, score);
                update_UI();
            }
            else if (score > Convert.ToUInt64(score_block[3].Text))
            {
                remove_score(3);
                add_score(3, name, score);
                update_UI();
            }
            else if (score > Convert.ToUInt64(score_block[4].Text))
            {
                remove_score(4);
                add_score(4, name, score);
                update_UI();
            }
            

        }
        private void add_score(int index,string name, ulong score)
        {
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
        public async Task<Dictionary<string,string>[]> saveToTxt(string path,string json)
        {
            string directory = @"C:\Users\rigom\source\repos\Tetriss\game\TetrisUWP\" + "Scores" + ".txt";
            await Task.Run(() =>
            {
                Task.Yield();
                using (var file = File.Create(directory))
                {
                    File.WriteAllText(path, json);
                }
            });
            return null;
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
