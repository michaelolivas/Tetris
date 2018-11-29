using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace game
{
    class gamescore
    {
        static void Main(string[] args)
        {
            string scores = @"C:\Users\jorom\source\repos\Tetris\game\TextFile1.txt";

            List<highscore> record = new List<highscore>();
            List<string> lines = File.ReadAllLines(scores).ToList(); //reading from text file

            foreach (var line in lines)
            {
                string[] input = line.Split('.'); //period used to split the text in text file

                highscore newHighScore = new highscore();


                //split using '.'
                //array elements
                newHighScore.Rank = input[0];
                newHighScore.UserName = input[1];
                newHighScore.Score = input[2];
                newHighScore.LinesCleared = input[3];

                record.Add(newHighScore);
            }
            foreach (var highscore in record)
            {
                Console.WriteLine($"{highscore.Rank}: {highscore.UserName} {highscore.Score}: {highscore.LinesCleared}:");
            }
            Console.ReadLine();
        }
    }
    public partial class highscore
    {
        public string Rank { get; set; }
        public string UserName { get; set; }
        public string LinesCleared { get; set; }
        public string Score { get; set; }
    }
}
