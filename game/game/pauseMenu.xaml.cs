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
    /// Interaction logic for pauseMenu.xaml
    /// </summary>
    public partial class pauseMenu : Window
    {
        public pauseMenu()
        {
            InitializeComponent();
        }

        private void ResumeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow gameWindow = new MainWindow(); //pauseMenu is the name of the pauseMenu.xaml file
            gameWindow.Show();
            this.Close();
        }

        private void quit_Click(object sender, RoutedEventArgs e)
        {
            //Application.Exit();
            MainWindow gameWindow = new MainWindow(); //gameWindow is the name of the MainWindow.xaml file
            gameWindow.Close();
            this.Close();
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            Start toStart = new Start(); //toStart is the name of the Start.xaml file
            toStart.Show();
            this.Close();
        }
    }
}
