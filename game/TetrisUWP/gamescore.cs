using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisUWP
{
    class gamescore
    {

        const int MaxLength = 3;

        public static string player = "leo";
        public static string rank = "3";
        public static string points = "101230";
        public static string cleared = "11232100";


        public static int compare = Convert.ToInt32(points);

        static void Main(string[] args)
        {
            string scores = @"C:\Users\jorom\source\repos\Tetris\game\TetrisUWP\SavedHighScore.txt";

            List<highscore> record = new List<highscore>();
           // List<string> txt = File.ReadAllLines(scores).ToList(); //reading from text file

            foreach (var line in txt)
            {
                string[] input = line.Split('.'); //used to spit in the text
                highscore newHighScore = new highscore();

                //split using '.'
                //array elements
                newHighScore.Rank = input[0];
                newHighScore.UserName = input[1];
                newHighScore.Score = input[2];
                newHighScore.LinesCleared = input[3];

                record.Add(newHighScore);
            }

            //writing to text file

            record.Add(new highscore { Rank = rank, UserName = player, Score = points, LinesCleared = cleared });

            List<string> outtxt = new List<string>();


            foreach (var highscore in record)
            {
                outtxt.Add($"{highscore.Rank}.{highscore.UserName}.{highscore.Score}.{highscore.LinesCleared}.");
            }

           // File.WriteAllLines(scores, outtxt);

            //reading the text filew
            foreach (var highscore in record)
            {
                //Console.WriteLine($"#{highscore.Rank} {highscore.UserName} {highscore.Score} {highscore.LinesCleared}");
            }

               // Console.ReadLine();
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
