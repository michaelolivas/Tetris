using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace tetrisMUWP
{
    class Score
    {
        public string username { get; set; }
        public int value { get; set; }

        private static string _fileName = "scores.xml";

        public List<Score> Highscores { get; private set; }

        public List<Score> Scores{ get; private set; }

        public Score()
            : this(new List<Score>())
        {

        }

        public Score(List<Score> scores)
        {
            Scores = scores;
            UpdateHighscores();
        }
        public void add(Score score)
        {
            Scores.Add(score);

            Scores = Scores.OrderByDescending(c => c.value).ToList();

            UpdateHighscores();
        }

        public static Score Load()
        {
            //if file doesnt exist create a file
            if (!File.Exists(_fileName))
                return new Score();
            //if it exists. load 
            using(var reader = new StreamReader(new FileStream(_fileName, FileMode.Open)))
            {
                var serializer = new XmlSerializer(typeof(List<Score>));

                var scores = (List<Score>)serializer.Deserialize(reader);

                return new Score(scores);
            }
        }

        public void UpdateHighscores()
        {
            Highscores = Scores.Take(5).ToList();
        }

        public static void Save(Score scoreManager)
        {
            using (var writer = new StreamWriter(new FileStream(_fileName, FileMode.Create)))
            {
                var serializer = new XmlSerializer(typeof(List<Score>));

                serializer.Serialize(writer, scoreManager.Scores);
            }
        }
    }
}
