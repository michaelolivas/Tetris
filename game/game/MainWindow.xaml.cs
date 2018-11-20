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

namespace game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        private static System.Timers.Timer aTimer;

        public MainWindow()
        {
            
            //Start startWindow = new Start(); //pauseMenu is the name of the pauseMenu.xaml file
            //startWindow.Show();
            InitializeComponent();
            /* Grid bar = new Grid();

             Rectangle one = new Rectangle();
             one.Height = 20;
             one.Width = 20;
             one.Fill = Brushes.Red;
             one.Stroke = Brushes.Black;
             one.Margin = new Thickness(0,-500,0,0);

             GridOne.Children.Add(one);*/

            Grid bar = create_z();
            GridOne.Children.Add(bar);
            bar.Margin = new Thickness(0, 120, 0, 0);
            bar.TranslatePoint(new Point(40,40),bar);

        }
        Grid create_z()
        {
            Grid z = new Grid();

            Rectangle one = new Rectangle();
            one.Height = 20;
            one.Width = 20;
            one.Fill = Brushes.LightGreen;
            one.Stroke = Brushes.Black;

            Rectangle two = new Rectangle();
            two.Height = 20;
            two.Width = 20;
            two.Fill = Brushes.LightGreen;
            two.Stroke = Brushes.Black;
            two.Margin = new Thickness(-40, 0, 0, 0);

            Rectangle three = new Rectangle();
            three.Height = 20;
            three.Width = 20;
            three.Fill = Brushes.LightGreen;
            three.Stroke = Brushes.Black;
            three.Margin = new Thickness(0, 0, 0, -40);

            Rectangle four = new Rectangle();
            four.Height = 20;
            four.Width = 20;
            four.Fill = Brushes.LightGreen;
            four.Stroke = Brushes.Black;
            four.Margin = new Thickness(0, 0, -40, -40);

            z.Children.Add(one);
            z.Children.Add(two);
            z.Children.Add(three);
            z.Children.Add(four);
            return z;
        }

        Grid create_square()
        {
            Grid square = new Grid();

            Rectangle one = new Rectangle();
            one.Height = 20;
            one.Width = 20;
            one.Fill = Brushes.Yellow;
            one.Stroke = Brushes.Black;

            Rectangle two = new Rectangle();
            two.Height = 20;
            two.Width = 20;
            two.Fill = Brushes.Yellow;
            two.Stroke = Brushes.Black;
            two.Margin = new Thickness(0, -40, 0, 0);

            Rectangle three = new Rectangle();
            three.Height = 20;
            three.Width = 20;
            three.Fill = Brushes.Yellow;
            three.Stroke = Brushes.Black;
            three.Margin = new Thickness(0, -40, 40, 0);

            Rectangle four = new Rectangle();
            four.Height = 20;
            four.Width = 20;
            four.Fill = Brushes.Yellow;
            four.Stroke = Brushes.Black;
            four.Margin = new Thickness(0, 0, 40,0);

            square.Children.Add(one);
            square.Children.Add(two);
            square.Children.Add(three);
            square.Children.Add(four);
            return square;
        }
        Grid create_bar()
        {
            Grid bar = new Grid();

            Rectangle one = new Rectangle();
            one.Height = 20;
            one.Width = 20;
            one.Fill = Brushes.Red;
            one.Stroke = Brushes.Black;

            Rectangle two = new Rectangle();
            two.Height = 20;
            two.Width = 20;
            two.Fill = Brushes.Red;
            two.Stroke = Brushes.Black;
            two.Margin = new Thickness(0, -40, 0, 0);

            Rectangle three = new Rectangle();
            three.Height = 20;
            three.Width = 20;
            three.Fill = Brushes.Red;
            three.Stroke = Brushes.Black;
            three.Margin = new Thickness(0, -80, 0, 0);

            Rectangle four = new Rectangle();
            four.Height = 20;
            four.Width = 20;
            four.Fill = Brushes.Red;
            four.Stroke = Brushes.Black;
            four.Margin = new Thickness(0, -120, 0, 0);

            bar.Children.Add(one);
            bar.Children.Add(two);
            bar.Children.Add(three);
            bar.Children.Add(four);

            return bar;
        }

        private void Button_Click(object sender, RoutedEventArgs e) //high score
        {

        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            pauseMenu myMenu = new pauseMenu(); //pauseMenu is the name of the pauseMenu.xaml file
            myMenu.Show();
            this.Close();
        }

    }
}
