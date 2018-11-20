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
using System.Windows.Shapes;

namespace game
{
    /// <summary>
    /// Interaction logic for Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        public Start()
        {
            InitializeComponent();
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            MainWindow gameWindow = new MainWindow(); //pauseMenu is the name of the pauseMenu.xaml file
            gameWindow.Show();
            this.Close();
        }

        private void highScore_Click(object sender, RoutedEventArgs e)
        {
            highScores scoreWindow = new highScores();
            scoreWindow.Show();
            this.Close();
        }
    }
}
